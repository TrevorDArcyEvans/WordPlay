namespace WordPlay
{
  public class Game
  {
    public bool GameOver;
    public char[] word;
    private int turn = 0;
    private int maxTurns = 6;
    public string Name = "";

    public Game(string wordSelected = null, int maxGuesses = 6)
    {
      word = wordSelected.ToCharArray();
      turn = 0;
      GameOver = false;
      maxTurns = maxGuesses - 1;
    }

    public char[] Play(char[] guessword)
    {
      int numberOfExactMatches = 0;
      char[] retval = new char[5];
      if (turn < maxTurns)
      {
        for (int i = 0; i < 5; i++)
        {
          retval[i] = (char)ResponseType.NoMatch;
          if (guessword[i] == word[i])
          {
            retval[i] = (char)ResponseType.Full;
            numberOfExactMatches++;
          }
          else
          {
            for (int j = 0; j < 5; j++)
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

        if (numberOfExactMatches == 5)
        {
          GameOver = true;
        }

        if (turn == maxTurns)
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
      bool retval = true;
      for (int i = 0; i < 5; i++)
      {
        if (responsetext[i] != (char)ResponseType.Full)
        {
          return false;
        }
      }

      return retval;
    }
  }
}
