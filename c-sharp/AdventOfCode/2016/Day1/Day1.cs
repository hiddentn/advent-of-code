using AdventOfCode.Common;
using AdventOfCode.Common.Extensions;

namespace AdventOfCode._2016.Day1;

public class Day1Solver : DaySolver
{
	public override string Day => "1";
	public override string Year => "2016";
	public Day1Solver(DaySolverOptions options) : base(options)
	{
	}

	public override string SolvePart1() =>
		InputLines
			.First()
			.Split(", ")
			.Bind(instructions =>
			{
				var direction = 0;
				var x = 0;
				var y = 0;

				foreach (var instruction in instructions)
				{
					var turn = instruction[0].ToString();
					var distance = int.Parse((string)instruction.Substring(1));
					(x, y) = GetMove(x, y, direction, turn, distance);
					direction = GetNewDirection(direction, turn);
				}

				return Math.Abs(x) + Math.Abs(y);
			})
			.ToString();


	public override string SolvePart2() =>
		InputLines
			.First()
			.Split(", ")
			.Bind(instructions =>
			{
				var direction = 0;
				var x = 0;
				var y = 0;
				var visited = new HashSet<(int, int)>();

				foreach (var instruction in instructions)
				{
					var turn = instruction[0].ToString();
					var distance = int.Parse(instruction.Substring(1));
					for (var i = 0; i < distance; i++)
					{
						(x, y) = GetMove(x, y, direction, turn, 1);
						if (!visited.Add((x, y)))
						{
							return Math.Abs(x) + Math.Abs(y);
						}
					}

					direction = GetNewDirection(direction, turn);
				}

				return Math.Abs(x) + Math.Abs(y);
			})
			.ToString();

	private static (int x, int y) GetMove(int x, int y, int direction, string turn, int distance)
	{
		switch (direction)
		{
			case 0:
				switch (turn)
				{
					case "L":
						x -= distance;
						break;
					case "R":
						x += distance;
						break;
				}

				break;
			case 1:
				switch (turn)
				{
					case "L":
						y += distance;
						break;
					case "R":
						y -= distance;
						break;
				}

				break;
			case 2:
				switch (turn)
				{
					case "L":
						x += distance;
						break;
					case "R":
						x -= distance;
						break;
				}

				break;
			case 3:
				switch (turn)
				{
					case "L":
						y -= distance;
						break;
					case "R":
						y += distance;
						break;
				}

				break;
		}

		return (x, y);
	}

	private static int GetNewDirection(int direction, string turn)
	{
		return direction switch
		{
			0 => turn switch
			{
				"L" => 3,
				"R" => 1,
				_ => direction
			},
			1 => turn switch
			{
				"L" => 0,
				"R" => 2,
				_ => direction
			},
			2 => turn switch
			{
				"L" => 1,
				"R" => 3,
				_ => direction
			},
			3 => turn switch
			{
				"L" => 2,
				"R" => 0,
				_ => direction
			},
			_ => throw new UnreachableException("help")
		};
	}
}
