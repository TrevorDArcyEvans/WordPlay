using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordPlay 
{
    // this is the minimum expected-to-play-right item.
    public class Dreamy : Player
    {
        public Dreamy(List<string> source) : base(source)
        {
            // our strategy is destructive
            // make a copy of the list.
            this.Wordlist = source.ToList();

        }

        public override char[] SelectWord()
        {
            return Wordlist.Random().ToCharArray();
        }

        // Dreamy doesn't remember affirmations, just negations
        public override void RespondToPlay(char[] guess, char[] response)
        {
            for (int i = 0; i < 5; i++)
            {
                if (response[i] == (char)responsetype.nomatch) whittle(guess[i]);
            }
        }
    }
}
