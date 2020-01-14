using System;
using System.Collections.Generic;
using System.Text;

namespace Wboy.Infrastructure.Core.Configuration
{
    public class SettingBase : ISetting
    {
        [AttributeSetting(Description = "SystemName", DefaultValue = "EasyCMS")]
        public string SystemName { get; set; }
        [AttributeSetting(Description = "imgHost", DefaultValue = " ")]
        public string ImgHost { get; set; }

        [AttributeSetting(Description = "OssfileHost", DefaultValue = " ")]
        public string OssFileHost { get; set; }

        [AttributeSetting(Description = "OssimgHost", DefaultValue = " ")]
        public string OssImgHost { get; set; }
    }
}
