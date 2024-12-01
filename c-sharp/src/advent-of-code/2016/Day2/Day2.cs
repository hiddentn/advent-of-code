using AdventOfCode.Common;

namespace AdventOfCode._2016.Day2;

public class Day2Solver(DaySolverOptions options) : DaySolver(options)
{
	private static readonly string[][] KeyPad =
	{
		new[] { "1", "2", "3" },
		new[] { "4", "5", "6" },
		new[] { "7", "8", "9" }
	};

	private static readonly string[][] KeyPad2 =
	{
		new[] { null, null, "1", null, null },
		new[] { null, "2", "3", "4", null },
		new[] { "5", "6", "7", "8", "9" },
		new[] { null, "A", "B", "C", null },
		new[] { null, null, "D", null, null }
	};

	public override string Day => "2";
	public override string Year => "2016";


	public override string SolvePart1()
	{
		return InputLines
			.Select(line => line.Aggregate((x: 1, y: 1), (pos, c) => c switch
			{
				'U' => (pos.x, Math.Max(0, pos.y - 1)),
				'D' => (pos.x, Math.Min(2, pos.y + 1)),
				'L' => (Math.Max(0, pos.x - 1), pos.y),
				'R' => (Math.Min(2, pos.x + 1), pos.y),
				_ => throw new InvalidOperationException()
			}))
			.Select(pos => KeyPad[pos.y][pos.x])
			.Aggregate((a, b) => a + b);
	}


	public override string SolvePart2()
	{
		var positions = new List<(int x, int y)>();

		foreach (var line in InputLines)
		{
			var pos = (x: 0, y: 2);

			foreach (var c in line)
			{
				(int x, int y) newPos = c switch
				{
					'U' => (pos.x, Math.Max(0, pos.y - 1)),
					'D' => (pos.x, Math.Min(4, pos.y + 1)),
					'L' => (Math.Max(0, pos.x - 1), pos.y),
					'R' => (Math.Min(4, pos.x + 1), pos.y),
					_ => throw new InvalidOperationException()
				};

				if (KeyPad2[newPos.y][newPos.x] != null) pos = newPos;
			}

			positions.Add(pos);
		}


		var keys = positions.Select(pos => KeyPad2[pos.y][pos.x]).ToList();
		return keys.Aggregate((a, b) => a + b);
	}
}
