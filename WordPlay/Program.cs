using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using CommandLine;

namespace WordPlay;

public class Program
{
  public static void Main(string[] args)
  {
    Parser.Default.ParseArguments<Options>(args)
      .WithParsed<Options>(opt => { Run(opt); });
  }

  private static void Run(Options opt)
  {
    var iterations = opt.Iterations;
    var gameLength = opt.GameLength;
    var gameWidth = opt.GameWidth;

    var sw = Stopwatch.StartNew();

    var wordlist = new Wordlist(gameWidth != 5 ? "words.txt" : "wordle-solves.txt", gameWidth);
    var possibleWords = new List<string>(wordlist.Words);

    //The DWYL of words
    //Wordlist.FileName = "words.txt";

    // The actual solutions that wordle allows
    //Wordlist.FileName = "wordle-solves.txt";

    var experiments = new List<ExperimentStruct>();
    ConfigExperiments(experiments, gameLength, gameWidth);

    Console.WriteLine("Loading {0}", wordlist.FileName);
    Console.WriteLine("Running simulations ... ");
    Console.WriteLine("Iterations: {0}\nLength: {1}\nWidth: {2}\nDictionarySize: {3}", iterations, gameLength, gameWidth, possibleWords.Count);
    Console.WriteLine("[----+----+----+----+----+----+----+----+----+-----]");
    Console.Write("[");

    var graphstep = iterations / 50;
    var pf = new PlayerFactory();

    for (var i = 0; i < iterations; i++)
    {
      var w = possibleWords.Random();

      if (i % graphstep == 0)
      {
        Console.Write("#");
      }

      Parallel.ForEach(experiments, exp =>
      {
        var p = pf.Create(
          exp.Settings.Type,
          possibleWords,
          exp.Settings.Name,
          exp.Settings.Seed);
        var g = new Game(w, exp.Settings.GameLength, exp.Settings.GameWidth);
        exp.Results.Add(p.Play(g));
      });
    }

    Console.WriteLine("]");
    Console.WriteLine();
    Console.WriteLine($"Elapsed time: {sw.ElapsedMilliseconds} ms");
    Console.WriteLine();
    Console.WriteLine(" ModelType           | Win % | Wins  | Losses  | Sol Rt | St Dev | Histogram");
    Console.WriteLine("---------------------|-------|-------|---------|--------|--------|------------------");
    foreach (var exp in experiments)
    {
      exp.Snapshot();
      Console.WriteLine(
        "{0} | {1,2} | {2,2}| {3} | {4} | {5} | {6}",
        exp.Settings.Name.PadRight(20, ' '),
        (Math.Floor((float)exp.Wins * 1000) / (10 * iterations)).ToString("#0.0").PadLeft(5, ' '),
        exp.Wins.ToString().PadRight(6, ' '),
        exp.Losses.ToString().PadRight(7, ' '),
        exp.StDev.ToString("#0.00#").PadRight(6, ' '),
        (exp.Losses == 0 ? 0 : exp.Avg).ToString("#0.00#").PadRight(6, ' '),
        exp.Histogram);
    }
  }

  private static void ConfigExperiments(List<ExperimentStruct> exps, int defaultLength = 6, int defaultWidth = 5)
  {
    exps.Add(new ExperimentStruct(
      new PlayStruct { Type = PlayerType.basic, Name = "Basic", GameLength = defaultLength, GameWidth = defaultWidth }));
    exps.Add(new ExperimentStruct(
      new PlayStruct { Type = PlayerType.dopey, Name = "Dopey", GameLength = defaultLength, GameWidth = defaultWidth }));
    exps.Add(new ExperimentStruct(
      new PlayStruct { Type = PlayerType.dreamy, Name = "Dreamy", GameLength = defaultLength, GameWidth = defaultWidth }));
    exps.Add(new ExperimentStruct(
      new PlayStruct { Type = PlayerType.thoughtful, Name = "Thoughtful", GameLength = defaultLength, GameWidth = defaultWidth }));
    exps.Add(new ExperimentStruct(
      new PlayStruct { Type = PlayerType.sage, Name = "Sage", GameLength = defaultLength, GameWidth = defaultWidth }));
    exps.Add(new ExperimentStruct(
      new PlayStruct { Type = PlayerType.picky, Name = "Picky - Banana", GameLength = defaultLength, GameWidth = defaultWidth, Seed = "learn;rough;aioli;route;louse;rouge;think;drink;cramp" }));
    exps.Add(new ExperimentStruct(
      new PlayStruct { Type = PlayerType.picky, Name = "Picky - Ravenclaw", GameLength = defaultLength, GameWidth = defaultWidth, Seed = "adore;spiny;audio;stern" }));
    exps.Add(new ExperimentStruct(
      new PlayStruct { Type = PlayerType.picky, Name = "Picky - word.tips", GameLength = defaultLength, GameWidth = defaultWidth, Seed = "Noise,Abuse,Opera,Naive,About,Piano,House,Alone,Above,Email,Azure,Juice,Movie,Cause,Video,Quiet,Olive,Ocean,Alive,Value,Voice,Radio,Media,ludic,ulnar,daunt,acidy".ToLower() }));
    exps.Add(new ExperimentStruct(
      new PlayStruct { Type = PlayerType.picky, Name = "Picky - Vanna", GameLength = defaultLength, GameWidth = defaultWidth, Seed = "terns,ducal,aphid,homed" }));
    exps.Add(new ExperimentStruct(
      new PlayStruct { Type = PlayerType.picky, Name = "Picky - Ace", GameLength = defaultLength, GameWidth = defaultWidth, Seed = "laser" }));
    exps.Add(new ExperimentStruct(
      new PlayStruct { Type = PlayerType.picky, Name = "Picky - Bean", GameLength = defaultLength, GameWidth = defaultWidth, Seed = "frisk,empty,gland,picky,wrest,buxom,judge,proxy,snack,blitz,blind,quoth,jerky,swamp,blind,mucky,topaz,shrew,bugle,frock,nymph,vista,album,fjord,sixty,wench,album,fjord,winch,zesty,dunce,morph,gawky,blitz,glyph,banjo,wreck,midst" }));
    exps.Add(new ExperimentStruct(
      new PlayStruct { Type = PlayerType.picky, Name = "Picky - Sonar", GameLength = defaultLength, GameWidth = defaultWidth, Seed = "outie,yarns" }));
    exps.Add(new ExperimentStruct(
      new PlayStruct { Type = PlayerType.freaky, Name = "Freaky", GameLength = defaultLength, GameWidth = defaultWidth }));
    exps.Add(new ExperimentStruct(
      new PlayStruct { Type = PlayerType.doublefreaky, Name = "DoubleFreak", GameLength = defaultLength, GameWidth = defaultWidth }));
  }
}
