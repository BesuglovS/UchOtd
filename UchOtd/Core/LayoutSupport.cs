﻿using System.Collections.Generic;

namespace UchOtd.Core
{
    public static class LayoutSupport
    {
        public static Dictionary<char, char> Match = new Dictionary<char, char>
        {
            {'q', 'й'}, {'w', 'ц'}, {'e', 'у'}, {'r', 'к'}, {'t', 'е'}, {'y', 'н'}, 
            {'u', 'г'}, {'i', 'ш'}, {'o', 'щ'}, {'p', 'з'}, {'[', 'х'}, {']', 'ъ'},
            {'a', 'ф'}, {'s', 'ы'}, {'d', 'в'}, {'f', 'а'}, {'g', 'п'}, {'h', 'р'},
            {'j', 'о'}, {'k', 'л'}, {'l', 'д'}, {';', 'ж'}, {'\'', 'э'},
            {'z', 'я'}, {'x', 'ч'}, {'c', 'с'}, {'v', 'м'}, {'b', 'и'}, {'n', 'т'},
            {'m', 'ь'}, {',', 'б'}, {'.', 'ю'}
        };

        public static string ConvertEnRu(string input)
        {
            var result = "";
            foreach (var с in input)
            {
                if (Match.ContainsKey(с))
                {
                    result += Match[с];
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
