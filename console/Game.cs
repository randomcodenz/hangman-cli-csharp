using System;
using System.Collections.Generic;
using System.Linq;

namespace console
{
    public class Game
    {
        private const string ValidGuesses = "abcdefghijklmnopqrstuvwxyz";
        
        private readonly string _word;
        private int _lives;
        private readonly List<char> _guesses;

        public Game(string word, int lives)
        {
            _word = word;
            _lives = lives;
            _guesses = new List<char>();
        }

        public GameState Start()
        {
            return GameState.GameInProgress(_lives, BuildClue(), _guesses);
        }

        public (GameState, GuessResult) MakeGuess(char guess)
        {
            if (GameWon())
            {
                return (GameState.GameWon(_lives, _word.OfType<char?>(), _guesses), GuessResult.GameOver);
            }

            if (GameLost())
            {
                return (GameState.GameLost(_lives, _word.OfType<char?>(), _guesses), GuessResult.GameOver);
            }
            
            if (InvalidGuess(guess))
            {
                return (GameState.GameInProgress(_lives, BuildClue(), _guesses), GuessResult.Invalid);
            }

            if (DuplicateGuess(guess))
            {
                return (GameState.GameInProgress(_lives, BuildClue(), _guesses), GuessResult.Duplicate);
            }

            _guesses.Add(char.ToLowerInvariant(guess));

            var guessResult = _word.Contains(guess, StringComparison.InvariantCultureIgnoreCase) ? GuessResult.Correct : GuessResult.Incorrect;
            if (guessResult == GuessResult.Incorrect)
            {
                _lives -= 1;
            }

            if (GameWon())
            {
                return (GameState.GameWon(_lives, _word.OfType<char?>(), _guesses), GuessResult.GameOver);
            }

            if (GameLost())
            {
                return (GameState.GameLost(_lives, _word.OfType<char?>(), _guesses), GuessResult.GameOver);
            }

            return (GameState.GameInProgress(_lives, BuildClue(), _guesses), guessResult);
        }

        private IEnumerable<char?> BuildClue() => _word.Select(letter => _guesses.Contains(letter, CharComparison.InvariantCultureIgnoreCase) ? letter : null as char?);

        private bool DuplicateGuess(in char guess) => _guesses.Contains(guess, CharComparison.InvariantCultureIgnoreCase);

        private bool WordGuessed() => !_word.Except(_guesses, CharComparison.InvariantCultureIgnoreCase).Any();

        private bool InvalidGuess(in char guess) => !ValidGuesses.Contains(guess, StringComparison.InvariantCultureIgnoreCase);

        private bool GameLost() => _lives <= 0;

        private bool GameWon() => _lives > 0 && WordGuessed();
    }
}