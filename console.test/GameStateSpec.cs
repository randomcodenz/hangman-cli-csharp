using System.Linq;
using Xunit;

namespace console.test
{
    public class GameStateSpec
    {
        public class GameWon
        {
            [Fact]
            public void TheGameIsWon()
            {
                var gameState = GameState.GameWon(5, Enumerable.Empty<char?>(), Enumerable.Empty<char>());
                Assert.True(gameState.Won);
            }

            [Fact]
            public void TheGameIsNotLost()
            {
                var gameState = GameState.GameWon(5, Enumerable.Empty<char?>(), Enumerable.Empty<char>());
                Assert.False(gameState.Lost);
            }
            
            [Fact]
            public void TheGameIsOver()
            {
                var gameState = GameState.GameWon(5, Enumerable.Empty<char?>(), Enumerable.Empty<char>());
                Assert.False(gameState.InProgress);
            }
        }

        public class GameLost
        {
            [Fact]
            public void TheGameIsNotWon()
            {
                var gameState = GameState.GameLost(5, Enumerable.Empty<char?>(), Enumerable.Empty<char>());
                Assert.False(gameState.Won);
            }
            
            [Fact]
            public void TheGameIsLost()
            {
                var gameState = GameState.GameLost(5, Enumerable.Empty<char?>(), Enumerable.Empty<char>());
                Assert.True(gameState.Lost);
            }
            
            [Fact]
            public void TheGameIsOver()
            {
                var gameState = GameState.GameLost(5, Enumerable.Empty<char?>(), Enumerable.Empty<char>());
                Assert.False(gameState.InProgress);
            }
        }

        public class GameInProgress
        {
            [Fact]
            public void TheGameIsNotWon()
            {
                var gameState = GameState.GameInProgress(5, Enumerable.Empty<char?>(), Enumerable.Empty<char>());
                Assert.False(gameState.Won);
            }
            
            [Fact]
            public void TheGameIsNotLost()
            {
                var gameState = GameState.GameInProgress(5, Enumerable.Empty<char?>(), Enumerable.Empty<char>());
                Assert.False(gameState.Lost);
            }
            
            [Fact]
            public void TheGameIsNotOver()
            {
                var gameState = GameState.GameInProgress(5, Enumerable.Empty<char?>(), Enumerable.Empty<char>());
                Assert.True(gameState.InProgress);
            }
        }
    }
}