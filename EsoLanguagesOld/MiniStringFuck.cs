using System;
using System.Collections.Generic;

namespace EsoLanguages
{
	public class MiniStringFuck
	{
		public static string MyFirstInterpreter(string code)
		{
			var interpreter = new MiniStringFuck();
			interpreter.ParseInstructions(code);
			foreach (var instruction in interpreter.instructions)
				interpreter.Execute(instruction);
			return interpreter.output;
		}

		private  void ParseInstructions(string code)
		{
			foreach (var instruction in code)
				instructions.Add(Parse(instruction));
		}
		
		private static Instruction Parse(char instruction) =>
			instruction switch
			{
				'+' => Instruction.Increment,
				'.' => Instruction.Output,
				_ => throw new InvalidCharacter()
			};

		public enum Instruction
		{
			Increment,
			Output
		}

		public class InvalidCharacter : Exception { }

		private readonly List<Instruction> instructions = new();

		private void Execute(Instruction instruction)
		{
			if (instruction == Instruction.Increment)
				memory++;
			else if (instruction == Instruction.Output)
				output += (char)memory;
		}

		private byte memory;
		private string output = "";
	}
}