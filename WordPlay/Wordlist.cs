using System.IO;
using System.Linq;

namespace WordPlay
{
    public static class Wordlist
    {
        static string[] words = new string[0];
        public static string FileName = "words.txt";
        public static string[] Words(int width=5, bool useProper=false)
        {        
            if (words.Count() == 0)
            {
                words = File.ReadAllLines(FileName);
            }
            // filter for width
            words = words.Where(s => s.Length == width).ToArray();
            // filter for 
            if (useProper == false)
                words = words.Where(s => s.ToCharArray()[0] > 'Z').ToArray();

            return words;
        }

    }
}
