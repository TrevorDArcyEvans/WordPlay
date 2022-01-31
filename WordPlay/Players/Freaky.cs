using System.Collections.Generic;

namespace WordPlay
{
    public class Freaky : Sage
    {
        public List<char> played= new List<char>();
        public Freaky(List<string> source) : base(source)
        {
             
        }

        public override char[] SelectWord()
        {
            // do a frequency analysis and choose a word with the
            // most popular letter
            var Fq = Wordlist.Analysis();

            var retval = Wordlist.Random();
            int i=0;
            while (this.played.Contains(Fq[i].Key)==true)
            {
                i++;
            }
            while (!retval.Contains(Fq[i].Key))
            {
                retval = Wordlist.Random();
            }
            this.played.Add(Fq[i].Key);
            return retval.ToCharArray();
        }

        // public override void RespondToPlay -- Inherited from base

    }
}
