using System.Collections.Generic;

namespace WordPlay;

public class Dopey : Player
{
  private HashSet<char[]> chosen = new();

  public Dopey(List<string> source) :
    base(source)
  {
  }

  public override char[] SelectWord()
  {
    // this doesn't duplicate guesses
    char[] s;
    while(true)
    {
      s = base.SelectWord();
      if (chosen.Contains(s))
      {
        continue;
      }

      chosen.Add(s);
      break;
    }

    return s;
  }
}
