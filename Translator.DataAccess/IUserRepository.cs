using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TranslatorEntities = Translator.Domain;
namespace TranslatorDataAccess
{
    public interface IUserRepository
    {
        List<TranslatorEntities.User> GetAll();
        TranslatorEntities.User GetById(int id);

        void InsertUser(TranslatorEntities.User user);

        void UpdateUser(TranslatorEntities.User user);
        void DeleteUser(TranslatorEntities.User user);
        int GetUsersCount();
        bool DoesUserExists(string username);

        bool DoesUserExists(string username, string password);

        string GetFirstName(string username);

        TranslatorEntities.User GetByLogin(string login);
    }
}
