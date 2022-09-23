using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motaz_Store
{
    public static class Pro_Des
    {
        private static char[] FChars = { 'K', 'G', 'S', 'C', 'H', 'P', 'O', 'B', 'A' };
        private static string[] FWords = { "كوتش", "حذاء", "شبشب", "صندل", "هاف", "بوط", "صابو", "شنطة", "إكسسوار" };

        private static char[] SChars = { 'R', 'H', 'B' };
        private static string[] SWords = { "رجالي", "حريمي", "أطفالي" };

        public static string GetDes(string parameter)
        {
            string Des = "";

            int i = Array.FindIndex(FChars, v => v.Equals(Char.ToUpper(parameter[0])));
            if (i != -1)
            {
                Des += FWords[i] + " ";
            }
            else
                return "غير معروف";

            i = Array.FindIndex(SChars, v => v.Equals(Char.ToUpper(parameter[1])));
            if (i != -1)
            {
                Des += SWords[i];
            }
            else
                return "غير معروف";

            return Des;
        }

        public static List<string> GetAllDes()
        {
            List<string> all = new List<string>();

            all.Add("الجميع");

            foreach(string s1 in FWords)
            {
                foreach(string s2 in SWords)
                {
                    all.Add(s1 + " " + s2);
                }
            }

            return all;
        }

        public static string GetChars(string parameter)
        {
            string[] words = parameter.Split(' ');
            string word = null;
            int i = Array.FindIndex(FWords, v => v.Equals(words[0]));
            word += FChars[i];
            i = Array.FindIndex(SWords, v => v.Equals(words[1]));
            word += SChars[i];
            return word;
        }
    }
}
