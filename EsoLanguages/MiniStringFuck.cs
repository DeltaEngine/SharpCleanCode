using System;
using System.Collections.Generic;

namespace EsoLanguages
{
	public class MiniStringFuck
	{
		public static string MyFirstInterpreter(string code) =>
			new MiniStringFuck().Parse(code).Execute();

		private MiniStringFuck Parse(string code)
		{
			foreach (var instruction in code)
				instructions.Add(instruction switch
				{
					'+' => Instruction.Increase,
					'.' => Instruction.Output,
					_ => throw new InvalidInstruction()
				});
			return this;
		}

		private readonly List<Instruction> instructions = new List<Instruction>();

		public enum Instruction
		{
			Increase,
			Output
		}

		public class InvalidInstruction : Exception { }
		
		private string Execute()
		{
			byte memory = 0;
			string result = "";
			foreach (var instruction in instructions)
				if (instruction == Instruction.Increase)
					memory++;
				else if (instruction == Instruction.Output)
					result += (char)memory;
			return result;
		}
	}
}