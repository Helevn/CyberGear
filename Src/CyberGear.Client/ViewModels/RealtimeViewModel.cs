using CyberGear.Client.ViewModels.Custom;
using CyberGear.Shared.Notification;
using Microsoft.Extensions.Logging;
using Reactive.Bindings;
using System.Collections.ObjectModel;

namespace CyberGear.Client.ViewModels
{
    public class RealtimeViewModel
    {
        private readonly ILogger<RealtimeViewModel> _logger;

        public RealtimeViewModel(ILogger<RealtimeViewModel> logger, CanConnectViewModel canConnectViewModel)
        {
            this._logger = logger;
            this.CanConnectViewModel = canConnectViewModel;
            this.AddLogMsg = new ReactiveCommand<LogMessage>().WithSubscribe(s =>
            {
                try
                {
                    var logcount = this.Logs.Count;
                    if (logcount >= 100)
                    {
                        this.Logs.RemoveAt(0);
                    }
                    this.Logs.Add(s);
                }
                catch (Exception e)
                {
                    this._logger.LogError($"添加日志出错：{e.Message}\n{e.StackTrace}");
                }
            });
        }

        public ObservableCollection<LogMessage> Logs { get; } = [];
        public ReactiveCommand<LogMessage> AddLogMsg { get; }
        public CanConnectViewModel CanConnectViewModel { get; }
    }
}
