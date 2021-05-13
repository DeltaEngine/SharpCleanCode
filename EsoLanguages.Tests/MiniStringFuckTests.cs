using NUnit.Framework;

namespace EsoLanguages.Tests
{
	public class MiniStringFuckTests
	{
		[Test]
		public void EmptyCommandDoesNotDoAnything() =>
			Assert.That(MiniStringFuck.MyFirstInterpreter(""), Is.EqualTo(""));

		[Test]
		public void OnlyIncrementOnceShouldOutputOne() =>
			Assert.That(MiniStringFuck.MyFirstInterpreter("+."), Is.EqualTo("" + (char)1));

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

		[Test]
		public void InvalidInstructionShouldThrowException() =>
			Assert.That(() => MiniStringFuck.MyFirstInterpreter("/."),
				Throws.InstanceOf<MiniStringFuck.InvalidInstruction>());
	}
}