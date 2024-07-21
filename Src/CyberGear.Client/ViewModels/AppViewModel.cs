using Microsoft.Extensions.DependencyInjection;
using System.Windows.Controls;
using Reactive.Bindings;

namespace CyberGear.Client.ViewModels
{

    public class AppViewModel
    {
        private readonly IServiceScopeFactory _ssf;

        public AppViewModel(IServiceScopeFactory ssf)
        {
            AppVersion = new ReactiveProperty<string>();
            AppTitle = new ReactiveProperty<string>();
            CurrentPage = new ReactiveProperty<Page>();
            _ssf = ssf;
        }


        /// <summary>
        /// 软件版本
        /// </summary>
        public ReactiveProperty<string> AppVersion { get; set; }

        /// <summary>
        /// 应用标题—— 和 AppName不同，这个可以动态变更
        /// </summary>
        public ReactiveProperty<string> AppTitle { get; set; }


        #region 路由
        /// <summary>
        /// 当前页面
        /// </summary>
        public ReactiveProperty<Page> CurrentPage { get; set; }

        public Func<string, Page> MapSourceToPage { get; set; }

        public void NavigateTo(string source)
        {
            if (MapSourceToPage == null)
            {
                throw new Exception($"{nameof(MapSourceToPage)}不可为NULL！你是否忘记设置该属性了？");
            }
            var page = MapSourceToPage(source);
            CurrentPage.Value = page;
        }
        #endregion
    }
}
