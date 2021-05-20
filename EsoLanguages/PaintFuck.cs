using System.Collections.Generic;
using System.Linq;

namespace EsoLanguages
{
	public class PaintFuck
	{
		public static string Interpret(string code, int iterations, int width, int height) =>
			new PaintFuck(width, height).Parse(code).Execute(iterations);

		private PaintFuck(int width, int height)
		{
			this.width = width;
			this.height = height;
			memory = new bool[height, width];
		}
		
		private readonly int width;
		private readonly int height;
		private readonly bool[,] memory;
		private int pointerX;
		private int pointerY;

		private PaintFuck Parse(string code)
		{
			foreach (var instruction in code)
				instructions.Add(instruction switch
				{
					'n' => Instruction.MovePointerUp,
					's' => Instruction.MovePointerDown,
					'e' => Instruction.MovePointerRight,
					'w' => Instruction.MovePointerLeft,
					'*' => Instruction.Flip,
					'[' => Instruction.JumpOver,
					']' => Instruction.JumpBack,
					_ => Instruction.None
				});
			return this;
		}

		private List<Instruction> instructions = new List<Instruction>();

		public enum Instruction
		{
			None,
			MovePointerUp,
			MovePointerDown,
			MovePointerRight,
			MovePointerLeft,
			Flip,
			JumpOver,
			JumpBack
		}
		
		private string Execute(int iterations)
		{
			int counter = 0;
			instructions = instructions.Where(code => code != Instruction.None).ToList();
			for (instructionIndex = 0;
				instructionIndex < instructions.Count && counter < iterations;
				instructionIndex++, counter++)
				ExecuteInstruction(instructions[instructionIndex]);
			return FormatOutput();
		}

		private void ExecuteInstruction(Instruction instruction)
		{
			//Console.WriteLine(instruction+", pointerX="+pointerX+", pointerY="+pointerY);
			switch (instruction)
			{
			case Instruction.MovePointerUp:
				pointerY--;
				if (pointerY < 0)
					pointerY = height - 1;
				break;
			case Instruction.MovePointerDown:
				pointerY = (pointerY + 1) % height;
				break;
			case Instruction.MovePointerRight:
				pointerX = (pointerX + 1) % width;
				break;
			case Instruction.MovePointerLeft:
				pointerX--;
				if (pointerX < 0)
					pointerX = width - 1;
				break;
			case Instruction.Flip:
				memory[pointerY, pointerX] = !memory[pointerY, pointerX];
				break;
			case Instruction.JumpOver:
				if (!memory[pointerY, pointerX])
					JumpToMatchingBracket(1);
				break;
			case Instruction.JumpBack:
				if (memory[pointerY, pointerX])
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

		private string FormatOutput()
		{
			var result = "";
			for (int y = 0; y < height; y++)
			{
				if (y > 0)
					result += "\r\n";
				for (int x = 0; x < width; x++)
					result += memory[y, x]
						? "1"
						: "0";
			}
			return result;
		}
	}
}