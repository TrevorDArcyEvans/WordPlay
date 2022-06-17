using System.Collections.Generic;
using System.Linq;

namespace WordPlay;

// this is the minimum expected-to-play-right item.
public class Thoughtful : Player
{
  public Thoughtful(List<string> source, int gameWidth) : 
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

  public override void RespondToPlay(char[] guess, char[] response)
  {
    for (var i = 0; i < _gameWidth; i++)
    {
      if (response[i] == (char)ResponseType.Full)
      {
        require(guess[i], i);
      }

      if (response[i] == (char)ResponseType.NoMatch)
      {
        whittle(guess[i]);
      }
    }
  }
}
