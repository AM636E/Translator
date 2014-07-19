using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using TranslatorEntities = Translator.Domain;

namespace TranslatorDataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }
        public List<TranslatorEntities.User> GetAll()
        {
            using (DbContext context = new DbContext(this._connectionString))
            {
                return context.Set<TranslatorEntities.User>().ToList();
            }
        }

        public TranslatorEntities.User GetById(int id)
        {
            using (TranslatorEntities.TranslatorEntities context = new TranslatorEntities.TranslatorEntities())
            {
                return context.Users.Find(id);
            }
        }

        public void InsertUser(TranslatorEntities.User user)
        {
            using (TranslatorEntities.TranslatorEntities context = new TranslatorEntities.TranslatorEntities())
            {
                //  context.Users.Attach(user);
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        public void UpdateUser(TranslatorEntities.User user)
        {
            using (TranslatorEntities.TranslatorEntities context = new TranslatorEntities.TranslatorEntities())
            {
                try
                {
                    context.Users.Attach(user);
                    var entry = context.Entry(user);
                    entry.Property("FirstName").IsModified = true;
                    entry.Property("SurName").IsModified = true;
                    entry.Property("Email").IsModified = true;
                    entry.Property("Pass").IsModified = true;
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var validationErrors in ex.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceWarning("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }
        }

        public void DeleteUser(TranslatorEntities.User user)
        {
            using (TranslatorEntities.TranslatorEntities context = new TranslatorEntities.TranslatorEntities())
            {
                context.Users.Attach(user);
                context.HistoryRecords.RemoveRange(user.HistoryRecords);
                context.Users.Remove(user);
                context.SaveChanges();
            }
        }

        public int GetUsersCount()
        {
            return this.GetAll().Count;
        }

        public bool DoesUserExists(string username)
        {
            using (TranslatorEntities.TranslatorEntities context = new TranslatorEntities.TranslatorEntities())
            {
                return context.Users.Any(r => r.Login == username);
            }
        }

        public string GetFirstName(string username)
        {
            return this.GetByLogin(username).Login;
        }

        public TranslatorEntities.User GetByLogin(string login)
        {
            using (TranslatorEntities.TranslatorEntities context = new TranslatorEntities.TranslatorEntities())
            {
                return context.Users.Where(e => e.Login == login).First();
            }
        }


        public bool DoesUserExists(string username, string password)
        {
            return this.GetAll().Any(e => e.Login == username && e.Password == password);
        }
    }
}
