using System;
using System.Collections.Generic;
using System.Linq;

namespace WordPlay;

/// <summary>
/// Base Player class. Worst strategy - just keep picking a random word. Should lose almost every time. 
/// </summary>
public class Player
{
  protected List<char[]> Outcomes = new();
  protected List<string> Wordlist;
  protected bool Victory = false;
  public string Name = "base";
  protected char[] Solution;
  protected readonly int _gameWidth = 5;

  public Player(List<string> source, int gameWidth = 5)
  {
    Name = "Base" + DateTime.Now.Ticks;
    Wordlist = source;
    _gameWidth = gameWidth;
  }

  public virtual void RespondToPlay(char[] Guess, char[] Response)
  {
    // this is a stub to allow descendents to respond to the response
  }

  public virtual char[] SelectWord()
  {
    return Wordlist.Random().ToCharArray();
  }

  /// <summary>
  /// Eliminates entries that have a certain letter
  /// </summary>
  /// <param name="c"></param>
  protected void banletter(char c)
  {
    Wordlist = Wordlist.Where(s => { return s.Contains(c) == true; }).ToList();
  }

  /// <summary>
  /// Eliminates entries that have a certain letter
  /// </summary>
  /// <param name="c"></param>
  protected void whittle(char c)
  {
    Wordlist = Wordlist.Where(s => { return s.Contains(c) == false; }).ToList();
  }

  /// <summary>
  /// Eliminates entries that dont have a certain letter
  /// </summary>
  /// <param name="c"></param>
  protected void gotta(char c)
  {
    Wordlist = Wordlist.Where(s => { return s.Contains(c) == true; }).ToList();
    // Thanks redben
  }

  /// <summary>
  /// Eliminates entries without a certain letter at a certain position
  /// </summary>
  /// <param name="c">character</param>
  /// <param name="pos">position</param>
  protected void require(char c, int pos)
  {
    Wordlist = Wordlist.Where(s => { return (s.ToCharArray()[pos] == c); }).ToList();
  }

  /// <summary>
  /// Eliminates entries having a certain letter at a fixed position
  /// </summary>
  /// <param name="c">character</param>
  /// <param name="pos">position</param>
  protected void restrict(char c, int pos)
  {
    Wordlist = Wordlist.Where(s => { return (s.ToCharArray()[pos] != c); }).ToList();
  }

  public ResultStruct Play(Game g)
  {
    while (g.GameOver == false && Wordlist.Count > 0)
    {
      var w = SelectWord();
      var r = g.Play(w);
      Outcomes.Add(r);
      RespondToPlay(w, r);
    }
    Victory = g.IsResponseSolution(Outcomes[Outcomes.Count - 1]);
    Solution = g.word;

    ResultStruct retval = new ResultStruct();
    retval.Outcomes = Outcomes;
    retval.Solution = Solution;
    retval.Victorious = Victory;
    return retval;
  }
  public List<char[]> results { get { return Outcomes; } }
}