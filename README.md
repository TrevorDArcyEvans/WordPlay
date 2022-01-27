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

Initial development uses/used the [english-words](https://github.com/dwyl/english-words) repo from [Dwyl](https://github.com/dwyl), and a small filter program to filter out 5-letter terms. This dictionary contains both proper nouns and non-proper nouns. In the online game of wordle, proper nouns do not seem to be permitted. The Wordle program was immediately repackaged by other websites, which was simple because it contains both the list of 'answers' (2315) and the list of 'viable guesses' (10656)

A simulator was built [Game.c](https://github.com/seanmunson/WordPlay/blob/main/WordPlay/Game.cs) and a base class [Player.c](https://github.com/seanmunson/WordPlay/blob/main/WordPlay/Players/Player.cs) to interact with the game. These established a baseline for 'bad guessing' players who establish the framework for analysis.  
