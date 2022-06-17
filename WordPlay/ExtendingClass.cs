using System;
using System.Collections.Generic;
using System.Linq;

namespace WordPlay;

public static class ExtendingClass
{
  private static Random randomGeneraton = new Random((int)DateTime.Now.Ticks);

  public static string Random(this IList<string> list)
  {
    // Todo : make generic
    if (list.Count > 0)
    {
      return list[randomGeneraton.Next(list.Count)];
    }
    else
    {
      return "";
    }
  }

  public static KeyValuePair<char, int>[] Analysis(this List<string> list)
  {
    Dictionary<char, int> FrequencyCount = new Dictionary<char, int>();

    foreach (string s in list)
    {
      foreach (char c in s.ToCharArray())
      {
        if (FrequencyCount.ContainsKey(c))
        {
          FrequencyCount[c]++;
        }
        else
        {
          FrequencyCount.Add(c, 1);
        }
      }
    }

    return FrequencyCount.ToArray().OrderBy(o => (1 - o.Value)).ToArray();
  }

  public static bool Contains(this string s, char c)
  {
    return s.ToCharArray().Contains(c);
  }
}