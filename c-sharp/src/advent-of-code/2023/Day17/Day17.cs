using AdventOfCode.Common;

namespace AdventOfCode._2023.Day17;

public class Day17Solver(DaySolverOptions options) : DaySolver(options)
{
	public override string Day => "17";
	public override string Year => "2023";


	public static int[][] ParseInput(string input)
	{
		return input
			.Split("\n")
			.SkipLast(1)
			.Select(line => line.Split().Select(int.Parse).ToArray())
			.ToArray();
	}


	public override string SolvePart1()
	{
		return "";
	}


	public override string SolvePart2()
	{
		return "";
	}
}
