using CyberGear.Shared;
using Microsoft.Extensions.Configuration;
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
            //services.AddMediatR(
            //     typeof(MessageBoxNotificationHandler).Assembly
            // );
            #endregion

            services.AddOptions<AppOpt>().Bind(context.Configuration.GetSection("App"));
        }
    }
}
