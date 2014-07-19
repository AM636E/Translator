using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Translator.Code.Service.Translator;
using Translator.Code.Manager.Security;
using TranslatorDataAccess;
using  Translator.Domain;

namespace Translator.Controllers
{
    public class TranslateController : Controller
    {
        private readonly IHistoryRecordRepository _historyRepository;
        private readonly ITranslatorService _translatorService;
        private readonly ISecurityManager _securityManager;
        private readonly User _user;
        public TranslateController(ITranslatorService translatorService, IHistoryRecordRepository repository, ISecurityManager manager)
        {
            this._translatorService = translatorService;
            this._historyRepository = repository;
            this._securityManager = manager;
            this._user = _securityManager.GetCurrentLoginUser();
        }

        // GET: Translate
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                ViewBag.SelectedLang = "";
                if (System.Web.HttpContext.Current.Request.Cookies["SelectedLang"] != null)
                {
                    ViewBag.SelectedLang = System.Web.HttpContext.Current.Request.Cookies["SelectedLang"].Value;
                }
                ViewBag.Languages = this._translatorService.GetLanguages();
                if (_user != null)
                {
                    ViewBag.Records = _historyRepository.GetByUser(_user.UserId);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Errors = new List<string>();
                ViewBag.Errors.Add(ex.Message);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(string txtTranslateText, string translateFrom, string translateTo)
        {
            try
            {
                HttpCookie cookie = new HttpCookie("SelectedLang", translateTo);
                cookie.Expires = DateTime.Now.AddDays(10);
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                if (translateFrom.Equals("detect"))
                {
                    //translateFrom = _translatorService.DetectLang()
                    translateFrom = "en";
                }
                if (translateTo == null)
                {
                    translateTo = "uk";
                }
                string translated = _translatorService.Translate("", txtTranslateText, translateTo);
                ViewBag.Text = translated;
                if (_user != null)
                {
                    _historyRepository.AddRecord(new HistoryRecord()
                        {
                            SourceLanguage = translateFrom,
                            SourceText = txtTranslateText,
                            DestinationLanguage = translateTo,
                            DestinationText = translated,
                            UserId = _user.UserId
                        });
                    ViewBag.Records = _historyRepository.GetByUser(_user.UserId);
                }

                if (Request.IsAjaxRequest())
                {
                    return PartialView("_Result");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Errors = new List<string>();
                ViewBag.Errors.Add(ex.Message);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Countries(string countryFrom)
        {
            throw new NotImplementedException();
        }
    }
}