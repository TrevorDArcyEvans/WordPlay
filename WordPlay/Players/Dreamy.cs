using System.Collections.Generic;
using System.Linq;

namespace WordPlay;

// this is the minimum expected-to-play-right item.
public class Dreamy : Player
{
  public Dreamy(List<string> source, int gameWidth) : 
    base(source, gameWidth)
  {
    // our strategy is destructive
    // make a copy of the list.
    Wordlist = source.ToList();
  }

  public override char[] SelectWord()
  {
    return Wordlist.Random().ToCharArray();
  }

  // Dreamy doesn't remember affirmations, just negations
  public override void RespondToPlay(char[] guess, char[] response)
  {
    for (int i = 0; i < _gameWidth; i++)
    {
      if (response[i] == (char)ResponseType.NoMatch)
      {
        whittle(guess[i]);
      }
    }
  }
}