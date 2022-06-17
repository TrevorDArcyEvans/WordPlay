using System.Collections.Generic;

namespace WordPlay;

public class Freaky : Sage
{
  public List<char> played = new();

  public Freaky(List<string> source, int gameWidth) :
    base(source, gameWidth)
  {
  }

  public override char[] SelectWord()
  {
    // do a frequency analysis and choose a word with the
    // most popular letter
    var Fq = Wordlist.Analysis();

    var retval = Wordlist.Random();

    var mostFrequentletter = -1; ;
    // avoid re-requiring 'successful' vowels
    for (var i = 0; (i < played.Count && i < Fq.Length); i++)
    {
      if (!played.Contains(Fq[i].Key) &&
          mostFrequentletter == -1)
      {
        mostFrequentletter = i;
      }
    }

    if (mostFrequentletter == -1)
    {
      mostFrequentletter = 0;
    }

    while (!retval.Contains(Fq[mostFrequentletter].Key))
    {
      retval = Wordlist.Random();
    }

    played.Add(Fq[mostFrequentletter].Key);

    return retval.ToCharArray();
  }
}