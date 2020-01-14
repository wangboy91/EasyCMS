using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wboy.Infrastructure.Core.Configuration;

namespace WebApp.Extensions
{
    public static class HtmlExtensions
    {
        public static IHtmlContent WebFrontConfig()
        {
            var setting = ConfigurationManager.GetSetting<SettingBase>();
            var sb = new StringBuilder();
            sb.Append("<script type=\"text/javascript\">");
            sb.Append($"var hostConfig = {{ ossfileHost: \"{setting.OssFileHost}\", ossImgHost: \"{setting.OssImgHost}\",imgHost: \"{setting.ImgHost}\"}};");
            sb.Append("</script>");
            return new HtmlString(sb.ToString());
        }
    }
}
