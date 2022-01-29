using System.IO;
using System.Linq;

namespace WordPlay
{
    public static class Wordlist
    {
        static string[] words = new string[0];
        static string FILENAME = "wordle-solves.txt";
        public static string[] Words
        {
            get
            {
                if (words.Count() == 0)
                {
                    words = File.ReadAllLines(FILENAME);
                }
                return words;
            }
        }

    }
}
