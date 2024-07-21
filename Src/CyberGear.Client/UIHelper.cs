using System.Windows.Threading;

namespace CyberGear.Client
{

    public static class UIHelper
    {
        public static void RunInUIThread(SendOrPostCallback callback)
        {
            _ = Task.Run(() => {
                var dispatcher = System.Windows.Application.Current?.Dispatcher;
                if (dispatcher != null)
                {
                    SynchronizationContext.SetSynchronizationContext(new DispatcherSynchronizationContext(dispatcher));
                    SynchronizationContext.Current?.Post(callback, null);
                }
            });
        }
    }
}
