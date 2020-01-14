using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Wboy.Infrastructure.Core.Configuration
{
    public class DefaultSettingProvider<TSettings> : ISettingProvider<TSettings> where TSettings : ISetting, new()
    {
        public DefaultSettingProvider()
        {
            ParseSettings();
        }

        public TSettings Settings
        {
            get;
            protected set; 
        }

        private void ParseSettings()
        {
            //var dataSource = (InitConfigDataSource)NSampleContext.Current.Resolve(typeof(InitConfigDataSource));
            using (var scope = SampleContext.Current.BeginScope())
            {
                var dataSource = (InitConfigXmlSource)scope.Resolve(typeof(InitConfigXmlSource));

                var properties = from prop in typeof(TSettings).GetProperties()
                                 where prop.CanWrite && prop.CanRead
                                 let value = dataSource.GetAll().FirstOrDefault(i => i.Key == typeof(TSettings).Name + "." + prop.Name).Value
                                 where value != null
                                 select new { prop, value };

                Settings = Activator.CreateInstance<TSettings>();
                properties.ToList().ForEach(p => p.prop.SetValue(Settings, p.value, null));
            }
        }

        public virtual void SaveSettings(TSettings setting)
        {
            return;
        }

        public virtual void DeleteSettings()
        {
            return;
        }
    }
}
