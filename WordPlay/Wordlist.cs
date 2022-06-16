using System;
using System.IO;
using System.Linq;

namespace WordPlay
{
  public static class Wordlist
  {
    public static string FileName = "words.txt";
    private static string[] words = Array.Empty<string>();

    public static string[] Words(int width = 5, bool useProper = false)
    {
      char[] garbagefilter = ".,-_/\\'\"".ToCharArray();
      if (words.Count() == 0)
      {
        words = File.ReadAllLines(FileName)
          .Where(w => w.LastIndexOfAny(garbagefilter) < 0)
          .Where(w => w.Length == width)
          .ToArray();
      }

      // filter for 
      if (useProper == false)
      {
        words = words.Where(s => s.ToCharArray()[0] > 'Z').ToArray();
      }

      return words;
    }
  }
}
