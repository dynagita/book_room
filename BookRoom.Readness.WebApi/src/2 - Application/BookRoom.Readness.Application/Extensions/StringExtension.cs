using System.Security.Cryptography;
using System.Text;

namespace BookRoom.Readness.Application.Extensions
{
    public static class StringExtension
    {
        public static string ComputeHash(this string text)
        {
            string hash = string.Empty;

            var textBytes = Encoding.UTF8.GetBytes(text);
            using(SHA256 _256 = SHA256.Create())
            {
                textBytes = _256.ComputeHash(textBytes);
            }

            hash = Encoding.UTF8.GetString(textBytes);

            return hash;
        }
    }
}
