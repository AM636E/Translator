using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Code.Service.Cache
{
    public interface ICacheService
    {
        object this[string key] { get; set; }
        object Get(string key);
        void Set(string key, object value, int cacheTime);
        bool IsSet(string key);
    }
}
