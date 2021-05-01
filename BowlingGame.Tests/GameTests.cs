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
		/*
If on his first try in the frame he knocks down all the pins, this is called a “strike”. His turn is over, and his score for the frame is ten plus the simple total of the pins knocked down in his next two rolls.
		
If in two tries, he fails to knock them all down, his score for that frame is the total number of pins knocked down in his two tries.

If he gets a spare or strike in the last (tenth) frame, the bowler gets to throw one or two more bonus balls, respectively. These bonus throws are taken as part of the same turn. If the bonus throws knock down all the pins, the process does not repeat: the bonus throws are only used to calculate the score of the final frame.
		 */
	}
}