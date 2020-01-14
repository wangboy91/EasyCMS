using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Caching.Memory;

namespace Wboy.Infrastructure.Core.Cache
{
    public class MemoryCacheService : ICache
    {
        private readonly IMemoryCache _cache;

        //public MemoryCacheService(IMemoryCache cache)
        //{
        //    _cache = cache;
        //}
        public MemoryCacheService()
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
        }
        public void Set<T>(string key, T data, int cacheSecond)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            var cacheData = (object)data;
            if (cacheData == null)
            {
                throw new ArgumentNullException(nameof(cacheData));
            }
            if (cacheSecond > 0)
            {
                _cache.Set(key, cacheData, TimeSpan.FromSeconds(cacheSecond));
            }
        }

        public T Get<T>(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (Contains(key))
            {
                return (T)_cache.Get(key);
            }
            return default(T);
        }

        public IDictionary<string, object> GetAll(IEnumerable<string> keys)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }
            var dict = new Dictionary<string, object>();
            keys.ToList().ForEach(item => dict.Add(item, _cache.Get(item)));
            return dict;
        }

        public void Remove(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            _cache.Remove(key);
        }
        public void RemoveAll(IEnumerable<string> keys)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys));
            }
            keys.ToList().ForEach(item => _cache.Remove(item));
        }

        public bool Contains(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            object cached;
            return _cache.TryGetValue(key, out cached);
        }
    }
}
