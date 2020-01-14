using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wboy.Infrastructure.Core.Cache
{
    public class CacheFactory
    {

        private static ICacheFactory _cacheFactory;

        /// <summary>
        /// 设置一个CacheFactory的创建工厂实例
        /// </summary>
        /// <param name="cacheFactory">工厂实例</param>
        public static void SetCurrent(ICacheFactory cacheFactory)
        {
            _cacheFactory = cacheFactory;
        }

        private static ICache _cache;
        /// <summary>
        /// 创建一个新的Logger
        /// </summary>
        /// <returns>创建后的 Logger</returns>
        public static ICache CreateCache()
        {
            if (_cacheFactory != null && _cache == null)
            {
                _cache = _cacheFactory.Create();
            }
            return _cache;
            //return _cache ?? (_cache = new MemoryCache());

        }
    }
}
