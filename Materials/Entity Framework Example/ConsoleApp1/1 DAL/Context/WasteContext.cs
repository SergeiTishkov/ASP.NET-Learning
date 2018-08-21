using System.Data.Entity;
using System.Diagnostics;
using Microsoft.AspNet.Identity.EntityFramework;
using WasteProducts.DataAccess.Common.Models.Users;
//using WasteProducts.DataAccess.Contexts.Config;

namespace WasteProducts.DataAccess.Contexts
{
    //[DbConfigurationType(typeof(MsSqlConfiguration))]
    // todo delete this comment after work with userDB is done
    // public class WasteContext : IdentityDbContext; IdentityUser 
    public class WasteContext : IdentityDbContext<UserDB, IdentityRole, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        // TODO delete : base after testing and clean up the App.Config
        public WasteContext() : base(@"name=ConsoleApp1.Properties.Settings.ConStrByServer")
        {
            Database.Log = (s) => Debug.WriteLine(s);
        }
    }
}