using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;

using Newtonsoft.Json;

namespace Translator.Code.Service.Translator.YandexTranslator
{
    public class YandexTranslatorService : ITranslatorService
    {
        /// <summary>
        /// 
        /// </summary>
        private string baseUrl;
        /// <summary>
        /// 
        /// </summary>
        private string apiKey;
        /// <summary>
        /// 
        /// </summary>
        private string requestUri;

        /// <summary>
        /// Initializes a new instance of YandexTranslatorService
        /// With your api key.
        /// </summary>
        /// <param name="baseUrl">Base url for api calls</param>
        /// <param name="apiKey">Your Yandex.Translator Api key</param>
        public YandexTranslatorService(string baseUrl, string apiKey)
        {
            this.baseUrl = baseUrl;
            this.apiKey = apiKey;

            this.requestUri = this.baseUrl + "/{0}?key=" + apiKey;
        }

        public string DetectLang(string[] text)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetAvailableLangs(string lang)
        {
            var dirs = this.GetTranslateDir();

            var langs = new Dictionary<string, string>();

            foreach (var dir in dirs)
            {
                if (dir.Item1 == lang)
                {
                    langs.Add(dir.Item2, dir.Item2.ToUpper());
                }
            }

            return langs;
        }

        public List<Tuple<string, string>> GetTranslateDir()
        {
            string response = this.RequestTranslator("getLangs");
            TranslateDirs dirs = JsonConvert.DeserializeObject<TranslateDirs>(response);
            return (from dir in dirs.dirs
                    select Tuple.Create<string, string>(dir.Split('-')[0], dir.Split('-')[1])).ToList();
        }

        public string Translate(string sourceLang, string sourceText, string targetLanguage)
        {
            string text = sourceText.Replace(' ', '+');

            var translated = this.RequestTranslator("translate", new Dictionary<string, string>()
            {
                {"lang", targetLanguage},
                {"text", text}
            });

            TranslateResult result = JsonConvert.DeserializeObject<TranslateResult>(translated);

            if (result.code != 200)
            {
                throw new System.Exception(String.Format("Unable to translate. Code {0}", result.code));
            }

            return result.text[0];
        }

        private string RequestTranslator(string method, Dictionary<string, string> requestData)
        {
            string url = String.Format(this.requestUri, method);

            foreach (string key in requestData.Keys)
            {
                url += string.Format("&{0}={1}", key, requestData[key]);
            }

            return this.Request(url);
        }

        private string RequestTranslator(string method)
        {
            return this.Request(String.Format(this.requestUri, method));
        }

        private string Request(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse result = request.GetResponse();
            StreamReader reader = new StreamReader(result.GetResponseStream());

            string line = null;
            string data = "";
            do
            {
                line = reader.ReadLine();

                data += line;
            } while (line != null);

            return data;
        }

        public Dictionary<string, string> GetLanguages()
        {
            string languages = this.RequestTranslator("getLangs", new Dictionary<string, string>{
            {"ui" , "uk"}
            });

            return JsonConvert.DeserializeObject<TranslateDirs>(languages).langs;
        }
    }
}