using System.Collections.Generic;

namespace WordPlay
{
    public class Freaky : Sage
    {
        public Freaky(List<string> source) : base(source)
        {
        }

        public override char[] SelectWord()
        {
            // do a frequency analysis and choose a word with the
            // most popular letter
            var Fq = Wordlist.Analysis();

            var retval = Wordlist.Random();
            while (!retval.Contains(Fq[0].Key))
            {
                retval = Wordlist.Random();
            }
            return retval.ToCharArray();
        }

        // public override void RespondToPlay -- Inherited from base

    }
}
