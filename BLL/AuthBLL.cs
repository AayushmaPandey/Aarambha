using System;
using Aarambha.Data_Access_Layer;
using Aarambha.Helpers;
using Aarambha.Models;

namespace Aarambha.BLL
{
    public class AuthBLL
    {
        private readonly UserDAL _userDal = new UserDAL();

        public User Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ValidationException("Username is required.");
            if (string.IsNullOrWhiteSpace(password)) throw new ValidationException("Password is required.");

            var user = _userDal.GetByUsername(username);
            if (user == null) throw new ValidationException("Invalid username or password.");
            if (!user.IsActive) throw new ValidationException("Account is disabled. Contact administrator.");

            var ok = PasswordHelper.VerifyPassword(password, user.PasswordHash, user.PasswordSalt);
            if (!ok) throw new ValidationException("Invalid username or password.");

            // Update last login (simple DAL Update call)
            user.LastLoginAt = DateTime.UtcNow;
            _userDal.Update(user);

            return user;
        }
    }
}
