using NUnit.Framework;

namespace EsoLanguages.Tests
{
	public class SmallfuckTests
	{
		[TestCase("00101100", "*", "10101100")]
		[TestCase("00101100", ">*>*", "01001100")]
		[TestCase("00101100", "*>*>*>*>*>*>*>*", "11010011")]
		[TestCase("00101100", "*>*>>*>>>*>*", "11111111")]
		[TestCase("00101100", ">>>>>*<*<<*", "00000000")]
		[TestCase("0000000000000000", "*>>>*>*>>*>>>>>>>*>*>*>*>>>**>>**", "1001101000000111")]
		[TestCase("00101100", "iwmlis *!BOSS 333 ^v$#@", "10101100")]
		[TestCase("0000000000000", "*[>*]", "1111111111111")]
		[TestCase("0000100000000", "*[>[]>*[>*]>*]", "1011010111111")]
		public void ExampleTest(string memory, string code, string expected) =>
			Assert.That(Smallfuck.Interpreter(code, memory), Is.EqualTo(expected));
	}
}