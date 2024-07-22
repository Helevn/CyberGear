using CyberGear.Client.MessageHandler;
using CyberGear.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CyberGear.Client
{
    public class HostStartup
    {
        public static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
        {

            services.AddViews();

            #region Message Handlers
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(UILogNotificationHandler).Assembly));
            #endregion

            services.AddOptions<AppOpt>().Bind(context.Configuration.GetSection("App"));
        }
    }
}
