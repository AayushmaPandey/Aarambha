using System;
using System.Security.Cryptography;
using System.Text;

namespace Aarambha.Helpers
{
    public static class PasswordHelper
    {
        public static string GenerateSalt()
        {
            var bytes = new byte[16]; // 16 bytes = 32 hex chars
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(bytes);
            }
            return BytesToHex(bytes);
        }

        public static string HashPassword(string password, string salt)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (salt == null) throw new ArgumentNullException(nameof(salt));

            using (var sha = SHA256.Create())
            {
                var input = Encoding.UTF8.GetBytes(password + salt);
                var hash = sha.ComputeHash(input);
                return BytesToHex(hash); // 64 hex chars
            }
        }

        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            var hash = HashPassword(password, storedSalt);
            return string.Equals(hash, storedHash, StringComparison.OrdinalIgnoreCase);
        }

        private static string BytesToHex(byte[] bytes)
        {
            var sb = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes)
                sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    }
}
