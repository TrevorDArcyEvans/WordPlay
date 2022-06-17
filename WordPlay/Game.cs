namespace WordPlay;

public class Game
{
  public bool GameOver;
  public char[] word;
  private int turn = 0;
  private readonly int _maxTurns = 6;
  private readonly int _gameWidth = 5;

  public Game(string wordSelected = null, int maxGuesses = 6, int gameWidth = 5)
  {
    word = wordSelected.ToCharArray();
    turn = 0;
    GameOver = false;
    _maxTurns = maxGuesses - 1;
    _gameWidth = gameWidth;
  }

  public char[] Play(char[] guessword)
  {
    var numberOfExactMatches = 0;
    var retval = new char[_gameWidth];
    if (turn < _maxTurns)
    {
      for (var i = 0; i < _gameWidth; i++)
      {
        retval[i] = (char)ResponseType.NoMatch;
        if (guessword[i] == word[i])
        {
          retval[i] = (char)ResponseType.Full;
          numberOfExactMatches++;
        }
        else
        {
          for (var j = 0; j < _gameWidth; j++)
          {
            if (guessword[i] == word[j] &&
                retval[i] == (char)ResponseType.NoMatch)
            {
              retval[i] = (char)ResponseType.Partial;
            }
          }
        }
      }
      turn++;

      if (numberOfExactMatches == _gameWidth)
      {
        GameOver = true;
      }

      if (turn == _maxTurns)
      {
        GameOver = true;
      }
    }
    else
    {
      GameOver = true;
    }
    return retval;
  }

  public bool IsResponseSolution(char[] responsetext)
  {
    var retval = true;
    for (var i = 0; i < _gameWidth; i++)
    {
      if (responsetext[i] != (char)ResponseType.Full)
      {
        return false;
      }
    }

    return retval;
  }
}
