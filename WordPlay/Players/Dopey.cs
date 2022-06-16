using System.Collections.Generic;

namespace WordPlay
{
  public class Dopey : Player
  {
    private HashSet<char[]> chosen = new();

    public Dopey(List<string> source) :
      base(source)
    {
    }

    public new char[] SelectWord()
    {
      // this doesn't duplicate guesses
      char[] s;
      do
      {
        s = base.SelectWord();
      }
      while (chosen.Contains(s));
      return s;
    }
  }
}
