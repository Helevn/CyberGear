namespace CyberGear.Shared
{
    /// <summary>
    /// 软件的静态配置
    /// </summary>
    public class AppOpt
    {
        /// <summary>
        /// 应用标题
        /// </summary>
        public string AppTitle { get; set; } = "";

        /// <summary>
        /// 客户端端口
        /// </summary>
        public int ClientPort { get; set; }
    }
}
