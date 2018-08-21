using System;
using System.Collections.Generic;
using System.Linq;
using WasteProducts.DataAccess.Common.Models.Users;
using WasteProducts.DataAccess.Common.Repositories;
using WasteProducts.DataAccess.Contexts;

namespace WasteProducts.DataAccess.Repositories
{
    class UserRepository : IUserRepository
    {
        public void Add(UserDB user)
        {
            if (user.Id != null)
                throw new ArgumentException("Cannot Add User with Id different from null.");

            user.Created = DateTime.UtcNow;
            using (var db = new WasteContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public void Delete(UserDB user)
        {
            if (user.Id != null)
            {
                using (var db = new WasteContext())
                {
                    var result = db.Users.Where(f => f.Id == user.Id).FirstOrDefault();

                    if (result != null)
                    {
                        db.Users.Remove(result);
                        db.SaveChanges();
                    }
                    else
                    {
                        throw new ArgumentException($"The User cannot be deleted because User with Id = {user.Id} doesn't exist.");
                    }
                }
            }
            else
            {
                throw new ArgumentException("Cannot delete User with Id = null.");
            }
        }

        public UserDB Select(string email, string password)
        {
            using (var db = new WasteContext())
            {
                return db.Users.Where(user => user.Email == email && user.PasswordHash == password).FirstOrDefault();
            }
        }

        public UserDB Select(string email)
        {
            using (var db = new WasteContext())
            {
                return db.Users.Where(user => user.Email == email).FirstOrDefault();
            }
        }

        public IEnumerable<UserDB> SelectAll()
        {
            using (var db = new WasteContext())
            {
                return db.Users.ToList();
            }
        }

        public IEnumerable<UserDB> SelectWhere(Func<UserDB, bool> predicate)
        {
            using (var db = new WasteContext())
            {
                return db.Users.Where(predicate);
            }
        }

        public void Update(UserDB user)
        {
            using (var db = new WasteContext())
            {
                var userInDB = db.Users.Find(user.Id);

                user.Created = userInDB.Created;

                db.Entry(userInDB).CurrentValues.SetValues(user);

                userInDB.Modified = DateTime.UtcNow;

                db.SaveChanges();
            }
        }
    }
}
