using NUnit.Framework;

namespace BowlingGame.Tests
{
	public class GameTests
	{
		[SetUp]
		public void CreateGame() => game = new Game();
		private Game game;

		[Test]
		public void Gutter()
		{
			RollMany(20, 0);
			Assert.That(game.Score, Is.EqualTo(0));
		}

		private void RollMany(int numberOfRolls, int pinsPerThrow)
		{
			for (int roll = 0; roll < numberOfRolls; roll++)
				game.Throw(pinsPerThrow);
		}

		[Test]
		public void SecondWorstPlayerOfTheWorld()
		{
			game.Throw(3);
			RollMany(19, 0);
			Assert.That(game.Score, Is.EqualTo(3));
		}
		
		[Test]
		public void AllOnes()
		{
			RollMany(20, 1);
			Assert.That(game.Score, Is.EqualTo(20));
		}

		[Test]
		public void Spare()
		{
			game.Throw(3);
			game.Throw(7);
			game.Throw(4);
			RollMany(17, 0);
			Assert.That(game.Score, Is.EqualTo(10 + 4 + 4));
		}
		
		[Test]
		public void Strike()
		{
			game.Throw(10);
			game.Throw(4);
			game.Throw(3);
			RollMany(16, 0);
			Assert.That(game.Score, Is.EqualTo(10 + 7 + 7));
		}
		
		[Test]
		public void PerfectGame()
		{
			RollMany(12, 10);
			Assert.That(game.Score, Is.EqualTo(300));
		}
	}
}