using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wboy.Infrastructure.Core.Cache;

namespace Wboy.Infrastructure.Core.Configuration
{
    public class ClearSettingCacheHelper
    {
        private readonly ICache _cache;
        private const string SettingProviderCacheAllData = "SETTING_PROVIDER_CACHE_ALL_DATA";

        public ClearSettingCacheHelper(ICache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// 手动清除缓存
        /// </summary>
        public void ClearSettingCache()
        {
            if (_cache != null)
            {
                if (!_cache.Contains(SettingProviderCacheAllData))
                {
                    _cache.Remove(SettingProviderCacheAllData);
                }
            }
        }
    }
}
