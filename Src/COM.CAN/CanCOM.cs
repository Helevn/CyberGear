using System.IO.Ports;

namespace Can
{
    public class CanCOM
    {
        private SerialPort _port;
        public readonly string PortName;
        public CanCOM(string portName)
        {
            this.PortName = portName;
        }

        public async Task<string> WriteAsync(byte[] cmd)
        {
            await EnsureConnectAsync();
            _port.Write(cmd, 0, cmd.Length);
            return await ReadAsync();
        }

        public async Task<string> ReadAsync()
        {
            ushort i = 0;
            do
            {
                if (i == ushort.MaxValue)
                    return "";

                var buffers = new byte[_port.BytesToRead];
                if (buffers.Length > 0)
                {
                    _port.Read(buffers, 0, buffers.Length);
                    await DisposeAsync();
                    return Convert.ToHexString(buffers);
                }

            } while (true);
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
