using AdventOfCode.Common;

namespace AdventOfCode._2022.Day1;

public class Day1Solver(DaySolverOptions options) : DaySolver(options)
{
	public override string Day => "1";
	public override string Year => "2022";

	public override string SolvePart1()
	{
		return InputLines
			.Split(x => x == "")
			.Select(s => s.Select(int.Parse).Sum())
			.Max()
			.ToString();
	}

	public override string SolvePart2()
	{
		return InputLines
			.Split(x => x == "")
			.Select(s => s.Select(int.Parse).Sum())
			.OrderDescending()
			.Take(3)
			.Sum()
			.ToString();
	}
}
