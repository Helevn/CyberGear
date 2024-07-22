
using CyberGear.Client.ViewModels;
using CyberGear.Client.Views;
using CyberGear.Shared;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace CyberGear.Client
{

    public static class ViewsServiceCollectionExtensions
    {
        public static IServiceCollection AddViews(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var vm = ActivatorUtilities.CreateInstance<AppViewModel>(sp);
                vm.AppVersion.Value = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "";
                var appopt = sp.GetRequiredService<IOptions<AppOpt>>().Value;

                vm.AppTitle.Value = appopt.AppTitle;

                vm.MapSourceToPage = url => url switch
                {
                    UrlDefines.URL_Realtime => sp.GetRequiredService<RealtimeView>(),
                    _ => throw new Exception($"未知的URL={url}"),
                };
                return vm;
            });
            services.AddSingleton<MainWindow>();
            services.AddSingleton<RealtimeView>();
            services.AddSingleton<RealtimeViewModel>();
            return services;
        }
    }
}
