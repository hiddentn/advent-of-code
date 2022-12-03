using AdventOfCode.Abstractions;

namespace AdventOfCode2022.Day3;

public class Day3Solver : DaySolver
{
	public Day3Solver(DaySolverOptions options) : base(options)
	{
	}

	public override string SolvePart1() =>
		InputLines
			.SelectMany(line =>
				line
					.SplitBy(line.Length / 2)
					.Intersect()
			)
			.Sum(x => char.IsUpper(x) ? (x - 'A' + 27) : (x - 'a' + 1))
			.ToString();

	public override string SolvePart2() =>
		InputLines
			.Chunk(3)
			.SelectMany(group =>
				group
					.Intersect()
			)
			.Sum(x => char.IsUpper(x) ? (x - 'A' + 27) : (x - 'a' + 1))
			.ToString();
}
