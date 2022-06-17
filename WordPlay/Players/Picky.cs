using System.Collections.Generic;
using System.Linq;

namespace WordPlay;

public class Picky : Player
{
  protected List<string> preferredWords;

  public Picky(List<string> source, string seed) : base(source)
  {
    // our strategy is destructive
    // make a copy of the list.
    preferredWords = new List<string>(seed.Split(';'));
    Wordlist = source.ToList();
  }

  public override char[] SelectWord()
  {
    // picky uses preferred words first.
    while (preferredWords.Count > 0)
    {
      if (Wordlist.Contains(preferredWords.First()))
      {
        return preferredWords.First().ToCharArray();
      }
      else
      {
        preferredWords.Remove(preferredWords.First());
      }
    }

    return Wordlist.Random().ToCharArray();
  }

  public override void RespondToPlay(char[] guess, char[] response)
  {
    for (int i = 0; i < 5; i++)
    {
      if (response[i] == (char)ResponseType.Full)
      {
        require(guess[i], i);
      }

      if (response[i] == (char)ResponseType.NoMatch)
      {
        whittle(guess[i]);
      }

      if (response[i] == (char)ResponseType.Partial)
      {
        restrict(guess[i], i);
      }
    }
    Wordlist.Remove(new string(guess));
  }
}