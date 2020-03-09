using System;
using System.Linq;
using System.Text;

namespace console
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Hangman");
            Console.WriteLine("Press any key to start a game.");
            Console.ReadKey();
            
            var game = new Game("Shenanigans", 10);
            var gameState = game.Start();

            while (gameState.InProgress)
            {
                var maskedWord = gameState.Clue
                    .Aggregate(new StringBuilder(), (s, c) => s.Append(c ?? '_'))
                    .ToString()
                    .TrimEnd();

                Console.WriteLine($"You have {gameState.LivesRemaining} guesses remaining.");
                Console.WriteLine();
                Console.WriteLine($"The word to guess is: {maskedWord}");
                Console.WriteLine($"Your previous guesses: {string.Join(" ", gameState.PreviousGuesses)}");
                Console.Write("Guess a letter > ");

                var guess = Console.ReadKey().KeyChar;
                Console.WriteLine();

                GuessResult result;
                (gameState, result) = game.MakeGuess(guess);

                switch (result)
                {
                    case GuessResult.Invalid:
                        Console.WriteLine($"{guess} is not a letter.");
                        break;
                        
                    case GuessResult.Duplicate:
                        Console.WriteLine($"You have already guessed {guess}.");
                        break;
                    
                    case GuessResult.Incorrect:
                        Console.WriteLine($"Nope, {guess} is not in the word.");
                        break;
                    
                    case GuessResult.Correct:
                        Console.WriteLine("Well done, you have unmasked a letter!");
                        break;
                    
                    case GuessResult.GameOver:
                        Console.WriteLine("Game over!");
                        break;
                }
            }
            
            if (gameState.Won)
            {
                Console.WriteLine("Well done, you unmasked the word!");
            }
            else
            {
                var word = string.Join("", gameState.Clue);
                Console.WriteLine($"You lost. The word was {word}");
            }

        }
    }
}
