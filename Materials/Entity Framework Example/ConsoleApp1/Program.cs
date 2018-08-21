using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WasteProducts.DataAccess.Common.Models.Users;
using WasteProducts.DataAccess.Contexts;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkWithEF();

            Console.ReadKey(true);
        }

        public static void WorkWithEF()
        {
            using (WasteContext dbContext = new WasteContext())
            {
                var userDB = new UserDB()
                {
                    UserName = "Sergei",
                    Email = "umanetto@mail.ru",
                    EmailConfirmed = true,
                    PasswordHash = "qwerty",
                    SecurityStamp = "Stamp",
                    PhoneNumber = "+375255024800",
                    PhoneNumberConfirmed = true,
                    TwoFactorEnabled = false,
                    LockoutEndDateUtc = null,
                    Created = DateTime.UtcNow,
                    Modified = null
                };
                dbContext.Users.Add(userDB);
                dbContext.SaveChanges();
                Console.WriteLine("Done!");
            }
        }
    }
}
