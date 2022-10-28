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
    }
}
