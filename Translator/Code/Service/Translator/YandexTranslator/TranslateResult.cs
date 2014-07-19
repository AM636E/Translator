using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Translator.Code.Service.Translator.YandexTranslator
{
    public class TranslateResult
    {
        public int code { get; set; }
        public string lang { get; set; }
        public List<string> text { get; set; }
    }
}