using System.IO.Ports;
using static System.Net.Mime.MediaTypeNames;

namespace COM.CAN
{
    public class CanCOM(string portName)
    {
        private SerialPort? _port;
        public readonly string PortName = portName;
        public bool Connect => _port?.IsOpen ?? false;

        public async Task<string> WriteAsync(byte[] cmd)
        {
            await EnsureConnectAsync();
            _port!.Write(cmd, 0, cmd.Length);
            var cancle = new CancellationTokenSource(1000);
            do
            {
                var rev = await ReadAsync();
                if (!string.IsNullOrEmpty(rev))
                    return rev;
            } while (!cancle.Token.IsCancellationRequested);

            throw new Exception($"串口{this.PortName}返回超时");
        }

        public Task<string> ReadAsync()
        {
            string rev = "";
            do
            {
                var buffers = new byte[_port!.BytesToRead];
                if (buffers.Length > 0)
                {
                    _port.Read(buffers, 0, buffers.Length);
                    rev += Convert.ToHexString(buffers);
                }
            } while (_port!.BytesToRead > 0);
            return Task.FromResult(rev);
        }

        public Task DisposeAsync()
        {
            if (_port != null)
            {
                _port.Close();
                _port.Dispose();
            }
            return Task.CompletedTask;
        }

        public Task EnsureConnectAsync()
        {
            if (_port != null && _port.IsOpen)
            {
                return Task.CompletedTask;
            }

            _port ??= new SerialPort
            {
                PortName = PortName,
                BaudRate = 921600,
                DataBits = 8,
                StopBits = StopBits.One,
                ParityReplace = 0
            };
            _port.Open();
            return Task.CompletedTask;
        }
    }
}
