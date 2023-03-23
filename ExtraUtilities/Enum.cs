using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace ExtraUtilities
{
    public static class Enums
    {
        /// <summary>
        /// Получает следующее за текущим значение перечисления. Если текущее значение последнее - вернется первое значение Enum[0].
        /// </summary>
        /// <typeparam name="T">Перечисление</typeparam>
        /// <param name="value">Текущее значение перечисления</param>
        /// <returns>Следующее за текущим значение перечисления</returns>
        public static T Next<T>(this T value) where T : Enum
        {
            T[] Arr = (T[])Enum.GetValues(value.GetType());
            int j = Array.IndexOf<T>(Arr, value) + 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }

        /// <summary>
        /// Получает значение name атрибута [Display(Name="...")] значения перечисления
        /// </summary>
        /// <param name="value">Значение перечисления</param>
        /// <returns>Значение name атрибута [Display(Name="...")] значения перечисления</returns>
        public static string GetDisplayName(this Enum value)
        {
            return value.GetType()
                            .GetMember(value.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()!
                            .GetName()!;
        }

        /// <summary>
        /// Получает значение name атрибута [Display(ShortName="...")] значения перечисления
        /// </summary>
        /// <param name="value">Значение перечисления</param>
        /// <returns>Значение name атрибута [Display(ShortName="...")] значения перечисления</returns>
        public static string GetDisplayShortName(this Enum value)
        {
            return value.GetType()
                            .GetMember(value.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()!
                            .GetShortName()!;
        }
    }
}