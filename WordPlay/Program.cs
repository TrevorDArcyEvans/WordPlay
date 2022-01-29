using System;
using System.Collections.Generic;
using System.Linq;
namespace WordPlay
{

    public class Program
    {
        public static List<string> PossibleWords = new List<string>(Wordlist.Words);

        public static HashSet<string> ChosenWords;
        static string GetWord()
        {
            return PossibleWords.Random();
        }
        public static void Main(string[] args)
        {
            int iterations = 10000;
            int gameLength = 6;
            List<ExperimentStruct> experiments = new List<ExperimentStruct>();
            ConfigExperiments(experiments, gameLength);

            PossibleWords = new List<string>(Wordlist.Words).Where(s => s.ToCharArray()[0] > 'Z').Select(s => s.ToUpper()).ToList();


            System.Console.Out.WriteLine("Running simulations ... ");
            Console.Out.WriteLine("Iterations: {0}\nLength:{1}\nDictionarySize:{2}", iterations, gameLength, PossibleWords.Count);
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

                foreach (var experiment in experiments)
                {
                    Player p = PF.Create(
                        experiment.Settings.Type,
                        PossibleWords,
                        experiment.Settings.Name,
                        experiment.Settings.Seed
                        );
                    Game g = new Game(w, experiment.Settings.Gamelength);
                    experiment.Results.Add(p.Play(g));
                }
            }


            System.Console.Out.WriteLine("]");
            System.Console.Out.WriteLine(" ModelType          |    Wins   |   Losses  |  Avg Length  | Histogram");
            foreach (var exp in experiments)
                System.Console.Out.WriteLine(
                    string.Format(
                        "{0}| {1,2}| {2,2}| {3} | {4}",
                        exp.Settings.Name.PadRight(20, ' '),
                        exp.Wins().ToString().PadRight(10, ' '),
                        exp.Losses().ToString().PadRight(10, ' '),
                        exp.Outcomes().ToString().PadRight(12, ' '),
                        exp.OutcomeHist()
                    )
                );
        }

        public static void ConfigExperiments(List<ExperimentStruct> E, int defaultLength = 6)
        {
            E.Add(new ExperimentStruct(
                new PlayStruct { Type = PlayerType.basic, Name = "Basic", Gamelength = defaultLength }));
            E.Add(new ExperimentStruct(
                new PlayStruct { Type = PlayerType.dopey, Name = "Dopey", Gamelength = defaultLength }));
            E.Add(new ExperimentStruct(
                new PlayStruct { Type = PlayerType.dreamy, Name = "Dreamy", Gamelength = defaultLength }));
            E.Add(new ExperimentStruct(
                new PlayStruct { Type = PlayerType.thoughtful, Name = "Thoughtful", Gamelength = defaultLength }));
            E.Add(new ExperimentStruct(
                new PlayStruct { Type = PlayerType.sage, Name = "Sage", Gamelength = defaultLength }));
            E.Add(new ExperimentStruct(
                new PlayStruct { Type = PlayerType.picky, Name = "Picky - Banana", Gamelength = defaultLength, Seed = "learn;rough;aioli;route;louse;rouge;think;drink;cramp" }));
            E.Add(new ExperimentStruct(
               new PlayStruct { Type = PlayerType.picky, Name = "Picky - Ravenclaw", Gamelength = defaultLength, Seed = "adore;spiny;audio;stern" }));
            E.Add(new ExperimentStruct(
               new PlayStruct { Type = PlayerType.picky, Name = "Picky - word.tips", Gamelength = defaultLength, Seed = "Noise,Abuse,Opera,Naive,About,Piano,House,Alone,Above,Email,Azure,Juice,Movie,Cause,Video,Quiet,Olive,Ocean,Alive,Value,Voice,Radio,Media,ludic,ulnar,daunt,acidy".ToLower() }));
            E.Add(new ExperimentStruct(
               new PlayStruct { Type = PlayerType.picky, Name = "Picky - Vanna", Gamelength = defaultLength, Seed = "terns,ducal,aphid,homed" }));
            E.Add(new ExperimentStruct(
               new PlayStruct { Type = PlayerType.picky, Name = "Picky - Ace", Gamelength = defaultLength, Seed = "laser" }));
            E.Add(new ExperimentStruct(
               new PlayStruct { Type = PlayerType.picky, Name = "Picky - Bean", Gamelength = defaultLength, Seed = "frisk,empty,gland,picky,wrest,buxom,judge,proxy,snack,blitz,blind,quoth,jerky,swamp,blind,mucky,topaz,shrew,bugle,frock,nymph,vista,album,fjord,sixty,wench,album,fjord,winch,zesty,dunce,morph,gawky,blitz,glyph,banjo,wreck,midst" }));
            E.Add(new ExperimentStruct(
               new PlayStruct { Type = PlayerType.picky, Name = "Picky - Sonar", Gamelength = defaultLength, Seed = "outie,yarns" }));
            E.Add(new ExperimentStruct(
                new PlayStruct { Type = PlayerType.freaky, Name = "Freaky", Gamelength = defaultLength }));
            E.Add(new ExperimentStruct(
                new PlayStruct { Type = PlayerType.doublefreaky, Name = "DoubleFreak", Gamelength = defaultLength }));
        }

    } //end class




}