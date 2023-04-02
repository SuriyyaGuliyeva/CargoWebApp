using System.Security.Cryptography;
using System.Text;

namespace Cargo.AdminPanel.Helpers
{
    public static class GetHashCodeImage
    {
        public static string GetImageHashCode(string imagePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = System.IO.File.OpenRead(imagePath))
                {
                    var hash = md5.ComputeHash(stream);

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
}
