using System.Collections.Generic;

namespace WordPlay;

public class DoubleFreaky : Sage
{
  public DoubleFreaky(List<string> source, int gameWidth) :
    base(source, gameWidth)
  {
  }

  // given a freq count, find the word with the highest frequency count;
  public string Scoringest()
  {
    var Qd = new Dictionary<char, int>(Wordlist.Analysis());
    var highScore = 0;
    var highString = "";

    foreach (var s in Wordlist)
    {
      var score = 0;
      foreach (var c in s)
      {
        score += Qd[c];
      }
      if (score > highScore)
      {
        highString = s;
      }
    }
    return highString;
  }

  public override char[] SelectWord()
  {
    var retval = Scoringest();
    return retval.ToCharArray();
  }
}