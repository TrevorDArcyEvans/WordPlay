using System.Collections.Generic;
using System;

namespace WordPlay
{
    public class Dopey : Player
    {
        private HashSet<char[]> chosen;
        public Dopey(List<string> source) : base(source)
        {
            chosen = new HashSet<char[]>(); 
        }

        public new char[] SelectWord()
        {
            // this doesn't duplicate guesses
            char[] s;
            do
            {
                s = base.SelectWord();
            }
            while (chosen.Contains(s));
            return s;
        }
    }

}