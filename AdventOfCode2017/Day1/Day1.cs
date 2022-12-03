using System.Diagnostics;
using AdventOfCode.Abstractions;

namespace AdventOfCode2017.Day1;

public class Day1Solver : DaySolver
{
	public Day1Solver(DaySolverOptions options) : base(options)
	{
	}

	public override string SolvePart1()
	{
		var items = InputLines
			.First()
			.Select(s => int.Parse(s.ToString()))
			.ToList();

		return items.Where((t, i) => t == items[(i + 1) % items.Count]).Sum().ToString();
	}

	public override string SolvePart2()
	{
		var items = InputLines
			.First()
			.Select(s => int.Parse(s.ToString()))
			.ToList();

		return items.Where((t, i) => t == items[(i + items.Count / 2) % items.Count]).Sum().ToString();
	}
}
