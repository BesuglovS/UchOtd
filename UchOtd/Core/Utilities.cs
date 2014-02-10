using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UchOtd.Core
{
    public static class Utilities
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>
            (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }

        public static bool MainGroups(string groupName)
        {
            if (!groupName.Contains('I') && 
                !groupName.Contains('-') && 
                !groupName.Contains('+') && 
                !groupName.Contains(".)"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
