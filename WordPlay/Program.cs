using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System;
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
            List<ExperimentStruct> experiments = new List<ExperimentStruct>();
            ConfigExperiments(experiments, 6);

            PossibleWords = new List<string>(Wordlist.Words).Where( s=>s.ToCharArray()[0] > 'Z').Select( s=> s.ToUpper()).ToList();

            int iterations = 10000;
            System.Console.Out.WriteLine("Running simulations ... ");
            System.Console.Out.WriteLine("[----+----+----+----+----+----+----+----+----+-----]");
            System.Console.Out.Write("[");

            int graphstep = iterations / 50;
            PlayerFactory PF = new PlayerFactory();
 
            for (int i = 0; i < iterations; i++)
            {
                var w = PossibleWords.Random();

                if (iterations>0 && (i % graphstep) == 0)
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
                        exp.Settings.Name.PadRight(20,' '), 
                        exp.Wins().ToString().PadRight(10, ' '),
                        exp.Losses().ToString().PadRight(10, ' '),
                        exp.Outcomes().ToString().PadRight(12, ' '), 
                        exp.OutcomeHist()
                    )
                );
            }

        public static void ConfigExperiments(List<ExperimentStruct> E, int defaultLength=6)
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
                new PlayStruct { Type = PlayerType.picky, Name = "Picky - Nancy", Gamelength = defaultLength, Seed = "learn;rough;aioli;route;louse;rouge;think;drink;cramp" }));
            E.Add(new ExperimentStruct(
               new PlayStruct { Type = PlayerType.picky, Name = "Picky - Rivkah", Gamelength = defaultLength, Seed = "adore;spiny;audio;stern" }));
            E.Add(new ExperimentStruct(
                new PlayStruct { Type = PlayerType.freaky, Name = "Freaky", Gamelength = defaultLength }));
            E.Add(new ExperimentStruct(
                new PlayStruct { Type = PlayerType.doublefreaky, Name = "DoubleFreak", Gamelength = defaultLength }));
        }

    } //end class

        
    

}