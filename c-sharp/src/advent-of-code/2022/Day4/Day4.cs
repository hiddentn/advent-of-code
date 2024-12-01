using AdventOfCode.Common;

namespace AdventOfCode._2022.Day4;

public class Day4Solver(DaySolverOptions options) : DaySolver(options)
{
	public override string Day => "4";
	public override string Year => "2022";

	public override string SolvePart1()
	{
		return InputLines
			.Select(s => s.Split(','))
			.Where(s => PairsInclude(s))
			.Count()
			.ToString();
	}

	private bool PairsInclude(string[] strings)
	{
		var one = strings[0].Split("-");
		var two = strings[1].Split("-");
		var oneStart = int.Parse(one[0]);
		var oneEnd = int.Parse(one[1]);

		var twoStart = int.Parse(two[0]);
		var twoEnd = int.Parse(two[1]);

		// check if one is in two or vice versa
		return (oneStart >= twoStart && oneEnd <= twoEnd) || (twoStart >= oneStart && twoEnd <= oneEnd);
	}

	public override string SolvePart2()
	{
		return InputLines
			.Select(s => s.Split(','))
			.Where(s => PairsIntercect(s))
			.Count()
			.ToString();
	}


	private bool PairsIntercect(string[] strings)
	{
		var one = strings[0].Split("-");
		var two = strings[1].Split("-");
		var oneStart = int.Parse(one[0]);
		var oneEnd = int.Parse(one[1]);

		var twoStart = int.Parse(two[0]);
		var twoEnd = int.Parse(two[1]);

		// check if one is in two or vice versa
		return (oneStart >= twoStart && oneStart <= twoEnd) || (twoStart >= oneStart && twoStart <= oneEnd);
	}
}
