using MediatR;
using Microsoft.Extensions.Logging;

namespace CyberGear.Shared.Notification
{
    public class UILogNotification(LogMessage msg) : INotification
    {
        public LogMessage LogMessage { get; set; } = msg;
    }

    public class LogMessage
    {
        public string EventSource { get; set; } = "";

        public string EventGroup { get; set; } = "";


        public DateTime Timestamp { get; set; }

        public LogLevel Level { get; set; }

        public string Content { get; set; } = "";
    }
}
