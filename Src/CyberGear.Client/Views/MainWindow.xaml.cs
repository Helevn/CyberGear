using CyberGear.Client.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace CyberGear.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AppViewModel viewModel;

        public MainWindow(AppViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            this.viewModel.CurrentPage.Subscribe(page =>
            {
                if (page != null)
                {
                    this.navWin.Navigate(page);
                }
            });
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var res = MessageBox.Show("是否真的要退出？", "退出程序确认", MessageBoxButton.YesNo);
            if (!res.Equals(MessageBoxResult.Yes))
            {
                e.Cancel = true;
            }
        }
    }
}