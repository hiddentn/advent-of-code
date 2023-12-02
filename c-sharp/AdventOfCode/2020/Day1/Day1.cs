using AdventOfCode.Common;

namespace AdventOfCode._2020.Day1;

public class Day1Solver : DaySolver
{
	public Day1Solver(DaySolverOptions options) : base(options)
	{
	}

	public override string Day => "1";
	public override string Year => "2020";

	public override string SolvePart1()
	{
		var combo = InputLines
			.Select(int.Parse)
			.SelectMany(x => InputLines.Select(int.Parse).Select(y => (x, y)))
			.First(tuple => tuple.x + tuple.y == 2020);
		return (combo.x * combo.y).ToString();
	}


	public override string SolvePart2()
	{
		var combo = InputLines
			.Select(int.Parse)
			.SelectMany(x => InputLines.Select(int.Parse).Select(y => (x, y)))
			.SelectMany(tuple => InputLines.Select(int.Parse).Select(z => (tuple.x, tuple.y, z)))
			.First(tuple => tuple.x + tuple.y + tuple.z == 2020);
		return (combo.x * combo.y * combo.z).ToString();
	}
}
