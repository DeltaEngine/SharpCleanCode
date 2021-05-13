using NUnit.Framework;

namespace EsoLanguages.Tests
{
	public class MiniStringFuckTests
	{
		[Test]
		public void PassingNoCodeItShouldOutputNothing() =>
			Assert.That(MiniStringFuck.MyFirstInterpreter(""), Is.EqualTo(""));

		[Test]
		public void SingleIncrementAndOutputShouldBeByteOne() =>
			Assert.That(MiniStringFuck.MyFirstInterpreter("+."), Is.EqualTo("" + (char)1));

		[Test]
		public void InvalidCode() =>
			Assert.That(() => MiniStringFuck.MyFirstInterpreter("Hello"),
				Throws.InstanceOf<MiniStringFuck.InvalidCharacter>());

		[Test]
		public void Alphabet() =>
			Assert.That(
				MiniStringFuck.MyFirstInterpreter(
					"+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+."),
				Is.EqualTo("ABCDEFGHIJKLMNOPQRSTUVWXYZ"));

		[Test]
		public void HelloWorld() =>
			Assert.That(
				MiniStringFuck.MyFirstInterpreter(
					"++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.+++++++++++++++++++++++++++++.+++++++..+++.+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.+++++++++++++++++++++++++++++++++++++++++++++++++++++++.++++++++++++++++++++++++.+++.++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++.+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++."),
				Is.EqualTo("Hello, World!"));
	}
}