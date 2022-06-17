# WordPlay
A simulator built to explore the dynamics of the Wordle Online word game.

In Late December 2021, the word game [Wordle](https://www.powerlanguage.co.uk/wordle/) blew up in popularity

The Game's simplicity was part of its charm:

- A player successively guesses 5-letter words up to six times
- Each guess earns a response of a non-match, a partial match, or a full match
- Players who guess 6 times without matching 'lose'
- All players play the same word each day (as defined by local timezone)

The game also came with a feature that made it easy to share and compare on social media : 

Wordle 222 5/6*

⬛⬛🟨⬛⬛<br>
🟨⬛⬛🟩⬛<br>
⬛⬛🟩🟩🟩<br>
⬛🟩🟩🟩🟩<br>
🟩🟩🟩🟩🟩<br>

## Initial Exploration
This game seemed sufficiently constrained that an automated exploration of the combinatorics was tempting. 

Initial development uses/used the [english-words](https://github.com/dwyl/english-words) repo from
[Dwyl](https://github.com/dwyl), and a small filter program to filter out 5-letter terms. This dictionary
contains both proper nouns and non-proper nouns (23136) in size. Current treatment is to remove capitalized
entries (proper nouns) resulting in a smaller list (13992).

In the online game of wordle, proper nouns do not seem to be permitted. The Wordle program was immediately
repackaged by other websites, which was simple because it contains both the list of 'answers' (2315) and
the list of 'viable guesses' (10656). Pulling those lists out of the Javascript source code we can see
the answers and guesses are both smaller than the list of words from dwyl. 

A simulator was built [Game.cs](WordPlay/Game.cs) and a
base class [Player.cs](WordPlay/Players/Player.cs) to
interact with the game. These established a baseline for 'bad guessing' players who establish the
framework for analysis. Word selection and word guessing both come from the same pool of words presently. 

## Bot types : 
- Player ([Player.cs](WordPlay/Players/Player.cs)) : Basic implementation. Randomly guesses words. 
- Dopey ([Dopey.cs](WordPlay/Players/Dopey.cs)) : Randomly guesses words, but doesn't repeat.
- Dreamy ([Dreamy.cs](WordPlay/Players/Dreamy.cs)) : When a letter is reported absent from the solution, dreamy won't guess words containing that letter (negative selection)
- Thoughtful ([Thoughtful.cs](WordPlay/Players/Thoughtful.cs)) : Dreamy + When a letter is reported in a location, thoughtful won't guess words without that letter at that position (positive selection)
- Sage ([Sage.cs](WordPlay/Players/Sage.cs)) : Thoughtful + When a letter is reported, but not at a location, Sage won't guess words having that letter in that position (partial selection)
- Picky ([Picky.cs](WordPlay/Players/Picky.cs)) : Sage + A pre-selected set of candidate guesses (simulating people with 'favorite starting words')
- Freaky ([Freaky.cs](WordPlay/Players/Freaky.cs) : Sage + Each step, it determines the most common letter in the dictionary and then ensures that the guess includes that letter. 
- DoubleFreak ([DoubleFreak.cs](WordPlay/Players/DoubleFreak.cs) : Sage + in each step, it evaluates every word in the list and assigns a weight based on the popularity of the letter, then choosing the highest-ranked word.

## ToDo:  
- Performance effects on seperated lists of candidate and non-winning words, as wordle does. 
- Language frequency based word & solution selection

## Log:
* 1/28 - Frequency analysis of two types have been implemented. More work is needed
* 1/29 - Covering strategies have been implemented using a selection of terms in a picky player. 
* 1/30 - In the interest of exploration, the command-line app has been reconfigured to allow a selection 
of an arbitrary width (word length) and an arbitrary number of guesses in the game, as well as the
selection of a different source file. 

## Prerequisites
* .NET Core 6

## Getting started
```bash
git clone https://github.com/TrevorDArcyEvans/WordPlay.git
cd WordPlay
dotnet restore
dotnet build
```

### Commandline interface
```bash
$ ./WordPlay --help
WordPlay 1.0.0
Copyright 2022 Sean Munson + Trevor D'Arcy-Evans

  -w, --width         (Default: 5) Number of letters in target word

  -l, --length        (Default: 6) Maximum number of guesses

  -i, --iterations    (Default: 1000) Number of iterations to run

  -f, --file          (Default: wordle-solves.txt) Word list to use: wordle-solves.txt [2315] or words.txt [26132]

  --help              Display this help screen.

  --version           Display version information.
```

### Sample run
```bash
$ ./WordPlay
Loading wordle-solves.txt
Running simulations ... 
Iterations: 1000
Length: 6
Width: 5
DictionarySize: 2315
[----+----+----+----+----+----+----+----+----+-----]
[##################################################]

Elapsed time: 5417 ms

 ModelType           | Win % | Wins  | Losses  | Sol Rt | St Dev | Histogram
---------------------|-------|-------|---------|--------|--------|------------------
Basic                |   0.3 | 3     | 997     | 3.686  | 0.011  |  0     0     1     2     0    
Dopey                |   0.2 | 2     | 998     | 4.519  | 0.009  |  0     0     0     1     1    
Dreamy               |   8.5 | 85    | 915     | 4.156  | 0.378  |  1     2     8     21    53   
Thoughtful           |  48.9 | 489   | 511     | 2.344  | 2.114  |  0     13    58    176   242  
Sage                 |  91.9 | 919   | 81      | 0.892  | 3.617  |  1     39    227   403   249  
Picky - Banana       |  73.1 | 731   | 269     | 1.381  | 3.119  |  1     12    102   292   324  
Picky - Ravenclaw    |  74.2 | 742   | 258     | 1.369  | 3.212  |  1     22    79    270   370  
Picky - word.tips    |  68.6 | 686   | 314     | 1.563  | 2.967  |  0     14    88    245   339  
Picky - Vanna        |  70.9 | 709   | 291     | 1.475  | 3.061  |  0     15    90    259   345  
Picky - Ace          |  68.1 | 681   | 319     | 1.589  | 2.96   |  0     13    89    228   351  
Picky - Bean         |  68.6 | 686   | 314     | 1.569  | 2.973  |  2     10    91    237   346  
Picky - Sonar        |  69.2 | 692   | 308     | 1.549  | 3.012  |  0     13    90    229   360  
Freaky               |  91.0 | 910   | 90      | 0.898  | 3.561  |  0     39    236   400   235  
DoubleFreak          |  90.2 | 902   | 98      | 0.923  | 3.549  |  0     40    229   383   250  
```
