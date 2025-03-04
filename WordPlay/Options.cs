﻿using CommandLine;

namespace WordPlay;

public class Options
{
  [Option('w', "width", Default = 5, Required = false, HelpText = "Number of letters in target word")]
  public int GameWidth { get; set; }

  [Option('l', "length", Default = 6, Required = false, HelpText = "Maximum number of guesses")]
  public int GameLength { get; set; }

  [Option('i', "iterations", Default = 1000, Required = false, HelpText = "Number of iterations to run")]
  public int Iterations { get; set; }

  [Option('f', "file", Default = "wordle-solves.txt", Required = false, HelpText = "Word list to use: wordle-solves.txt [2315] or words.txt [26132]")]
  public string WordListFile { get; set; }
}
