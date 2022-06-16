using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WordPlay
{

  public class Program
  {
    public static List<string> PossibleWords = new List<string>(Wordlist.Words());

    public static void Main(string[] args)
    {
      var sw = Stopwatch.StartNew();

      System.Console.Out.WriteLine("Loading {0}", Wordlist.FileName);
      //List<string> PossibleWords = new List<string>(Wordlist.Words());

      int iterations = 1000;
      int gameLength = 6;
      int gameWidth = 5;
      //The DWYL of words
      //Wordlist.FileName = "words.txt";

      // The actual solutions that wordle allows
      Wordlist.FileName = "wordle-solves.txt";

      List<ExperimentStruct> experiments = new List<ExperimentStruct>();
      ConfigExperiments(experiments, gameLength);
      System.Console.Out.WriteLine("Loading {0}", Wordlist.FileName);
      if (gameLength != 5)
      {
        PossibleWords = new List<string>(Wordlist.Words(gameWidth, false));
      }

      System.Console.Out.WriteLine("Running simulations ... ");
      Console.Out.WriteLine("Iterations: {0}\nLength: {1}\nWidth: {2}\nDictionarySize: {3}", iterations, gameLength, gameWidth, PossibleWords.Count);
      System.Console.Out.WriteLine("[----+----+----+----+----+----+----+----+----+-----]");
      System.Console.Out.Write("[");

      int graphstep = iterations / 50;
      PlayerFactory PF = new PlayerFactory();

      for (int i = 0; i < iterations; i++)
      {
        var w = PossibleWords.Random();

        if (iterations > 0 && (i % graphstep) == 0)
        {
          System.Console.Out.Write("#");
        }

        Parallel.ForEach(experiments, experiment =>
            {
              Player p = PF.Create(
                          experiment.Settings.Type,
                          PossibleWords,
                          experiment.Settings.Name,
                          experiment.Settings.Seed);
              Game g = new Game(w, experiment.Settings.Gamelength);
              experiment.Results.Add(p.Play(g));
            });
      }

      System.Console.Out.WriteLine("]");
      System.Console.Out.WriteLine();
      System.Console.Out.WriteLine($"Elapsed time: {sw.ElapsedMilliseconds} ms");
      System.Console.Out.WriteLine();
      System.Console.Out.WriteLine(" ModelType           | Win % | Wins  | Losses  | Sol Rt | St Dev | Histogram");
      System.Console.Out.WriteLine("---------------------|-------|-------|---------|--------|--------|------------------");
      foreach (var exp in experiments)
      {
        exp.Snapshot();
        System.Console.Out.WriteLine(
            string.Format(
                "{0} | {1,2} | {2,2}| {3} | {4} | {5} | {6}",
                exp.Settings.Name.PadRight(20, ' '),
                (Math.Floor((float)exp.Wins * 1000) / (10 * iterations)).ToString("#0.0").PadLeft(5, ' '),
                exp.Wins.ToString().PadRight(6, ' '),
                exp.Losses.ToString().PadRight(7, ' '),
                exp.StDev.ToString("#0.00#").PadRight(6, ' '),
                (exp.Losses == 0 ? 0 : exp.Avg).ToString("#0.00#").PadRight(6, ' '),
                exp.Histogram
            )
        );
      }
    }

    private static void ConfigExperiments(List<ExperimentStruct> exps, int defaultLength = 6)
    {
      exps.Add(new ExperimentStruct(
          new PlayStruct { Type = PlayerType.basic, Name = "Basic", Gamelength = defaultLength }));
      exps.Add(new ExperimentStruct(
          new PlayStruct { Type = PlayerType.dopey, Name = "Dopey", Gamelength = defaultLength }));
      exps.Add(new ExperimentStruct(
          new PlayStruct { Type = PlayerType.dreamy, Name = "Dreamy", Gamelength = defaultLength }));
      exps.Add(new ExperimentStruct(
          new PlayStruct { Type = PlayerType.thoughtful, Name = "Thoughtful", Gamelength = defaultLength }));
      exps.Add(new ExperimentStruct(
          new PlayStruct { Type = PlayerType.sage, Name = "Sage", Gamelength = defaultLength }));
      exps.Add(new ExperimentStruct(
          new PlayStruct { Type = PlayerType.picky, Name = "Picky - Banana", Gamelength = defaultLength, Seed = "learn;rough;aioli;route;louse;rouge;think;drink;cramp" }));
      exps.Add(new ExperimentStruct(
         new PlayStruct { Type = PlayerType.picky, Name = "Picky - Ravenclaw", Gamelength = defaultLength, Seed = "adore;spiny;audio;stern" }));
      exps.Add(new ExperimentStruct(
         new PlayStruct { Type = PlayerType.picky, Name = "Picky - word.tips", Gamelength = defaultLength, Seed = "Noise,Abuse,Opera,Naive,About,Piano,House,Alone,Above,Email,Azure,Juice,Movie,Cause,Video,Quiet,Olive,Ocean,Alive,Value,Voice,Radio,Media,ludic,ulnar,daunt,acidy".ToLower() }));
      exps.Add(new ExperimentStruct(
         new PlayStruct { Type = PlayerType.picky, Name = "Picky - Vanna", Gamelength = defaultLength, Seed = "terns,ducal,aphid,homed" }));
      exps.Add(new ExperimentStruct(
         new PlayStruct { Type = PlayerType.picky, Name = "Picky - Ace", Gamelength = defaultLength, Seed = "laser" }));
      exps.Add(new ExperimentStruct(
         new PlayStruct { Type = PlayerType.picky, Name = "Picky - Bean", Gamelength = defaultLength, Seed = "frisk,empty,gland,picky,wrest,buxom,judge,proxy,snack,blitz,blind,quoth,jerky,swamp,blind,mucky,topaz,shrew,bugle,frock,nymph,vista,album,fjord,sixty,wench,album,fjord,winch,zesty,dunce,morph,gawky,blitz,glyph,banjo,wreck,midst" }));
      exps.Add(new ExperimentStruct(
         new PlayStruct { Type = PlayerType.picky, Name = "Picky - Sonar", Gamelength = defaultLength, Seed = "outie,yarns" }));
      exps.Add(new ExperimentStruct(
          new PlayStruct { Type = PlayerType.freaky, Name = "Freaky", Gamelength = defaultLength }));
      exps.Add(new ExperimentStruct(
          new PlayStruct { Type = PlayerType.doublefreaky, Name = "DoubleFreak", Gamelength = defaultLength }));
    }
  }
}