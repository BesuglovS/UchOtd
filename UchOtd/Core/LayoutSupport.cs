using System.Collections.Generic;

namespace UchOtd.Core
{
    public static class LayoutSupport
    {
        public static Dictionary<char, char> match = new Dictionary<char, char>
        {
            {'q', 'й'}, {'w', 'ц'}, {'e', 'у'}, {'r', 'к'}, {'t', 'е'}, {'y', 'н'}, 
            {'u', 'г'}, {'i', 'ш'}, {'o', 'щ'}, {'p', 'з'}, {'[', 'х'}, {']', 'ъ'},
            {'a', 'ф'}, {'s', 'ы'}, {'d', 'в'}, {'f', 'а'}, {'g', 'п'}, {'h', 'р'},
            {'j', 'о'}, {'k', 'л'}, {'l', 'д'}, {';', 'ж'}, {'\'', 'э'},
            {'z', 'я'}, {'x', 'ч'}, {'c', 'с'}, {'v', 'м'}, {'b', 'и'}, {'n', 'т'},
            {'m', 'ь'}, {',', 'б'}, {'.', 'ю'}
        };

        public static string ConvertEnRU(string input)
        {
            var result = "";
            foreach (var с in input)
            {
                if (match.ContainsKey(с))
                {
                    result += match[с];
                }
                else
                {
                    result += с;
                }
            }

            return result;
        }
    }
}
