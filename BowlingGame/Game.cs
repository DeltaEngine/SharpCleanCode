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
					sum = Roll(sum);
				return sum;
			}
		}

		private int roll;

		private int Roll(int sum)
		{
			if (IsSpare)
				sum += 10 + throws[roll += 2];
			else if (IsStrike)
				sum += 10 + throws[++roll] + throws[roll + 1];
			else
				sum += throws[roll++] + throws[roll++];
			return sum;
		}

		private bool IsSpare => throws[roll] + throws[roll + 1] == 10;
		private bool IsStrike => throws[roll] == 10;
	}
}