using WasteProducts.Logic.Common.Models.Users;

namespace WasteProducts.Logic.Common.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Tries to register a new user with a specific parameters and returns whether registration succeed or not.
        /// </summary>
        /// <param name="email">Email of the new user.</param>
        /// <param name="password">Password of the new user.</param>
        /// <param name="passwordConfirmation">Confirmation of the password, must be the same as the password.</param>
        /// <param name="registeredUser">New registered user.</param>
        /// <returns>Boolean representing whether registration succeed or not.</returns>
        bool Register(string email, string password, string passwordConfirmation, out User registeredUser);

        /// <summary>
        /// Tries to login as a user with the specific email and password and returns whether logging in succeed or not.
        /// </summary>
        /// <param name="email">Email of the logging in user.</param>
        /// <param name="password">Password of the logging in user.</param>
        /// <param name="loggedInUser">When this methods returns, this variable contains a User object if this method succeed, or null if method failed.</param>
        /// <returns>Boolean representing whether logging in succeed or not.</returns>
        bool LogIn(string email, string password, out User loggedInUser);

        /// <summary>
        /// Tries to reset a password of the specific user to the new password and returns whether resetting succeed or not.
        /// </summary>
        /// <param name="user">The specific user to change its password.</param>
        /// <param name="oldPassword">Old password of the specific user.</param>
        /// <param name="newPassword">New password of the specific user.</param>
        /// <param name="newPasswordConfirmation">Confirmation of the new password, must be the same as the newPassword.</param>
        /// <returns>Boolean representing whether resetting password succeed or not.</returns>
        bool ResetPassword(User user, string oldPassword, string newPassword, string newPasswordConfirmation);

        /// <summary>
        /// Requests an email with the password of the user registered to this email.
        /// </summary>
        /// <param name="email">Email of the user forgotten its password.</param>
        /// <returns>Boolean representing whether email was correct or not.</returns>
        bool PasswordRequest(string email);

        /// <summary>
        /// Updates the specific user in the Database.
        /// </summary>
        /// <param name="user">The specific user to update.</param>
        /// <returns>Boolean representing whether updating the user was correct or not.</returns>
        void UpdateUserInfo(User user);

        /// <summary>
        /// Adds a specific new friend to the specific user's friend list.
        /// </summary>
        /// <param name="user">Friend list of this user will be expanded by the newFriend user.</param>
        /// <param name="newFriend">New friend to add to the user's friend list.</param>
        void AddFriend(User user, User newFriend);

        /// <summary>
        /// Deletes a specific friend from the specific user's friend list.
        /// </summary>
        /// <param name="user">From the friend list of this user will be deleted the deletingFriend user.</param>
        /// <param name="deletingFriend">Specific friend to delete from the user' sfriend list.</param>
        void DeleteFriend(User user, User deletingFriend);

        // TODO USER MANAGEMENT PENDING FUNCTIONAL TO ADD:
        // sharing my products with my friends after model "Product" is created
        // subscribing special users to watch their news (if this functional will be approved)
        // chatting between users
        // registering by Facebook and VK profiles
        // getting "Approved Representative of The Company" status and its unique functional like special tools for speed feedback

    }
}
