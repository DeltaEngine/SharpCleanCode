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
				int roll = 0;
				for (int frame = 0; frame < 10; frame++)
				{
					// Is Spare?
					if (throws[roll] + throws[roll + 1] == 10)
					{
						sum += 10 + throws[roll];
						roll += 2;
					}
					// TODO: Is Strike
					else
					{
						sum += throws[roll] + throws[roll + 1];
						roll += 2;
					}
				}
				return sum;
			}
		}
	}
}