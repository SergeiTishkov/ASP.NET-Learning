using System;
using System.Collections.Generic;

namespace WasteProducts.Logic.Common.Models.Users
{
    // Удалим лишние проперти когда разберемся, а какие из них вообще лишние на бизнес-слое
    /// <summary>
    /// Full BL version of UserDB.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Email of the user.
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// True if the email is confirmed, default is false.
        /// </summary>
        public virtual bool EmailConfirmed { get; set; }

        /// <summary>
        /// User password.
        /// </summary>
        public virtual string Password { get; set; }

        /// <summary>
        /// A random value that should change whenever a users credentials have changed (password changed, login removed)
        /// </summary>
        public virtual string SecurityStamp { get; set; }

        /// <summary>
        /// PhoneNumber for the user.
        /// </summary>
        public virtual string PhoneNumber { get; set; }

        /// <summary>
        /// True if the phone number is confirmed, default is false.
        /// </summary>
        public virtual bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// Is two factor enabled for the user.
        /// </summary>
        public virtual bool TwoFactorEnabled { get; set; }

        /// <summary>
        /// DateTime in UTC when lockout ends, any time in the past is considered not locked out.
        /// </summary>
        public virtual DateTime? LockoutEndDateUtc { get; set; }

        /// <summary>
        /// Is lockout enabled for this userю
        /// </summary>
        public virtual bool LockoutEnabled { get; set; }

        /// <summary>
        /// Used to record failures for the purposes of lockout.
        /// </summary>
        public virtual int AccessFailedCount { get; set; }

        /// <summary>
        /// All user roles.
        /// </summary>
        public virtual ICollection<UserRole> Roles { get; }

        /// <summary>
        /// All user claims.
        /// </summary>
        public virtual ICollection<UserClaim> Claims { get; }

        /// <summary>
        /// All user logins.
        /// </summary>
        public virtual ICollection<UserLogin> Logins { get; }

        /// <summary>
        /// Unique key for the user.
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        /// Unique username.
        /// </summary>
        public virtual string UserName { get; set; }

        /// <summary>
        /// List of Users which belong to group of friends related to current User.
        /// </summary>
        public List<User> UserFriends { get; set; }

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
    }
}