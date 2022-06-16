using System;
using System.Collections.Generic;
using System.Linq;

namespace WordPlay
{
  public class ExperimentStruct
  {
    public PlayStruct Settings;
    public List<ResultStruct> Results = new();
    public int Wins;
    public double WinRate;
    public int Losses;
    public double LossRate;
    public double Avg;
    public double StDev;
    public string Histogram;

    public ExperimentStruct(PlayStruct p)
    {
      this.Settings = p;
    }

    public void Snapshot()
    {
      Wins = 0;
      WinRate = 0;
      Losses = 0;
      LossRate = 0;
      Avg = 0;
      StDev = 0;
      Histogram = "";

      int[] countOfSolutionLengths = new int[Results.Max(o => o.Outcomes.Count) + 1];
      foreach (var r in Results)
      {
        if (r.Victorious)
        {
          Wins++;
          Avg += r.Outcomes.Count;
          countOfSolutionLengths[r.Outcomes.Count]++;
        }
        else
        {
          Losses++;
        }
      }

      WinRate = Wins / Results.Count;
      LossRate = 1 - WinRate;
      Avg = Avg / Results.Count;

      for (int l = 1; l < countOfSolutionLengths.Length; l++)
      {
        Histogram += String.Format(" {0}", countOfSolutionLengths[l].ToString().PadRight(5, ' '));
      }

      foreach (var r in Results)
      {
        if (r.Victorious)
        {
          double i = r.Outcomes.Count;
          StDev += (i - Avg) * (i - Avg);
        }
      }

      if (Wins != 0)
      {
        StDev /= Wins;
        StDev = Math.Sqrt(StDev);
      }
      else
      {
        StDev = 0;
      }
    }
  }
}
