using System.Collections.Generic;
using System.Linq;
using System;
namespace WordPlay
{
    public class ExperimentStruct
    {
        public ExperimentStruct(PlayStruct p)
        {
            this.Settings = p;
            this.Results = new List<ResultStruct>();
        }
        public PlayStruct Settings;
        public List<ResultStruct> Results;
        public float Losses()
        {
            return Results.Select(o => (o.Victorious == true) ? 0 : 1 ).Sum()  ;
        }
        public float Wins()
        {
            return Results.Select(o => (o.Victorious == true) ? 1 : 0).Sum() ;
        }
        public float LossRate()
        {
            return Results.Count == 0 ? 0 : Results.Where(o => o.Victorious == false).Count() / (float)Results.Count;
        }
        public float WinRate()
        {
            float v = Results.Where(o => o.Victorious == false).Count() / (float)Results.Count;
            return Results.Count == 0 ? 0 : v;
        }
        public float Outcomes()
        {
            float tot =  (float)Results.Select(o => (o.Victorious == true) ? o.Outcomes.Count:0).Sum() ;

            return (Results.Count > 0) ? tot / (float)this.Wins() : 0;
        }

        public string OutcomeHist()
        {
            string retval = "";
            int[] countOfSolutionLengths = new int[Results.Max(o => o.Outcomes.Count)+1];
            Array.Fill(countOfSolutionLengths, 0);
            foreach (var r in Results)
            {
                countOfSolutionLengths[r.Outcomes.Count]++;
            }
            for(int l=1;l<countOfSolutionLengths.Length;l++)
            {
                retval += String.Format(" {0}", countOfSolutionLengths[l].ToString().PadRight(5, ' '));
            }
            return retval;
        }
    }

        
    

}