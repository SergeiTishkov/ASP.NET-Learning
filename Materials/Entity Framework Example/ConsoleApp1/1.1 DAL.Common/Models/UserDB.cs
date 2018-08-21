using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WasteProducts.DataAccess.Common.Models.Users
{
    public class UserDB : IdentityUser
    {
        /// <summary>
        /// List of Users which belong to group of friends related to current User.
        /// </summary>
        public List<UserDB> UserFriends { get; set; }

        // TODO decomment after the "Product" model is enabled
        /// <summary>
        /// List of Products which User have ever captured.
        /// </summary>
        //public List<Product> UserProducts { get; set; }

        // TODO decomment after the "Groups" model is enabled
        /// <summary>
        /// List of all Groups to which current User is assigned.
        /// </summary>
        //public List<Group> GroupMembership { get; set; }

        /// <summary>
        /// Specifies timestamp of creation of concrete User in Database.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Specifies timestamp of modifying of any Property of User in Database.
        /// </summary>
        public DateTime? Modified { get; set; }
    }
}
