using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtraUtilities
{
    public static class String
    {
        /// <summary>
        /// Преобразует первый символ строки в нижний регистр
        /// </summary>
        /// <param name="str">Исходная строка</param>
        /// <returns>Строка с преобразованным в нижний регистр первым символом</returns>
        public static string FirstCharToLowerCase(this string str)
        {
            if (string.IsNullOrEmpty(str) || char.IsLower(str[0]))
                return str;

            return char.ToLower(str[0]) + str.Substring(1);
        }

        /// <summary>
        /// Преобразует первый символ строки в верхний регистр
        /// </summary>
        /// <param name="str">Исходная строка</param>
        /// <returns>Строка с преобразованным в верхний регистр первым символом</returns>
        public static string FirstCharToUpperCase(this string str)
        {
            if (string.IsNullOrEmpty(str) || char.IsUpper(str[0]))
                return str;

            return char.ToUpper(str[0]) + str.Substring(1);
        }

        /// <summary>
        /// Очищает строку от символов, не входящих в русский, украинский и белорусский алфавиты, и цифр; после этого заменяет двойные и более пробелы на один.
        /// </summary>
        /// <param name="source">Исходная строка</param>
        /// <returns>Очищенная строка</returns>
        public static string Clean(string source)
        {
            string words = Const.NOT_ALPHANUMERIC.Replace(source.ToLower(), " ");
            words = Const.MANY_SPACES.Replace(words.Trim(), " ");
            return words;
        }

        /// <summary>
        /// Удаляет слэш, стоящий в конце строки (url)
        /// </summary>
        /// <param name="url">Исходная строка</param>
        /// <returns>Строка без слэша в конце</returns>
        public static string RemoveTrainingSlash(this string url)
        {
            if (url.EndsWith("/"))
                return url.Substring(0, url.Length - 1);
            else
                return url;
        }

        /// <summary>
        /// Генерирует пароль, включающий символы английского алфавита в разном регистре, цифры и некоторые спецсимволы (не требующие экранирования в большинстве случаев): @, #, $, %
        /// </summary>
        /// <param name="lengthFrom">Минимальная длина пароля, по умолчанию 8 символов</param>
        /// <param name="lengthTo">Максимальная длина пароля, по умолчанию 16 символов</param>
        /// <returns>Сгенерированный пароль</returns>
        public static string GeneratePassword(int lengthFrom = 8, int lengthTo = 16)
        {
            string[] symbols = new[] { "q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "a", "s", "d", "f", "g", "h", "j", "k", "l", "z", "x", "c", "v", "b", "n", "m", "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P", "A", "S", "D", "F", "G", "H", "J", "K", "L", "Z", "X", "C", "V", "B", "N", "M", "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "@", "#", "$", "%", "@", "#", "$", "%", "@", "#", "$", "%"};
            Random rnd = new ();

            int length = rnd.Next(lengthFrom, lengthTo);

            string password = "";

            for (int i = 0; i < length; i++)
                password += symbols[rnd.Next(0, symbols.Length)];

            return password;
        }

        /// <summary>
        /// Декодирует исходную строку, закодированную ранее в Base64
        /// </summary>
        /// <param name="input">Исходная строка</param>
        /// <returns>Декодированная строка</returns>
        public static string Base64Decode(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) 
                return string.Empty;
            byte[] bytes = Convert.FromBase64String(input);
            return Encoding.UTF8.GetString(bytes);
        }

        /// <summary>
        /// Кодирует исходную строку в Base64
        /// </summary>
        /// <param name="input">Исходная строка</param>
        /// <returns>Закодированная в Base64 строка</returns>
        public static string Base64Encode(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;
            byte [] bytes = Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Проверяет, закодирована ли исходная строка в Base64
        /// </summary>
        /// <param name="input">Исходная строка</param>
        /// <returns>true - строка закодирована в Base64, false - нет</returns>
        public static bool IsBase64(string input)
        {
            return (input.Length % 4 == 0) && Const.BASE64.IsMatch(input);
        }

        /// <summary>
        /// Проверяет, содержит ли строка любой из элементов массива строк
        /// </summary>
        /// <param name="input">Исходная строка</param>
        /// <param name="values">Массив исходных строк</param>
        public static bool ContainsAny(this string str, string[] values, StringComparison comparisonType = StringComparison.Ordinal)
        {
            if (!string.IsNullOrEmpty(str) || values.Length > 0)
            {
                foreach (string value in values)
                {
                    if (str.Contains(value, comparisonType))
                        return true;
                }
            }

            return false;
        }

    }
}
