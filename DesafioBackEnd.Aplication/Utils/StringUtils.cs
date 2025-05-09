using System.Security.Cryptography;
using System.Text;

namespace StockManager.Aplication.Utils
{
    public static class StringUtils
    {
        public static string GetMD5Hash(string pass)
        {
            StringBuilder sb = new StringBuilder();
            using (MD5 md5 = MD5.Create())
            {
                byte[] stringByte = Encoding.UTF8.GetBytes(pass);
                byte[] hashBystes = md5.ComputeHash(stringByte);
                return Convert.ToHexString(hashBystes);
            }
        }
        public static string GetBcryptHash(string pass)
        {
            return BCrypt.Net.BCrypt.HashPassword(pass, BCrypt.Net.BCrypt.GenerateSalt(10));
        }
        public static bool VerifyBcryptHash(string pass, string hash)
        {
            var verify = BCrypt.Net.BCrypt.Verify(pass, hash); // BCrypt.Verify()
            return verify;
        }
    }
}
