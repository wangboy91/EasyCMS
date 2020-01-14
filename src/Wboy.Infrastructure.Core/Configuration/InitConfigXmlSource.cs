using System;
using System.Collections.Generic;
using System.IO;
using Wboy.Infrastructure.Core.Cache;
using Wboy.Infrastructure.Core.Extension;

namespace Wboy.Infrastructure.Core.Configuration
{
    public class InitConfigXmlSource
    {
        private readonly ICache _cache;
        private const string InitializeConfigCacheKey = "Wboy.InitConfigXml";
        public InitConfigXmlSource(ICache cache)
        {
            _cache = cache;
        }
        public Dictionary<string, object> GetAll()
        {
            var settings = new Dictionary<string, object>();
            if (_cache.Contains(InitializeConfigCacheKey))
            {
                settings = _cache.Get<Dictionary<string, Object>>(InitializeConfigCacheKey);
            }
            else
            {
                settings = LoadFileData();
                _cache.Set(InitializeConfigCacheKey, settings, 10800);//缓存3个小时
            }
            return settings;
        }

        private Dictionary<string, object> LoadFileData()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filepath = Path.Combine(baseDirectory, "Configs/Initialize.config");
            if (File.Exists(filepath))
            {
                string text = File.ReadAllText(filepath);
                return ParseSettings(text);
            }
            return new Dictionary<string, object>();
        }
        private Dictionary<string, object> ParseSettings(string text)
        {
            var settings = new Dictionary<string, object>();
            using (var reader = new StringReader(text))
            {
                string str;
                while ((str = reader.ReadLine()) != null)
                {
                    var separatorIndex = str.IndexOf(':');
                    if (separatorIndex == -1)
                    {
                        continue;
                    }
                    string key = str.Substring(0, separatorIndex).Trim();
                    string value = str.Substring(separatorIndex + 1).Trim();
                    settings.Add(key, value);
                }
            }
            return settings;
        }
    }
}
