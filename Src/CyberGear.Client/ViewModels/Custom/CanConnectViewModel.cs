using COM.CAN.COMHelper;
using Reactive.Bindings;

namespace CyberGear.Client.ViewModels.Custom
{
    public class CanConnectViewModel
    {
        public CanConnectViewModel()
        {
            this.SelectComPortName = new ReactiveProperty<string>();
            this.ListComPortName = new ReactiveProperty<IList<string>>();

            this.CmdReLoad = new ReactiveCommand().WithSubscribe(async () =>
            {
                this.ListComPortName.Value = await COMHelper.GetPortName();
            });

            this.CmdReLoad.Execute();
            this.SelectComPortName.Value = this.ListComPortName.Value.FirstOrDefault() ?? "";
        }


        public ReactiveCommand CmdReLoad { get; }

        public ReactiveProperty<string> SelectComPortName { get; set; }
        public ReactiveProperty<IList<string>> ListComPortName { get; set; }
    }
}
