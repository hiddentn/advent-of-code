using AdventOfCode.Common;

namespace AdventOfCode._2015.Day2;

public class Day2Solver : DaySolver
{
	public Day2Solver(DaySolverOptions options) : base(options)
	{
	}

	public override string Day => "2";
	public override string Year => "2015";

	public override string SolvePart1()
	{
		return InputLines
			.Select(s => s.Split('x').Select(int.Parse).ToList())
			.Select(s => (s[0], s[1], s[2]))
			.Select(dims =>
			{
				var (l, w, h) = dims;
				var sides = new[] { l * w, w * h, h * l };
				return 2 * sides.Sum() + sides.Min();
			})
			.Sum()
			.ToString();
	}

	public override string SolvePart2()
	{
		return InputLines
			.Select(s => s.Split('x').Select(int.Parse).ToList())
			.Select(s => (s[0], s[1], s[2]))
			.Select(dims =>
			{
				var (l, w, h) = dims;
				var ribbon = new[] { l, w, h }.OrderBy(x => x).Take(2).Select(x => x * 2).Sum();
				var bow = l * w * h;
				return ribbon + bow;
			})
			.Sum()
			.ToString();
	}
}
