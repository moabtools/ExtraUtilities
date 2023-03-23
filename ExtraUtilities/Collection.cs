using System;
using System.Collections.Generic;
using System.Linq;

namespace ExtraUtilities
{
    public static class Collection
    {
        /// <summary>
        /// Сравнивает, эквивалентны ли два списка по содержимому между собой
        /// </summary>
        public static bool EquivalentTo<T>(this List<T> list, List<T> other)
        {
            if (list.Except(other).Any())
                return false;
            if (other.Except(list).Any())
                return false;
            return true;
        }

        /// <summary>
        /// Находи индекс объекта в массиве объектов
        /// </summary>
        public static int IndexOf<T>(this IEnumerable<T> source, T value)
        {
            int index = 0;
            var comparer = EqualityComparer<T>.Default; // or pass in as a parameter
            foreach (T item in source)
            {
                if (comparer.Equals(item, value)) return index;
                index++;
            }
            return -1;
        }

        /// <summary>
        /// Получает следующий элемент за указанным в массиве объектов
        /// </summary>
        public static T GetNextElement<T>(this IEnumerable<T> strArray, T element)
        {
            int index = strArray.IndexOf(element);

            if ((index > strArray.Count() - 1) || (index < 0))
                throw new Exception("Invalid index");

            else if (index == strArray.Count() - 1)
                index = 0;
            else
                index++;

            return strArray.ElementAt(index);
        }
    }
}
