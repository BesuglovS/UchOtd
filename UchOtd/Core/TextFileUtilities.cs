using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UchOtd.Core
{
    public static class TextFileUtilities
    {
        public static void CreateOrEmptyFile(string filename)
        {
            var sw = new StreamWriter(filename);
            sw.Close();
        }

        public static void WriteString(string filename, string line)
        {
            var sw = new StreamWriter(filename, true);
            sw.WriteLine(line);
            sw.Close();
        }

        public static void WriteStringList(string filename, List<string> lines)
        {
            var sw = new StreamWriter(filename, true);
            foreach (string line in lines)
            {
                sw.WriteLine(line);
            }
            sw.Close();
        }
    }
}
