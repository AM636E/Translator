using Microsoft.Practices.Unity;
using Translator.Code.Service.Translator;
using Translator.Code.Service.Translator.YandexTranslator;
using Translator.Code.Service.Cache.Default;
using TranslatorDataAccess;
using TranslatorEntities = Translator.Domain;
using Translator.Code.Manager.Security;
using System.Configuration;
namespace Translator
{
    public class ContainerBootstrapper
    {
        
        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<Translator.Controllers.TranslateController>();
            string baseUrl = System.Configuration.ConfigurationManager.AppSettings["TranslatorBaseUrl"];
            string apiKey = System.Configuration.ConfigurationManager.AppSettings["TranslatorApiKey"];            
            ITranslatorService yandexService = new YandexTranslatorService(baseUrl, apiKey);
            ITranslatorService service = new CacheableTranslatorService(yandexService, new DefaultCacheService());
            container.RegisterInstance<ITranslatorService>(service);

            string connectionString = ConfigurationManager.ConnectionStrings["TranslatorEntities"].ConnectionString;
            container.RegisterInstance<IUserRepository>(new UserRepository(connectionString));
            container.RegisterInstance<IHistoryRecordRepository>(new HistoryRecordRepository(connectionString));

            container.RegisterType<ISecurityManager, SecurityManager>();
        }
    }
}