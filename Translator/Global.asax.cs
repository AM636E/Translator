using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Security.Principal;

using Unity.Mvc5;
using Microsoft.Practices.Unity;

namespace Translator
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private IUnityContainer _container = new UnityContainer();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            UnityConfig.RegisterComponents();
        }

        protected void Application_AuthenticateRequest()
        {
            if(User != null)
            {
                var username = User.Identity.Name;
                GenericIdentity identity = new GenericIdentity(username);
                GenericPrincipal principal = new GenericPrincipal(identity, GetUserRoles(username));
                HttpContext.Current.User = principal;
                System.Web.Security.FormsAuthentication.SetAuthCookie(username, true);
            }
        }

        private string[] GetUserRoles(string username)
        {
            List<string> roles = new List<string>() { "user" };
            if(username == "admin")
            {
                roles.Add("admin");
            }

            return roles.ToArray();
        }
    }
}
