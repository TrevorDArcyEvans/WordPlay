using System.IO;
using System.Linq;

namespace WordPlay
{
    public static class Wordlist
    {
        static string[] words = new string[0];
        public static string FileName = "words.txt";
        public static string[] Words(int width = 5, bool useProper = false)
        {
            char[] garbagefilter = ".,-_/\\'\"".ToCharArray();
            if (words.Count() == 0)
            {
                words = File.ReadAllLines(FileName).Where(w => w.LastIndexOfAny(garbagefilter) < 0).ToArray();
                 
            }
            // filter garbage
             
            foreach (string w in words)
            {
                if (w.LastIndexOfAny(garbagefilter) != -1) ;
            
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
