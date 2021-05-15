using System.Collections.Generic;
using System.Linq;

namespace EsoLanguages
{
	public class Smallfuck
	{
		public static string Interpreter(string code, string tape) =>
			new Smallfuck(tape).Parse(code).Execute();

		private Smallfuck(string tape)
		{
			memory = new bool[tape.Length];
			for (var index = 0; index < tape.Length; index++)
				memory[index] = tape[index] == '1';
		}

		private readonly bool[] memory;
		private int pointerIndex;

		private Smallfuck Parse(string code)
		{
			foreach (var instruction in code)
				instructions.Add(instruction switch
				{
					'>' => Instruction.MovePointerRight,
					'<' => Instruction.MovePointerLeft,
					'*' => Instruction.Flip,
					'[' => Instruction.JumpOver,
					']' => Instruction.JumpBack,
					_ => Instruction.None
				});
			return this;
		}

		private readonly List<Instruction> instructions = new List<Instruction>();

		public enum Instruction
		{
			None,
			MovePointerRight,
			MovePointerLeft,
			Flip,
			JumpOver,
			JumpBack
		}
		
		private string Execute()
		{
			for (instructionIndex = 0; instructionIndex < instructions.Count; instructionIndex++)
			{
				ExecuteInstruction(instructions[instructionIndex]);
				if (pointerIndex >= memory.Length || pointerIndex < 0)
					break;
			}
			return new string(memory.Select(bit=>bit ? '1' : '0').ToArray());
		}

		private void ExecuteInstruction(Instruction instruction)
		{
			switch (instruction)
			{
			case Instruction.MovePointerRight:
				pointerIndex++;
				break;
			case Instruction.MovePointerLeft:
				pointerIndex--;
				break;
			case Instruction.Flip:
				memory[pointerIndex] = !memory[pointerIndex];
				break;
			case Instruction.JumpOver:
				if (!memory[pointerIndex])
					JumpToMatchingBracket(1);
				break;
			case Instruction.JumpBack:
				if (memory[pointerIndex])
					JumpToMatchingBracket(-1);
				break;
			}
		}

		private int instructionIndex;

		private void JumpToMatchingBracket(int direction)
		{
			int numberOfBrackets = direction;
			for (instructionIndex += direction; instructionIndex >= 0; instructionIndex += direction)
			{
				if (instructions[instructionIndex] == Instruction.JumpBack)
					numberOfBrackets--;
				if (instructions[instructionIndex] == Instruction.JumpOver)
					numberOfBrackets++;
				if (numberOfBrackets == 0)
					break;
			}
		}
	}
}