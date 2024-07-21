using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace CyberGear.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Mutex _singletonMutex;

        public App()
        {
            var appname = typeof(App).AssemblyQualifiedName;
            this._singletonMutex = new Mutex(true, appname, out var createdNew);

            if (!createdNew)
            {
                MessageBox.Show($"站控软件已经启动！不可重复启动！");
                Environment.Exit(0);
                return;
            }

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            this._host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, builder) =>
                {
                    builder
                        .SetBasePath(context.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        ;
                    builder.AddEnvironmentVariables();
                })
                .ConfigureLogging((ctx, logging) =>
                {
                    if (ctx.HostingEnvironment.IsDevelopment())
                    {
                        logging.AddDebug();
                    }
                    logging.AddLog4Net();
                })
                .ConfigureServices(HostStartup.ConfigureServices)
                .Build();
            this.RootServiceProvider = this._host.Services;
        }

        public IHost _host { get; private set; }
        public IServiceProvider RootServiceProvider { get; internal set; }

        private CancellationTokenSource cts = new();

        private void SigninOperatorAsync(IServiceProvider sp)
        {
            var mainWin = sp.GetRequiredService<MainWindow>() as MainWindow;
            mainWin.Show();
        }
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            RenderOptions.ProcessRenderMode = System.Windows.Interop.RenderMode.SoftwareOnly;
            try
            {
                SigninOperatorAsync(this.RootServiceProvider);
                var thread = new Thread(async () =>
                {
                    try
                    {
                        // do sth before running
                        await _host.RunAsync(cts.Token);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        this.Shutdown();
                    }
                });
                thread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                using (var scope = this.RootServiceProvider.CreateScope())
                {
                    var sp = scope.ServiceProvider;
                    var logger = sp.GetRequiredService<ILogger<App>>();
                    logger.LogError($"{ex.Message}\r\n{ex.Message}");
                }
                this.Shutdown();
            }
        }


        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                this._singletonMutex.ReleaseMutex();
            }
            finally
            {
                using (_host)
                {
                    var lieftime = _host.Services.GetRequiredService<IHostApplicationLifetime>();
                    lieftime.StopApplication();
                }
                base.OnExit(e);
            }
        }
    }
}
