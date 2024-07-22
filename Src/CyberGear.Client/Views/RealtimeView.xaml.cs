using CyberGear.Client.ViewModels;
using System.Windows.Controls;

namespace CyberGear.Client.Views
{
    /// <summary>
    /// RealtimeView.xaml 的交互逻辑
    /// </summary>
    public partial class RealtimeView : Page
    {
        public RealtimeView(RealtimeViewModel vm)
        {
            InitializeComponent();
            this.DataContext = vm;
        }
        private void Scroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (!scroll.IsMouseOver)
            {
                this.scroll.ScrollToEnd();
            }
        }
    }
}
