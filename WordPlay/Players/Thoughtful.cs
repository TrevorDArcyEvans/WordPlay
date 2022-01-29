using System.Collections.Generic;
using System.Linq;

namespace WordPlay
{
    // this is the minimum expected-to-play-right item.
    public class Thoughtful : Player
    {
        public Thoughtful(List<string> source) : base(source)
        {
            // our strategy is destructive
            // make a copy of the list.
            this.Wordlist = source.ToList();

        }

        public override char[] SelectWord()
        {
            return Wordlist.Random().ToCharArray();
        }

        public override void RespondToPlay(char[] guess, char[] response)
        {
            for (int i = 0; i < 5; i++)
            {
                if (response[i] == (char)responsetype.full) require(guess[i], i);
                if (response[i] == (char)responsetype.nomatch) whittle(guess[i]);
            }
        }
    }

}