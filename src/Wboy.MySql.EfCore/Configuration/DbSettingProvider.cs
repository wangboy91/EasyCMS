using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wboy.Domain.PlatformModule.Entities;
using Wboy.Domain.PlatformModule.IRepository;
using Wboy.Infrastructure.Core.Cache;
using Wboy.Infrastructure.Core.Configuration;

namespace Wboy.MySql.EfCore.Configuration
{
    public class DbSettingProvider<TSettings> : ISettingProvider<TSettings> where TSettings : ISetting, new()
    {
        private readonly ISystemSettingRepository _settingConfigRepository;
        private readonly ICache _cache;
        private const string SettingProviderCacheAllData = "SETTING_PROVIDER_CACHE_ALL_DATA";

        public DbSettingProvider(ISystemSettingRepository settingConfigRepository,
            ICache cache)
        {
            _settingConfigRepository = settingConfigRepository;
            _cache = cache;
            BuildConfiguration();
        }

        private void BuildConfiguration()
        {
            Settings = Activator.CreateInstance<TSettings>();

            // get properties we can write to
            var properties = from prop in typeof(TSettings).GetProperties()
                where prop.CanWrite && prop.CanRead
                let settingConfig = GetSettingSourceByKey(typeof(TSettings).Name + "." + prop.Name)
                where settingConfig != null
                select new { prop, settingConfig };

            // assign properties
            properties.ToList().ForEach(p =>
            {
                var value = p.settingConfig.Value;
                switch (p.settingConfig.DataType)
                {
                    case "Int32":
                        p.prop.SetValue(Settings, Convert.ToInt32(value), null);
                        break;
                    case "Boolean":
                        p.prop.SetValue(Settings, value == "true", null);
                        break;
                    case "String":
                        p.prop.SetValue(Settings, value, null);
                        break;
                    case "Double":
                        p.prop.SetValue(Settings, Convert.ToDouble(value), null);
                        break;
                    case "List`String":
                        p.prop.SetValue(Settings, value.Split('|').ToList(), null);
                        break;
                    case "List`Int32":
                        var v = value.Split('|').ToList();
                        var vInt = new List<int>();
                        v.ForEach(i => vInt.Add(Convert.ToInt32(i)));
                        p.prop.SetValue(Settings, vInt, null);
                        break;
                    case "DateTime":
                        p.prop.SetValue(Settings, DateTime.Parse(value), null);
                        break;
                    default:
                        p.prop.SetValue(Settings, value, null);
                        break;
                }
            });
        }

        private SystemSetting GetSettingSourceByKey(string key)
        {
            if (!_cache.Contains(SettingProviderCacheAllData))
            {
                _cache.Set<List<SystemSetting>>(SettingProviderCacheAllData,
                    _settingConfigRepository.GetAll().ToList(), 10800);//缓存3个小时
            }
            var list = _cache.Get<List<SystemSetting>>(SettingProviderCacheAllData);

            return list.FirstOrDefault(i => i.Key == key);
        }

        public TSettings Settings { get; private set; }
        /// <summary>
        /// 优选冲缓存中取配置文件
        /// </summary>
        /// <param name="setting"></param>
        public void SaveSettings(TSettings setting)
        {
            var properties = from prop in typeof(TSettings).GetProperties()
                where prop.CanWrite && prop.CanRead
                let settingConfig = GetSettingSourceByKey(typeof(TSettings).Name + "." + prop.Name)
                where settingConfig != null
                select new { prop, settingConfig };

            var current = from prop in setting.GetType().GetProperties()
                where prop.CanWrite && prop.CanRead
                let key = setting.GetType().Name + "." + prop.Name
                let value = ConvertValue(prop.GetValue(setting, null))
                select new { key, value };

            bool removeCache = false;
            properties.ToList().ForEach(i =>
            {
                var p = current.ToList().FirstOrDefault(m => m.key == i.settingConfig.Key);
                if (p != null && p.value != null && p.value != i.settingConfig.Value)
                {
                    removeCache = true;
                    var sconfig = _settingConfigRepository.Get(i.settingConfig.Key);
                    sconfig.Value = p.value.ToString();
                    sconfig.UpdateTime = DateTime.Now;
                    _settingConfigRepository.UnitOfWork.Commit();
                }
            });
            if (removeCache) _cache.Remove(SettingProviderCacheAllData);
        }

        private string ConvertValue(object value)
        {
            if (value.GetType().IsGenericType)
            {
                var arrInt = value as List<int>;
                var arrStr = value as List<string>;
                var result = string.Empty;
                if (arrInt != null)
                    result = string.Join("|", arrInt);
                if (arrStr != null)
                    result = string.Join("|", arrStr);
                return result;
            }
            else
            {
                return value != null ? value.ToString() : "";
            }
        }

        public void DeleteSettings()
        {
            throw new NotImplementedException();
        }
    }
}
