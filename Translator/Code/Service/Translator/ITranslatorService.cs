using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Code.Service.Translator
{
    /// <summary>
    /// Represents a translator service.
    /// </summary>
    public interface ITranslatorService
    {
        /// <summary>
        /// Deletec language text is written
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        string DetectLang(string[] text);

        /// <summary>
        /// Get languages available for translation from given language
        /// </summary>
        /// <param name="lang">Language to check</param>
        /// <returns>List of languages</returns>
        Dictionary<string, string> GetAvailableLangs(string lang);

        /// <summary>
        /// Get all avaliable translate directions.
        /// </summary>
        /// <returns></returns>
        List<Tuple<string, string>> GetTranslateDir();

        /// <summary>
        /// Translate text.
        /// </summary>
        /// <param name="sourceLang"></param>
        /// <param name="sourceText"></param>
        /// <param name="targetLanguage"></param>
        /// <returns></returns>
        string Translate(string sourceLang, string sourceText, string targetLanguage);

        /// <summary>
        /// Get all available languages.
        /// </summary>
        /// <returns>
        /// Dictionary where keys is code of language and values is a human name of language.
        /// </returns>
        Dictionary<string, string> GetLanguages();

    }
}
