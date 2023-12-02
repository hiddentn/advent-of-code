using AdventOfCode.Common;

namespace AdventOfCode._2021.Day1;

public class Day1Solver : DaySolver
{
	public override string Day => "1";
	public override string Year => "2021";
	public Day1Solver(DaySolverOptions options) : base(options)
	{
	}

	public override string SolvePart1()
	{
		var lines = InputLines
			.Select(int.Parse).ToList();

		var increasedCount = 0;
		for (var i = 0; i < lines.Count - 1; i++)
		{
			if (lines[i] < lines[i + 1])
			{
				increasedCount++;
			}
		}

		return increasedCount.ToString();
	}

	public override string SolvePart2()
	{
		var lines = InputLines
			.Select(int.Parse).ToList();

		var increasedCount = 0;

		for (var i = 1; i < lines.Count - 2; i++)
		{
			var current = lines[i - 1] + lines[i] + lines[i + 1];
			var next = lines[i] + lines[i + 1] + lines[i + 2];
			if (current < next)
			{
				increasedCount++;
			}
		}

		return increasedCount.ToString();
	}
}
