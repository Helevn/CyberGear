using COM.CAN;
using COM.CAN.CanHelper;
using COM.CAN.COMHelper;
using CyberGear.Shared.Notification;
using MediatR;
using Microsoft.Extensions.Logging;
using Reactive.Bindings;
using System.Reactive.Linq;

namespace CyberGear.Client.ViewModels.Custom
{
    public class CanConnectViewModel
    {
        private readonly IMediator _mediator;

        public CanConnectViewModel(string canName, IMediator mediator)
        {
            CanName = canName;
            this._mediator = mediator;
            this.SelectComPortName = new ReactiveProperty<string>();
            this.ListComPortName = new ReactiveProperty<IList<string>>();
            this.Canid = new ReactiveProperty<byte>();
            this.CanEnable = new ReactiveProperty<bool>();

            this.CmdReLoad = new ReactiveCommand().WithSubscribe(async () =>
            {
                this.ListComPortName.Value = await COMHelper.GetPortName();
            });

            this.CanNameid = this.CanEnable.Select(s =>
             {
                 if (s)
                     return $"{this.CanName}:{Convert.ToHexString([this.Canid.Value])}";
                 else
                     return string.Empty;
             }).ToReactiveProperty();

            this.CmdConnect = this.SelectComPortName
                .CombineLatest(this.Canid)
                .Select(i => !string.IsNullOrEmpty(i.First) && i.Second != default)
                .ToReactiveCommand()
                .WithSubscribe(async () =>
                {
                    try
                    {
                        if (this.CanCOM?.Connect ?? false)
                        {
                            await RecordLogAsync(LogLevel.Information, $"{this.CanCOM.PortName}已开启");
                            return;
                        }
                        this.CanCOM = new CanCOM(this.SelectComPortName.Value);
                        this.CyberGearCanCmd = new CyberGearCanCmd(0xfd, this.Canid.Value);
                        await this.CanCOM.EnsureConnectAsync();
                        this.CanEnable.Value = true;
                        await RecordLogAsync(LogLevel.Information, $"{this.CanCOM.PortName}已开启");
                    }
                    catch (Exception ex)
                    {
                        this.CanCOM = null;
                        this.CyberGearCanCmd = null;
                        await RecordLogAsync(LogLevel.Error, ex.ToString());
                    }
                });
            this.CmdDisConnect = new ReactiveCommand()
                .WithSubscribe(async () =>
                {
                    this.CanCOM?.DisposeAsync();
                    this.CanEnable.Value = false;
                    await RecordLogAsync(LogLevel.Information, $"{this.CanCOM?.PortName}已断开");
                    this.CanCOM = null;
                    this.CyberGearCanCmd = null;
                });

            #region Jog
            this.CmdJogForward = new ReactiveCommand()
                .WithSubscribe(async () =>
                {
                    try
                    {
                        await SendATAsync();
                        var cmd = this.CyberGearCanCmd!.CmdSetJogForward();
                        await RecordLogAsync(LogLevel.Information, $"Send  =>  {Convert.ToHexString(cmd)}");
                        var rev = await this.CanCOM!.WriteAsync(cmd);
                        await RecordLogAsync(LogLevel.Information, $"Rev  =>{rev}");
                    }
                    catch (Exception ex)
                    {
                        await RecordLogAsync(LogLevel.Error, ex.ToString());
                    }
                });
            this.CmdJogReverse = new ReactiveCommand()
                .WithSubscribe(async () =>
                {
                    try
                    {
                        await SendATAsync();
                        var cmd = this.CyberGearCanCmd!.CmdSetJogReverse();
                        await RecordLogAsync(LogLevel.Information, $"Send  =>  {Convert.ToHexString(cmd)}");
                        var rev = await this.CanCOM!.WriteAsync(cmd);
                        await RecordLogAsync(LogLevel.Information, $"Rev  =>{rev}");
                    }
                    catch (Exception ex)
                    {
                        await RecordLogAsync(LogLevel.Error, ex.ToString());
                    }
                });
            this.CmdJogStop = new ReactiveCommand()
                .WithSubscribe(async () =>
                {
                    try
                    {
                        await SendATAsync();
                        var cmd = this.CyberGearCanCmd!.CmdSetJogStop();
                        await RecordLogAsync(LogLevel.Information, $"Send  =>  {Convert.ToHexString(cmd)}");
                        var rev = await this.CanCOM!.WriteAsync(cmd);
                        await RecordLogAsync(LogLevel.Information, $"Rev  =>{rev}");
                    }
                    catch (Exception ex)
                    {
                        await RecordLogAsync(LogLevel.Error, ex.ToString());
                    }
                });
            #endregion

            this.CmdReLoad.Execute();
            this.SelectComPortName.Value = this.ListComPortName.Value.FirstOrDefault() ?? "";
        }

        public string CanName { get; set; }
        public CanCOM? CanCOM { get; set; }
        public CyberGearCanCmd? CyberGearCanCmd { get; set; }

        public ReactiveCommand CmdReLoad { get; }
        public ReactiveCommand CmdConnect { get; }
        public ReactiveCommand CmdDisConnect { get; }
        #region Jog
        public ReactiveCommand CmdJogForward { get; }
        public ReactiveCommand CmdJogReverse { get; }
        public ReactiveCommand CmdJogStop { get; }
        #endregion


        public ReactiveProperty<string> SelectComPortName { get; set; }
        public ReactiveProperty<IList<string>> ListComPortName { get; set; }
        public ReactiveProperty<string?> CanNameid { get; set; }
        public ReactiveProperty<byte> Canid { get; set; }
        public ReactiveProperty<bool> CanEnable { get; set; }

        private async Task SendATAsync()
        {
            var cmd = this.CyberGearCanCmd!.AT_Command_Mode();
            await RecordLogAsync(LogLevel.Information, $"Send  =>  {Convert.ToHexString(cmd)}");
            var rev = await this.CanCOM!.WriteAsync(cmd);
            await RecordLogAsync(LogLevel.Information, $"Rev  =>{rev}");
        }

        private async Task RecordLogAsync(LogLevel logLevel, string msg)
        {
            var log = new LogMessage
            {
                Level = logLevel,
                Content = msg,
                Timestamp = DateTime.Now,
            };
            await _mediator.Publish(new UILogNotification(log));
        }
    }
}
