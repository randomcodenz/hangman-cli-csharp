using System.Collections.Generic;

namespace console
{
    public readonly struct GameState
    {
        public GameState(bool won, bool lost, int livesRemaining, IEnumerable<char?> clue, IEnumerable<char> previousGuesses)
        {
            Won = won;
            Lost = lost;
            LivesRemaining = livesRemaining;
            Clue = clue;
            PreviousGuesses = previousGuesses;
        }

        public bool Won { get; }
        public bool Lost { get; }
        public bool InProgress => !Won && !Lost;
        public int LivesRemaining { get; }
        public IEnumerable<char?> Clue { get; }
        public IEnumerable<char> PreviousGuesses { get; }
        
        public static GameState GameWon(int livesRemaining, IEnumerable<char?> clues, IEnumerable<char> previousGuesses) 
            => new GameState(true, false, livesRemaining, clues, previousGuesses);

        public static GameState GameLost(int livesRemaining, IEnumerable<char?> clues, IEnumerable<char> previousGuesses) 
            => new GameState(false, true, livesRemaining, clues, previousGuesses);
        
        public static GameState GameInProgress(int livesRemaining, IEnumerable<char?> clues, IEnumerable<char> previousGuesses) 
            => new GameState(false, false, livesRemaining, clues, previousGuesses);
    }
}