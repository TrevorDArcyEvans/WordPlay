using System.Collections.Generic;

namespace WordPlay
{
    public class DoubleFreaky : Sage
    {
        public DoubleFreaky(List<string> source) : base(source)
        {
        }

        // given a freq count, find the word with the highest frequency count;
        public string Scoringest()
        {
            var Qd = new Dictionary<char, int>(Wordlist.Analysis());
            int highScore=0;
            string highString="";

            foreach(string s in Wordlist)
            {
                var score = 0;
                foreach (char c in s.ToCharArray())
                {
                    score += Qd[c];
                }
                if (score > highScore)
                {
                    highString = s;
                }
            }
            return highString;
        }
        public override char[] SelectWord()
        { 
            var retval = Scoringest();  
            return retval.ToCharArray();
        }

        // public override void RespondToPlay -- Inherited from base

    }
}
