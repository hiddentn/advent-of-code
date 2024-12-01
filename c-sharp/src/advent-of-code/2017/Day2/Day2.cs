using AdventOfCode.Common;

namespace AdventOfCode._2017.Day2;

public class Day2Solver(DaySolverOptions options) : DaySolver(options)
{
	public override string Day => "2";
	public override string Year => "2017";

	public override string SolvePart1()
	{
		return InputLines
			.Select(line => line.Split('\t'))
			.Select(line => line.Select(int.Parse).ToList())
			.Select(numbers => numbers.Max() - numbers.Min())
			.Sum()
			.ToString();
	}

	public override string SolvePart2()
	{
		return InputLines
			.Select(line => line.Split('\t'))
			.Select(line => line.Select(int.Parse).ToList())
			.Select(numbers =>
			{
				foreach (var x in numbers)
				foreach (var y in numbers.Where(y => x != y && x % y == 0))
					return x / y;

				throw new UnreachableException("help");
			})
			.Sum()
			.ToString();
	}
}
