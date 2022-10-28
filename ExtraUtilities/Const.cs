using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExtraUtilities
{
    public class Const
    {
        /// <summary>
        /// Возвращает список букв русского алфавита в нижнем регистре
        /// </summary>
        public static readonly List<string> RUSSIAN_ALPHABET = new() { "а", "б", "в", "г", "д", "е", "ё", "ж", "з", "и", "й", "к", "л", "м", "н", "о", "п", "р", "с", "т", "у", "ф", "х", "ц", "ч", "ш", "щ", "э", "ю", "я" };

        /// <summary>
        /// Возвращает список букв английского алфавита в нижнем регистре
        /// </summary>
        public static readonly List<string> ENGLISH_ALPHABET = new() { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };

        /// <summary>
        /// Возвращает список цифр
        /// </summary>
        public readonly static List<string> DIGITS = new() { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        /// <summary>
        /// Регулярка, выделяющая все символы, не соответствующие алфавитам русского, украинского и белорусского языка и цифрам - [^a-z0-9а-яіїєґў ]+
        /// </summary>
        public readonly static Regex NOT_ALPHANUMERIC = new ("[^a-z0-9а-яіїєґў ]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Регулярка, выделяющая двойные и более пробелы \s+
        /// </summary
        public readonly static Regex MANY_SPACES = new ("\\s+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        /// <summary>
        /// Регулярка, проверяющая, является ли строка закодированной в Base64
        /// </summary
        public readonly static Regex BASE64 = new("^[a-zA-Z0-9\\+/]*={0,3}$", RegexOptions.Compiled);

    }
}
