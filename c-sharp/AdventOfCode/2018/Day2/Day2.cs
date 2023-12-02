using AdventOfCode.Common;

namespace AdventOfCode._2018.Day2;

public class Day2Solver : DaySolver
{
	public Day2Solver(DaySolverOptions options) : base(options)
	{
	}

	public override string Day => "2";
	public override string Year => "2018";

	public override string SolvePart1()
	{
		var counts = InputLines
			.Select(line =>
			{
				var groups = line.GroupBy(c => c).Select(g => g.Count());
				(var twos, var threes) = (groups.Contains(2) ? 1 : 0, groups.Contains(3) ? 1 : 0);
				return (twos, threes);
			})
			.Aggregate((twos: 0, threes: 0), (acc, next) => (acc.twos + next.twos, acc.threes + next.threes));
		return (counts.twos * counts.threes).ToString();
	}

	public override string SolvePart2()
	{
		var items = InputLines.ToList();
		foreach (var a in items)
		foreach (var b in items)
			if (a != b)
			{
				var diff = a.Zip(b, (x, y) => x == y).Count(x => !x);
				if (diff <= 1)
					return new string(a.Zip(b, (x, y) => (x, y)).Where(x => x.x == x.y).Select(x => x.x).ToArray());
			}

		throw new NotImplementedException();
	}
}
