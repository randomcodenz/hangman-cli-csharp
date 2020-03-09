using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace console.test
{
    public class GameSpec
    {
        private const int Lives = 5;
        private static Game Game => new Game("Hangman", Lives);

        public class StartSpec
        {
            private static GameState GameState => Game.Start();

            [Fact]
            public void ClueIsEntirelyMasked() => Assert.All(GameState.Clue, c => Assert.Null(c));

            [Fact]
            public void NoLivesHaveBeenTaken() => Assert.Equal(Lives, GameState.LivesRemaining);

            [Fact]
            public void PreviousGuessesIsEmpty() => Assert.Empty(GameState.PreviousGuesses);
        }

        public class MakeGuessSpec
        {
            public class WhenMakingACorrectGuess
            {
                private static (GameState, GuessResult) GuessOutcome => Game.MakeGuess('h');
                private static GameState GameState => GuessOutcome.Item1;
                private static GuessResult GuessResult => GuessOutcome.Item2;
                
                [Fact]
                public void ResultIndicatesGuessIsCorrect() => Assert.Equal(GuessResult.Correct, GuessResult);

                [Fact]
                public void NoLivesHaveBeenTaken() => Assert.Equal(Lives, GameState.LivesRemaining);

                [Fact]
                public void PreviousGuessesIncludesGuess() => Assert.Equal(new[] {'h'}, GameState.PreviousGuesses);

                [Fact]
                public void ClueIncludesGuessedLetterInCorrectPlaces() => Assert.Equal(new char?[] {'H', null, null, null, null, null, null}, GameState.Clue);

                [Fact]
                public void GameIsNotWon() => Assert.False(GameState.Won);

                [Fact]
                public void GameIsNotLost() => Assert.False(GameState.Lost);

                [Fact]
                public void GameIsInProgress() => Assert.True(GameState.InProgress);
            }
            
            public class WhenMakingAnIncorrectGuess
            {
                private static (GameState, GuessResult) GuessOutcome => Game.MakeGuess('x');
                private static GameState GameState => GuessOutcome.Item1;
                private static GuessResult GuessResult => GuessOutcome.Item2;
                
                [Fact]
                public void ResultIndicatesGuessIsIncorrect() => Assert.Equal(GuessResult.Incorrect, GuessResult);

                [Fact]
                public void OneLifeHasBeenTaken() => Assert.Equal(Lives - 1, GameState.LivesRemaining);

                [Fact]
                public void PreviousGuessesIncludesGuess() => Assert.Equal(new[] {'x'}, GameState.PreviousGuesses);

                [Fact]
                public void ClueIsStillCompletelyMasked() => Assert.Equal(new char?[] {null, null, null, null, null, null, null}, GameState.Clue);

                [Fact]
                public void GameIsNotWon() => Assert.False(GameState.Won);

                [Fact]
                public void GameIsNotLost() => Assert.False(GameState.Lost);

                [Fact]
                public void GameIsInProgress() => Assert.True(GameState.InProgress);
            }
            
            public class WhenMakingADuplicateCorrectGuess
            {
                private static (GameState, GuessResult) GuessOutcome
                {
                    get
                    {
                        var game = Game;
                        game.MakeGuess('h');
                        return game.MakeGuess('h');
                    }
                }

                private static GameState GameState => GuessOutcome.Item1;
                private static GuessResult GuessResult => GuessOutcome.Item2;
                
                [Fact]
                public void ResultIndicatesGuessIsDuplicate() => Assert.Equal(GuessResult.Duplicate, GuessResult);

                [Fact]
                public void NoLivesHaveBeenTaken() => Assert.Equal(Lives, GameState.LivesRemaining);

                [Fact]
                public void PreviousGuessesIncludesGuessOnce() => Assert.Equal(new[] {'h'}, GameState.PreviousGuesses);

                [Fact]
                public void ClueIncludesGuessedLetterInCorrectPlaces() => Assert.Equal(new char?[] {'H', null, null, null, null, null, null}, GameState.Clue);

                [Fact]
                public void GameIsNotWon() => Assert.False(GameState.Won);

                [Fact]
                public void GameIsNotLost() => Assert.False(GameState.Lost);

                [Fact]
                public void GameIsInProgress() => Assert.True(GameState.InProgress);
            }
            
            public class WhenMakingADuplicateIncorrectGuess
            {
                private static (GameState, GuessResult) GuessOutcome
                {
                    get
                    {
                        var game = Game;
                        game.MakeGuess('x');
                        return game.MakeGuess('x');
                    }
                }

                private static GameState GameState => GuessOutcome.Item1;
                private static GuessResult GuessResult => GuessOutcome.Item2;
                
                [Fact]
                public void ResultIndicatesGuessIsDuplicate() => Assert.Equal(GuessResult.Duplicate, GuessResult);

                [Fact]
                public void OneLifeHasBeenTaken() => Assert.Equal(Lives - 1, GameState.LivesRemaining);

                [Fact]
                public void PreviousGuessesIncludesGuessOnce() => Assert.Equal(new[] {'x'}, GameState.PreviousGuesses);

                [Fact]
                public void ClueIsStillCompletelyMasked() => Assert.Equal(new char?[] {null, null, null, null, null, null, null}, GameState.Clue);

                [Fact]
                public void GameIsNotWon() => Assert.False(GameState.Won);

                [Fact]
                public void GameIsNotLost() => Assert.False(GameState.Lost);

                [Fact]
                public void GameIsInProgress() => Assert.True(GameState.InProgress);
            }
            
            public class WhenMakingAnInvalidGuess
            {
                private static (GameState, GuessResult) GuessOutcome => Game.MakeGuess('1');

                private static GameState GameState => GuessOutcome.Item1;
                private static GuessResult GuessResult => GuessOutcome.Item2;
                
                [Fact]
                public void ResultIndicatesGuessIsInvalid() => Assert.Equal(GuessResult.Invalid, GuessResult);

                [Fact]
                public void NoLivesHaveBeenTaken() => Assert.Equal(Lives, GameState.LivesRemaining);

                [Fact]
                public void InvalidGuessIsNotIncludedInPreviousGuesses() => Assert.Empty(GameState.PreviousGuesses);

                [Fact]
                public void ClueIsStillCompletelyMasked() => Assert.Equal(new char?[] {null, null, null, null, null, null, null}, GameState.Clue);

                [Fact]
                public void GameIsNotWon() => Assert.False(GameState.Won);

                [Fact]
                public void GameIsNotLost() => Assert.False(GameState.Lost);

                [Fact]
                public void GameIsInProgress() => Assert.True(GameState.InProgress);
            }
            
            public class WhenMakingTheWinningGuess
            {
                private static IEnumerable<char> Guesses => new[] {'h', 'a', 'n', 'g', 'm'};
                
                private static (GameState, GuessResult) GuessOutcome
                {
                    get
                    {
                        var game = Game;
                        return Guesses.Select(game.MakeGuess).ToList().Last();
                    }
                }

                private static GameState GameState => GuessOutcome.Item1;
                private static GuessResult GuessResult => GuessOutcome.Item2;
                
                [Fact]
                public void ResultIndicatesGameIsOver() => Assert.Equal(GuessResult.GameOver, GuessResult);

                [Fact]
                public void NoLivesHaveBeenTaken() => Assert.Equal(Lives, GameState.LivesRemaining);

                [Fact]
                public void PreviousGuessesIncludesGuess() => Assert.Equal(Guesses, GameState.PreviousGuesses);

                [Fact]
                public void WordIsCompletelyUnmasked() => Assert.Equal("Hangman".OfType<char?>(), GameState.Clue);

                [Fact]
                public void GameIsWon() => Assert.True(GameState.Won);

                [Fact]
                public void GameIsNotLost() => Assert.False(GameState.Lost);

                [Fact]
                public void GameIsOver() => Assert.False(GameState.InProgress);
            }
            
            public class WhenMakingTheLoosingGuess
            {
                private static IEnumerable<char> Guesses => new[] {'q', 'w', 'e', 'r', 't'};
                
                private static (GameState, GuessResult) GuessOutcome
                {
                    get
                    {
                        var game = Game;
                        return Guesses.Select(game.MakeGuess).ToList().Last();
                    }
                }

                private static GameState GameState => GuessOutcome.Item1;
                private static GuessResult GuessResult => GuessOutcome.Item2;
                
                [Fact]
                public void ResultIndicatesGameIsOver() => Assert.Equal(GuessResult.GameOver, GuessResult);

                [Fact]
                public void NoLivesAreLeft() => Assert.Equal(0, GameState.LivesRemaining);

                [Fact]
                public void PreviousGuessesIncludesGuess() => Assert.Equal(Guesses, GameState.PreviousGuesses);

                [Fact]
                public void WordIsCompletelyUnmasked() => Assert.Equal("Hangman".OfType<char?>(), GameState.Clue);

                [Fact]
                public void GameIsNotWon() => Assert.False(GameState.Won);

                [Fact]
                public void GameIsLost() => Assert.True(GameState.Lost);

                [Fact]
                public void GameIsOver() => Assert.False(GameState.InProgress);
            }
        }
    }
}