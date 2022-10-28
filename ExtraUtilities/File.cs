using System.IO;
using System.Text.RegularExpressions;

namespace ExtraUtilities
{
    public class File
    {
        /// <summary>
        /// Возвращает имя файла, очищенное от неподдерживаемых символов и специальных наименований, таких как CON, PRN, LPT и пр.
        /// </summary>
        /// <param name="filename">Имя файла</param>
        /// <returns>Имя файла, очищенное от неподдерживаемых символов и специальных наименований</returns>
        public static string SanitizeFileName(string filename)
        {
            var invalidChars = Regex.Escape(new string(Path.GetInvalidFileNameChars()));
            var invalidReStr = string.Format(@"[{0}]+", invalidChars);

            var reservedWords = new[]
            {
                "CON", "PRN", "AUX", "CLOCK$", "NUL", "COM0", "COM1", "COM2", "COM3", "COM4",
                "COM5", "COM6", "COM7", "COM8", "COM9", "LPT0", "LPT1", "LPT2", "LPT3", "LPT4",
                "LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
            };

            var sanitisedNamePart = Regex.Replace(filename, invalidReStr, "_");
            foreach (var reservedWord in reservedWords)
            {
                var reservedWordPattern = string.Format("^{0}\\.", reservedWord);
                sanitisedNamePart = Regex.Replace(sanitisedNamePart, reservedWordPattern, "_.", RegexOptions.IgnoreCase);
            }

            if (sanitisedNamePart.Length > 60)
                sanitisedNamePart = sanitisedNamePart[..60];

            return sanitisedNamePart;
        }
    }
}
