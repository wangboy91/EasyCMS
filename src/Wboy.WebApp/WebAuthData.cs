using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public class WebAuthData
    {
        //public int DeviceType { set; get; }
        /// <summary>
        /// 编号
        /// </summary>
        public Guid Id { set; get; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
    }

    /// <summary>
    /// 客户端静态键
    /// </summary>
    public static class WebAuthConstKey
    {

        public static readonly string AuthName = "API-WANG-TOKEN";
        //public static readonly string CookieDomain = ".sumxiang.com";
        //public static readonly string CookieDomain = "";
    }
}
