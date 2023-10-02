﻿using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ExtraUtilities
{
    public class Files
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

        /// <summary>
        /// Нормализация пути для сравнения
        /// </summary>
        /// <param name="path">Путь</param>
        /// <returns></returns>
        public static string NormalizePath(string path)
        {
            return Path.GetFullPath(new Uri(path).LocalPath)
                       .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                       .ToUpperInvariant();
        }

        /// <summary>
        /// Копирование папки с вложенными папками и файлами
        /// </summary>
        /// <param name="sourcePath">Откуда копировать</param>
        /// <param name="targetPath">Куда копировать</param>
        /// <param name="overwrite">Перезаписывать существующие файлы</param>
        public static void CopyFilesRecursively(string sourcePath, string targetPath, bool overwrite = true, string[] excludePaths = null!)
        {
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                if (excludePaths.Any(x => NormalizePath(dirPath).StartsWith(NormalizePath(x))))
                    continue;
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                if (excludePaths.Any(x => NormalizePath(newPath).StartsWith(NormalizePath(x))))
                    continue;
                System.IO.File.Copy(newPath, newPath.Replace(sourcePath, targetPath), overwrite);
            }
        }

        /// <summary>
        /// Получает расширение файла из URL
        /// </summary>
        /// <param name="url">URL, расширение файла из которого нужно получить</param>
        /// <returns></returns>
        public static string GetFileExtensionFromUrl(string url)
        {
            url = url.Split('?')[0];
            url = url.Split('/').Last();
            return url.Contains('.') ? url.Substring(url.LastIndexOf('.')) : "";
        }
    }
}
