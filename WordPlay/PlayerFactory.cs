using System;
using System.Collections.Generic;

namespace WordPlay;

public class PlayerFactory
{
  private Random R = new Random((int)DateTime.Now.Ticks);

  public Player Create(
    PlayerType type,
    List<string> dictionary,
    int gameWidth = 5,
    string Name = null,
    string Seed = null)
  {
    Player retval = null;
    string name = (Name == null) ? Guid.NewGuid().ToString() : Name;
    switch (type)
    {
      case PlayerType.basic:
        retval = new Player(dictionary, gameWidth);
        break;
      case PlayerType.dopey:
        retval = new Dopey(dictionary);
        break;
      case PlayerType.thoughtful:
        retval = new Thoughtful(dictionary, gameWidth);
        break;
      case PlayerType.sage:
        retval = new Sage(dictionary, gameWidth);
        break;
      case PlayerType.picky:
        retval = new Picky(dictionary, Seed, gameWidth);
        break;
      case PlayerType.dreamy:
        retval = new Dreamy(dictionary, gameWidth);
        break;
      case PlayerType.freaky:
        retval = new Freaky(dictionary, gameWidth);
        break;
      case PlayerType.doublefreaky:
        retval = new DoubleFreaky(dictionary, gameWidth);
        break;
    }

    retval.Name = name;

    return retval;
  }
}
