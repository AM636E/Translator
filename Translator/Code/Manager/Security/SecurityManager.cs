using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

using TranslatorDataAccess;
using  Translator.Domain;

using Translator.Code.Helper;
using Translator.Code.Exception;

namespace Translator.Code.Manager.Security
{
    public class SecurityManager : ISecurityManager
    {
        private readonly IUserRepository _repository;

        public SecurityManager(IUserRepository repository)
        {
            this._repository = repository;
        }

        public void RegisterUser(User user)
        {
            if (!_repository.DoesUserExists(user.Login))
            {
                user.Password = PasswordHelper.GetHash(user.Password);
                _repository.InsertUser(user);
                FormsAuthentication.SetAuthCookie(user.Login, true);
            }
            else
            {
                throw new DuplicateUserException(user.Login);
            }
        }

        public void AuthenticateUser(User user)
        {
            if (_repository.DoesUserExists(user.Login, PasswordHelper.GetHash(user.Password)))
            {
                FormsAuthentication.SetAuthCookie(user.Login, true);
            }
        }

        public User GetCurrentLoginUser()
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return _repository.GetByLogin(HttpContext.Current.User.Identity.Name);
            }
            return null;
        }
    }
}