using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TranslatorDataAccess;
using TranslatorEntities = Translator.Domain;

namespace Translator.Controllers
{
    [Authorize(Roles="admin")]
    public class AdminController : Controller
    {
        private readonly IUserRepository _userRepository;


        public AdminController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Users()
        {
            ViewBag.Users = _userRepository.GetAll();

            return View();
        }

        public ActionResult Delete(int id)
        {
            TranslatorEntities.User user = _userRepository.GetById(id);
            _userRepository.DeleteUser(user);

            return new RedirectResult("/admin/users");
        }
    }
}