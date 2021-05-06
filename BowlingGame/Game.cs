using System.Collections.Generic;

namespace BowlingGame
{
	public class Game
	{
		public void Throw(int pins) => throws.Add(pins);
		private readonly List<int> throws = new();

		public int Score
		{
			get
			{
				int sum = 0;
				for (int frame = 0; frame < 10; frame++)
					sum += Roll();
				return sum;
			}
		}

		private int roll;

		private int Roll() =>
			IsSpare
				? 10 + throws[roll += 2]
				: IsStrike
					? 10 + throws[++roll] + throws[roll + 1]
					: throws[roll++] + throws[roll++];

		private bool IsSpare => throws[roll] + throws[roll + 1] == 10;
		private bool IsStrike => throws[roll] == 10;
	}
}