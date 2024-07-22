using CyberGear.Client.ViewModels;
using CyberGear.Shared.Notification;
using MediatR;

namespace CyberGear.Client.MessageHandler
{
    public class UILogNotificationHandler(RealtimeViewModel realtimeViewModel) : INotificationHandler<UILogNotification>
    {
        public Task Handle(UILogNotification notification, CancellationToken cancellationToken)
        {
            UIHelper.RunInUIThread(pl =>
            {
                realtimeViewModel.AddLogMsg.Execute(notification.LogMessage);
            });
            return Task.CompletedTask;
        }
    }
}
