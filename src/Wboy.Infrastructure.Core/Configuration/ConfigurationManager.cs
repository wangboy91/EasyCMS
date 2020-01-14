using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wboy.Infrastructure.Core.Configuration
{
    public static class ConfigurationManager
    {
        public static TSettings GetSetting<TSettings>() where TSettings : ISetting, new()
        {
            using (var scope = SampleContext.Current.BeginScope())
            {
                var provider = scope.Resolve<ISettingProvider<TSettings>>();
                if (provider != null)
                    return provider.Settings;

                return new TSettings();
            }
        }
    }
}
