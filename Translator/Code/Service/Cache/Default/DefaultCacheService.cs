using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.Serialization;

namespace Translator.Code.Service.Cache.Default
{
    public class DefaultCacheService : ICacheService
    {
        private ObjectCache Cache { get { return MemoryCache.Default; } }

        public object this[string key]
        {
            get
            {
                return this.Get(key);
            }
            set
            {
                this.Set(key, value, 200);
            }
        }

        public object Get(string key)
        {
            return Cache[key];
        }

        public void Set(string key, object value, int cacheTime)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);

            Cache.Add(new CacheItem(key, value), policy);
        }

        public bool IsSet(string key)
        {
            return (Cache[key] != null);
        }
    }
}