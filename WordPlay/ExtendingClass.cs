using System;
using System.Collections.Generic;
using System.Linq;
namespace WordPlay
{
    public static class ExtendingClass
    {
        private static Random randomGeneraton = new Random((int)DateTime.Now.Ticks);
        public static string Random(this IList<string> L) 
        {
            // Todo : make generic
            if (L.Count>0)
            {
                return L[randomGeneraton.Next(L.Count)];
            }
            else
            {
                return "";
            }
        }
    }

}