using System.Security.Cryptography;
using System.Text;

namespace ExtraUtilities
{
    public class Crypto
    {
        /// <summary>
        /// Вычилсяет хэш SHA256 исходной строки
        /// </summary>
        /// <param name="rawData">Исходная строка</param>
        /// <returns>Хэш исходной строки</returns>
        public static string ComputeSha256Hash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new ();
                for (int i = 0; i < bytes.Length; i++)
                    builder.Append(bytes[i].ToString("x2"));

                return builder.ToString();
            }
        }

        /// <summary>
        /// Вычисляет хэш MD5 исходной строки
        /// </summary>
        /// <param name="input">Исходная строка</param>
        /// <returns>Хэш MD5 исходной строки</returns>
        public static string ComputeMD5Hash(string input)
        {
            using (MD5 sha256Hash = MD5.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new ();
                for (int i = 0; i < bytes.Length; i++)
                    builder.Append(bytes[i].ToString("x2"));

                return builder.ToString();
            }
        }
    }
}
