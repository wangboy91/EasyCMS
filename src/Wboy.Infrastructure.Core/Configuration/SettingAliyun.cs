using System;
using System.Collections.Generic;
using System.Text;

namespace Wboy.Infrastructure.Core.Configuration
{
    public class SettingAliyun : ISetting
    {
        [AttributeSetting(Description = "Sms_AccessId", DefaultValue = " ")]
        public string SmsAccessId { get; set; }

        [AttributeSetting(Description = "Sms_AccessSecret", DefaultValue = " ")]
        public string SmsAccessSecret { get; set; }
        [AttributeSetting(Description = "Sms_SignName", DefaultValue = " ")]
        public string SmsSignName { get; set; }
        [AttributeSetting(Description = "Sms_TemplateCode", DefaultValue = " ")]
        public string SmsTemplateCode { get; set; }

        [AttributeSetting(Description = "Oss_AccessId", DefaultValue = " ")]
        public string OssAccessId { get; set; }

        [AttributeSetting(Description = "Oss_AccessSecret", DefaultValue = " ")]
        public string OssAccessSecret { get; set; }
        [AttributeSetting(Description = "Endpoint", DefaultValue = "")]
        public string OssEndpoint { get; set; }

        [AttributeSetting(Description = "BucketName", DefaultValue = " ")]
        public string OssBucketName { get; set; }
        [AttributeSetting(Description = "访问域名地址", DefaultValue = "")]
        public string OssDomainUrl { get; set; }

    }
}
