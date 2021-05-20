using NUnit.Framework;

namespace EsoLanguages.Tests
{
	public class PaintFuckTests
	{
		private const string PaintP = "*e*e*e*es*es*ws*ws*w*w*w*n*n*n*ssss*s*s*s*";

		[TestCase("*", 1, 1, 1, "1")]
		[TestCase("////", 1, 1, 1, "0")]
		[TestCase("*", 1, 2, 2, @"
10
00")]
		[TestCase("es*", 3, 2, 2, @"
00
01")]
		[TestCase("wn*", 3, 2, 2, @"
00
01")]
		[TestCase(PaintP, 0, 2, 2, @"
00
00")]
		[TestCase(PaintP, 7, 6, 9, @"
111100
000000
000000
000000
000000
000000
000000
000000
000000")]
		[TestCase(PaintP, 100, 6, 9, @"
111100
100010
100001
100010
111100
100000
100000
100000
100000")]
		[TestCase("*[e*]", 7, 4, 1, "1110")]
		[TestCase("[*]", 7, 4, 1, "0000")]
		public void Calculate2DMap(string code, int iterations, int width, int height,
			string expected) =>
			Assert.That(PaintFuck.Interpret(code, iterations, width, height), Is.EqualTo(expected.TrimStart()));
	}
}