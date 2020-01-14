using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Caching.Memory;

namespace Wboy.Infrastructure.Core.Cache
{
    public class MemoryCacheFactory: ICacheFactory
    {
        /// <summary>
        /// 创建一个新的Cache
        /// </summary>
        /// <returns>创建后的 Cache</returns>
        public ICache Create()
        {
            return new MemoryCacheService();
        }
    }
}
