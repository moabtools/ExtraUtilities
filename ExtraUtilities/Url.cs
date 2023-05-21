using System;
using System.Globalization;

namespace ExtraUtilities
{
    public static class Url
    {
        /// <summary>
        /// Проверяет, является ли URI закодированным в Punycode (IDN)
        /// </summary>
        /// <param name="uri">Исходный URL</param>
        public static bool IsPunycode(Uri uri)
        {
            return uri.Host.StartsWith("xn--");
        }

        /// <summary>
        /// Конвертирует Uri из Punycode в читабельный формат
        /// </summary>
        /// <param name="uri">Исходный URL</param>
        public static Uri ConvertFromPunycode(this Uri uri)
        {
            if (!IsPunycode(uri))
                throw new ArgumentException("Uri is not in Punycode");

            IdnMapping idnMapping = new();

            UriBuilder uriBuilder = new(uri)
            {
                Host = idnMapping.GetUnicode(uri.Host)
            };

            return uriBuilder.Uri;
        }
    }
}
