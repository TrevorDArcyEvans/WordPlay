using System.Collections.Generic;
using System;

namespace WordPlay
{
    public enum PlayerType { basic, dreamy, dopey, thoughtful, sage, picky }
    public class PlayerFactory{
        private Random R = new Random((int)DateTime.Now.Ticks);
        public Player Create(PlayerType type, List<string> dictionary, string Name=null, string Seed=null)
        {
            Player retval = null ;
            string name = (Name == null) ? Guid.NewGuid().ToString() :Name;
            switch (type)
            {
                case PlayerType.basic:
                    retval = new Player(dictionary);
                    break;
                case PlayerType.dopey:
                    retval = new Dopey(dictionary);
                    break;
                case PlayerType.thoughtful:
                    retval = new Thoughtful(dictionary);
                    break;
                case PlayerType.sage:
                    retval = new Sage(dictionary);
                    break;
                case PlayerType.picky:
                    retval = new Picky(dictionary,Seed);
                    break;
                case PlayerType.dreamy:
                    retval = new Dreamy(dictionary);
                    break;

            }
            retval.Name = name;
            return retval;
        }
    }

}