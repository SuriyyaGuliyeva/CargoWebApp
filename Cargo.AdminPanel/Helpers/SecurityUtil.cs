using System.Security.Cryptography;
using System.Text;

namespace Cargo.AdminPanel.Helpers
{
    public static class SecurityUtil
    {
        public static string CalculateHash(byte[] bytes)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(bytes);

                var stringBuilder = new StringBuilder();

                foreach (byte b in hash)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }

                return stringBuilder.ToString();
            }
        }
    }
}
