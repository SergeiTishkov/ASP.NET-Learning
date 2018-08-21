using System;
using System.Collections.Generic;
using WasteProducts.DataAccess.Common.Models.Users;

namespace WasteProducts.DataAccess.Common.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Returns a registered user by its email.
        /// </summary>
        /// <param name="email">Email of the requested user.</param>
        /// <returns></returns>
        UserDB Select(string email);

        /// <summary>
        /// Returns a registered user by its email and password.
        /// </summary>
        /// <param name="email">Email of the requested user.</param>
        /// <param name="password">Password of the requested user.</param>
        /// <returns>User with the specific email and password.</returns>
        UserDB Select(string email, string password);

        /// <summary>
        /// Returns all registered users in an enumerable.
        /// </summary>
        /// <returns>All the registered users.</returns>
        IEnumerable<UserDB> SelectAll();

        // не знаю пока, как надо, такое сработало бы на EF или ADO.NET если все запросить, и потом предикатом
        // но это ведь глупость, лучше сразу в селект передать запрос
        // скажем так, это заготовка на будущее, потом изменится на нормальный код
        /// <summary>
        /// Returns the users filtered by the predicate.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<UserDB> SelectWhere(Func<UserDB, bool> predicate); 

        /// <summary>
        /// Adds new registered user in the repository.
        /// </summary>
        /// <param name="user">New registered user to add.</param>
        void Add(UserDB user);

        /// <summary>
        /// Updates the record of the specific user.
        /// </summary>
        /// <param name="user">Specific user to update.</param>
        void Update(UserDB user);

        /// <summary>
        /// Deletes the record of the specific user.
        /// </summary>
        /// <param name="user">Specific user to delete.</param>
        void Delete(UserDB user);
    }
}