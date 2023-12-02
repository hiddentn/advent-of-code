using AdventOfCode.Common;

namespace AdventOfCode._2022.Day5;

public class Day5Solver : DaySolver
{
	public override string Day => "5";
	public override string Year => "2022";
	public Day5Solver(DaySolverOptions options) : base(options)
	{
	}

	public override string SolvePart1()
	{
		var stacks = CreateInput();
		var instructions = InputLines.ToList();
		foreach (var instruction in instructions)
		{
			var parts = instruction.Split(' ');

			var from = int.Parse(parts[3]);
			var to = int.Parse(parts[5]);
			var count = int.Parse(parts[1]);

			var fromStack = stacks[from - 1];
			var toStack = stacks[to - 1];

			for (var i = 0; i < count; i++)
			{
				toStack.Push(fromStack.Pop());
			}
		}

		// concat to top items from each stack to get the final result
		return string.Join("", stacks.Select(s => s.Peek()));
	}

	public override string SolvePart2()
	{
		var stacks = CreateInput();
		var instructions = InputLines.ToList();
		foreach (var instruction in instructions)
		{
			var parts = instruction.Split(' ');

			var from = int.Parse(parts[3]);
			var to = int.Parse(parts[5]);
			var count = int.Parse(parts[1]);

			var fromStack = stacks[from - 1];
			var toStack = stacks[to - 1];

			var temp = new Stack<char>();
			for (var i = 0; i < count; i++)
			{
				temp.Push(fromStack.Pop());
			}

			while (temp.Count > 0)
			{
				toStack.Push(temp.Pop());
			}
		}

		return string.Join("", stacks.Select(s => s.Peek()));
	}


	private List<Stack<char>> CreateInput()
	{
		var stacks = new List<Stack<char>>()
		{
			new Stack<char>(),
			new Stack<char>(),
			new Stack<char>(),
			new Stack<char>(),
			new Stack<char>(),
			new Stack<char>(),
			new Stack<char>(),
			new Stack<char>(),
			new Stack<char>(),
		};

		// [P]
		// [L]
		// [M]
		// [N]
		// [W]
		// [V]
		// [B]
		// [H]
		stacks[0].Push('H');
		stacks[0].Push('B');
		stacks[0].Push('V');
		stacks[0].Push('W');
		stacks[0].Push('N');
		stacks[0].Push('M');
		stacks[0].Push('L');
		stacks[0].Push('P');

		//[H]
		//[Q]
		//[M]

		stacks[1].Push('M');
		stacks[1].Push('Q');
		stacks[1].Push('H');

		// [L]
		// [M]
		// [Q]
		// [F]
		// [G]
		// [B]
		// [D]
		// [N]

		stacks[2].Push('N');
		stacks[2].Push('D');
		stacks[2].Push('B');
		stacks[2].Push('G');
		stacks[2].Push('F');
		stacks[2].Push('Q');
		stacks[2].Push('M');
		stacks[2].Push('L');


		// [G]
		// [W]
		// [M]
		// [Q]
		// [F]
		// [T]
		// [Z]

		stacks[3].Push('Z');
		stacks[3].Push('T');
		stacks[3].Push('F');
		stacks[3].Push('Q');
		stacks[3].Push('M');
		stacks[3].Push('W');
		stacks[3].Push('G');


		//[P]
		//[H]
		//[T]
		//[M]

		stacks[4].Push('M');
		stacks[4].Push('T');
		stacks[4].Push('H');
		stacks[4].Push('P');


		// [T]
		// [G]
		// [H]
		// [D]
		// [J]
		// [M]
		// [B]
		// [C]

		stacks[5].Push('C');
		stacks[5].Push('B');
		stacks[5].Push('M');
		stacks[5].Push('J');
		stacks[5].Push('D');
		stacks[5].Push('H');
		stacks[5].Push('G');
		stacks[5].Push('T');


		//[R]
		//[V]
		//[F]
		//[B]
		//[N]
		//[M]

		stacks[6].Push('M');
		stacks[6].Push('N');
		stacks[6].Push('B');
		stacks[6].Push('F');
		stacks[6].Push('V');
		stacks[6].Push('R');


		// [S]
		// [G]
		// [R]
		// [M]
		// [H]
		// [L]
		// [P]


		stacks[7].Push('P');
		stacks[7].Push('L');
		stacks[7].Push('H');
		stacks[7].Push('M');
		stacks[7].Push('R');
		stacks[7].Push('G');
		stacks[7].Push('S');


		// [N]
		// [C]
		// [B]
		// [D]
		// [P]

		stacks[8].Push('P');
		stacks[8].Push('D');
		stacks[8].Push('B');
		stacks[8].Push('C');
		stacks[8].Push('N');

		return stacks;
	}
}
