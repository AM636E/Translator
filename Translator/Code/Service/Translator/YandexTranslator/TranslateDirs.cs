using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Translator.Code.Service.Translator.YandexTranslator
{
    public class TranslateDirs
    {
        public List<string> dirs { get; set; }
        public Dictionary<string, string> langs { get; set; }
    }
}