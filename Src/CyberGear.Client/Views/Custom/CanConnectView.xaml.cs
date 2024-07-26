using CyberGear.Client.ViewModels.Custom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CyberGear.Client.Views.Custom
{
    /// <summary>
    /// CanConnectView.xaml 的交互逻辑
    /// </summary>
    public partial class CanConnectView : UserControl
    {
        public CanConnectView()
        {
            InitializeComponent();
        }

        #region Jog
        private void Jogadd_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.DataContext is CanConnectViewModel vm)
            {
                vm.CmdJogForward.Execute();
            }
        }
        private void Jogadd_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.DataContext is CanConnectViewModel vm)
            {
                vm.CmdJogStop.Execute();
            }
        }

        private void Jogsub_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.DataContext is CanConnectViewModel vm)
            {
                vm.CmdJogReverse.Execute();
            }
        }
        private void Jogsub_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.DataContext is CanConnectViewModel vm)
            {
                vm.CmdJogStop.Execute();
            }
        }
        #endregion
    }
}
