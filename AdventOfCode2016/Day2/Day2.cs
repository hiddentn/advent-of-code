using AdventOfCode.Abstractions;

namespace AdventOfCode2016.Day2;

public class Day2Solver : DaySolver
{
	public Day2Solver(DaySolverOptions options) : base(options)
	{
	}

	private static string[][] KeyPad =
	{
		new[] { "1", "2", "3" },
		new[] { "4", "5", "6" },
		new[] { "7", "8", "9" }
	};

	private static string[][] KeyPad2 =
	{
		new[] { null, null, "1", null, null },
		new[] { null, "2", "3", "4", null },
		new[] { "5", "6", "7", "8", "9" },
		new[] { null, "A", "B", "C", null },
		new[] { null, null, "D", null, null }
	};


	public override string SolvePart1() =>
		InputLines
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


	public override string SolvePart2() =>
		InputLines
			.Select(line => line.Aggregate((x: 1, y: 1), (pos, c) => c switch
			{
				'U' => KeyPad2[pos.y - 1][pos.x] != null ? (pos.x, pos.y--) : (pos.x, pos.y),
				'D' => KeyPad2[pos.y + 1][pos.x] != null ? (pos.x, pos.y++) : (pos.x, pos.y),
				'L' => KeyPad2[pos.y][pos.x - 1] != null ? (pos.x--, pos.y) : (pos.x, pos.y),
				'R' => KeyPad2[pos.y][pos.x + 1] != null ? (pos.x++, pos.y) : (pos.x, pos.y),
				_ => throw new InvalidOperationException()
			}))
			.Select(pos => KeyPad2[pos.y][pos.x])
			.Aggregate((a, b) => a + b);
}
