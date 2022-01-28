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

Initial development uses/used the [english-words](https://github.com/dwyl/english-words) repo from [Dwyl](https://github.com/dwyl), and a small filter program to filter out 5-letter terms. This dictionary contains both proper nouns and non-proper nouns (23136) in size. Current treatment is to remove capitalized entries (proper nouns) resulting in a smaller list (13992).

In the online game of wordle, proper nouns do not seem to be permitted. The Wordle program was immediately repackaged by other websites, which was simple because it contains both the list of 'answers' (2315) and the list of 'viable guesses' (10656). Pulling those lists out of the Javascript source code we can see the answers and guesses are both smaller than the list of words from dwyl. 

A simulator was built [Game.c](https://github.com/seanmunson/WordPlay/blob/main/WordPlay/Game.cs) and a base class [Player.c](https://github.com/seanmunson/WordPlay/blob/main/WordPlay/Players/Player.cs) to interact with the game. These established a baseline for 'bad guessing' players who establish the framework for analysis. Word selection and word guessing both come from the same pool of words presently. 

## Bot types : 
- Player ([player.c](https://github.com/seanmunson/WordPlay/blob/main/WordPlay/Players/Player.cs)) : Basic implementation. Randomly guesses words. 
- Dopey ([Dopey.c](https://github.com/seanmunson/WordPlay/blob/main/WordPlay/Players/Dopey.cs)) : Randomly guesses words, but doesn't repeat.
- Dreamy ([Dreamy.c](https://github.com/seanmunson/WordPlay/blob/main/WordPlay/Players/Dreamy.cs)) : When a letter is reported absent from the solution, dreamy won't guess words containing that letter (negative selection)
- Thoughtful ([Thoughtful.c](https://github.com/seanmunson/WordPlay/blob/main/WordPlay/Players/Thoughtful.cs)) : Dreamy + When a letter is reported in a location, thoughtful won't guess words without that letter at that position (positive selection)
- Sage ([Sage.c](https://github.com/seanmunson/WordPlay/blob/main/WordPlay/Players/Sage.cs)) : Thoughtful + When a letter is reported, but not at a location, Sage won't guess words having that letter in that position (partial selection)
- Picky ([Picky.c](https://github.com/seanmunson/WordPlay/blob/main/WordPlay/Players/Picky.cs)) : Sage + A pre-selected set of candidate guesses (simulating people with 'favorite starting words')


## ToDo: 
- Frequency analysis (FA) on remaining words & picking terms with maximum potential to expose / eliminate future choices. 
- Covering-first strategies.
- Testing with more than 6 guesses.
- Testing with 'wider' words (6,7,8+ letters) 
- Performance effects on seperated lists of candidate and non-winning words, as wordle does. 
- Language frequency based word & solution selection
