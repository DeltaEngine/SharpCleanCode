using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EsoLanguages
{
	public class Boolfuck
	{
		public static string Interpret(string code, string input) =>
			new Boolfuck(input).Parse(code).Execute();

		private Boolfuck(string tape)
		{
			var inputBytes = tape.ToCharArray().Select(letter => (byte)letter).ToArray();
			input = new BitArray(inputBytes);
			memory = new BitArray(MemorySize);
			pointerIndex = memory.Length / 2;
			output = new BitArray(MemorySize);
		}

		private readonly BitArray input;
		private const int MemorySize = 10000;
		private readonly BitArray memory;
		private int pointerIndex;
		private readonly BitArray output;

		private Boolfuck Parse(string code)
		{
			foreach (var instruction in code)
				instructions.Add(instruction switch
				{
					'+' => Instruction.Flip,
					',' => Instruction.ReadBit,
					';' => Instruction.OutputBit,
					'<' => Instruction.MovePointerLeft,
					'>' => Instruction.MovePointerRight,
					'[' => Instruction.JumpOver,
					']' => Instruction.JumpBack,
					_ => Instruction.None
				});
			return this;
		}

		private readonly List<Instruction> instructions = new List<Instruction>();

		private enum Instruction
		{
			None,
			Flip,
			ReadBit,
			OutputBit,
			MovePointerLeft,
			MovePointerRight,
			JumpOver,
			JumpBack
		}

		private string Execute()
		{
			for (instructionIndex = 0; instructionIndex < instructions.Count; instructionIndex++)
				ExecuteInstruction(instructions[instructionIndex]);
			var outputBytes = new byte[output.Length / 8];
			output.CopyTo(outputBytes, 0);
			return new string(outputBytes.Select(letter => (char)letter).
				Take((outputLength + 7) / 8).ToArray());
		}

		// ReSharper disable once MethodTooLong
		private void ExecuteInstruction(Instruction instruction)
		{
			switch (instruction)
			{
			case Instruction.Flip:
				memory[pointerIndex] = !memory[pointerIndex];
				break;
			case Instruction.ReadBit:
				memory[pointerIndex] = inputPosition < input.Length && input[inputPosition++];
				break;
			case Instruction.OutputBit:
				output[outputLength++] = memory[pointerIndex];
				break;
			case Instruction.MovePointerLeft:
				pointerIndex--;
				break;
			case Instruction.MovePointerRight:
				pointerIndex++;
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

		private int inputPosition;
		private int outputLength;
		private int instructionIndex;

		private void JumpToMatchingBracket(int direction)
		{
			var numberOfBrackets = direction;
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