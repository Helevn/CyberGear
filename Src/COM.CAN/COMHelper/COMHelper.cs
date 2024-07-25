using System.IO.Ports;

namespace COM.CAN.COMHelper
{
    public class COMHelper
    {
        public static Task<List<string>> GetPortName()
        {
            return Task.FromResult(SerialPort.GetPortNames().ToList());
        }
    }
}
