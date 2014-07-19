using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Translator.Domain;


namespace Translator.Code.Manager.Security
{
    public interface ISecurityManager
    {
        void RegisterUser(User user);
        void AuthenticateUser(User user);
        User GetCurrentLoginUser();
    }
}
