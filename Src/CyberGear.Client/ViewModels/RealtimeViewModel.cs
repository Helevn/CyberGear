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

        public RealtimeViewModel(ILogger<RealtimeViewModel> logger, IEnumerable<CanConnectViewModel> canConnectViewModels)
        {
            this._logger = logger;
            this.CanConnectViewModel1 = canConnectViewModels.FirstOrDefault(s => s.CanName == CanNameDefines.CanName1) ?? throw new Exception("是不是忘记配置CanViewModel1");
            this.CanConnectViewModel2 = canConnectViewModels.FirstOrDefault(s => s.CanName == CanNameDefines.CanName2) ?? throw new Exception("是不是忘记配置CanViewModel2");
            this.CanConnectViewModel3 = canConnectViewModels.FirstOrDefault(s => s.CanName == CanNameDefines.CanName3) ?? throw new Exception("是不是忘记配置CanViewModel3");
            this.CanConnectViewModel4 = canConnectViewModels.FirstOrDefault(s => s.CanName == CanNameDefines.CanName4) ?? throw new Exception("是不是忘记配置CanViewModel4");
            this.CanConnectViewModel5 = canConnectViewModels.FirstOrDefault(s => s.CanName == CanNameDefines.CanName5) ?? throw new Exception("是不是忘记配置CanViewModel5");
            this.CanConnectViewModel6 = canConnectViewModels.FirstOrDefault(s => s.CanName == CanNameDefines.CanName6) ?? throw new Exception("是不是忘记配置CanViewModel6");
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
        public CanConnectViewModel CanConnectViewModel1 { get; }
        public CanConnectViewModel CanConnectViewModel2 { get; }
        public CanConnectViewModel CanConnectViewModel3 { get; }
        public CanConnectViewModel CanConnectViewModel4 { get; }
        public CanConnectViewModel CanConnectViewModel5 { get; }
        public CanConnectViewModel CanConnectViewModel6 { get; }
    }
}
