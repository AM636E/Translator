using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity.Validation;

using TranslatorDataAccess;
using Translator.Domain;

using Translator.Code.Helper;
using Translator.Code.Manager.Security;

namespace Translator.Controllers
{
    [HandleError]
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ISecurityManager _securityManager;
        public AccountController(IUserRepository userRepository, ISecurityManager manager)
        {
            _userRepository = userRepository;
            _securityManager = manager;
        }

        

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string userName, string password)
        {
            _securityManager.AuthenticateUser(new User()
                {
                    Login = userName,
                    Password = password
                });
            

            return new RedirectResult("/");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(string userLogin, string userEmail, string userPassword)
        {
            ViewBag.Errors = new List<string>();
            ViewBag.Messages = new List<string>();
            try
            {
                _securityManager.RegisterUser(new User()
                {
                    Login = userLogin,
                    Password = userPassword,
                    Email = userEmail
                });

                ViewBag.Messages.Add("User created");

                return new RedirectResult("/");
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var errors in ex.EntityValidationErrors)
                {
                    foreach (var error in errors.ValidationErrors)
                    {
                        ViewBag.Errors.Add(error.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Errors.Add(ex.Message);
                ViewBag.Errors.Add((ex.InnerException ?? new Exception()).Message);
            }
           return View();
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();

            return new RedirectResult("/");
        }
        /*
        protected override void OnException(ExceptionContext filterContext)
        {
            var view = filterContext.ParentActionViewContext;
            view.ViewBag.Errors = new List<string>()
            {
                filterContext.Exception.Message
            };
            filterContext.Result = View(view);
        }*/
    }
}