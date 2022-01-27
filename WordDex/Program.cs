using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic; 

namespace WordDex
{
 
    class Program
    {

        static string Clean( string s)
        {
          return s.Replace("\"","").Replace(" ", "").Replace(".", "").Replace("/", "").Replace("\\", "").Replace(",", "").Replace("&", "").Replace("-", "").Replace("`", "").Replace("'", "");
        }

        static void Main(string[] args)
        {
            string source = args[0];
            string target = args[1];

            List<string> SourceList = File.ReadAllLines(source).ToList<string>();
            List<string> OutList = new List<string>();
            foreach (var item in SourceList)
            {
                var x = Clean(item);
                if (x.Length ==5)  OutList.Add(x);
            }   
            File.WriteAllLines(target, OutList);

        }
    }
}
