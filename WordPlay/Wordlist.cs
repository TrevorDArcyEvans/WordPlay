using System;
using System.IO;
using System.Linq;

namespace WordPlay;

public class Wordlist
{
  public readonly string FileName = "words.txt";

  public string[] Words { get; } = Array.Empty<string>();

  public Wordlist(string fileName = "words.txt", int width = 5, bool useProper = false)
  {
    FileName = fileName;

    var garbagefilter = ".,-_/\\'\"".ToCharArray();
    if (Words.Count() == 0)
    {
      Words = File.ReadAllLines(FileName)
        .Where(w => w.LastIndexOfAny(garbagefilter) < 0)
        .Where(w => w.Length == width)
        .ToArray();
    }

    // filter for 
    if (useProper == false)
    {
      Words = Words.Where(s => s.ToCharArray()[0] > 'Z').ToArray();
    }
  }
}