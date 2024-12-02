using AdventOfCode.Common;

namespace AdventOfCode._2024.Day2;

public class Day2Solver(DaySolverOptions options) : DaySolver(options)
{
	public override string Day => "2";
	public override string Year => "2024";


	private List<List<int>> Parse()
	{
		var reports = new List<List<int>>();
		foreach (var line in InputLines)
		{
			// the parts are separated by any number of spaces
			var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			reports.Add(parts.Select(int.Parse).ToList());
		}

		return reports;
	}

	private static bool IsSafe(List<int> report)
	{
		var isAllIncreasing = report.Zip(report.Skip(1), (x, y) => x <= y).All(x => x);
		var isAllDecreasing = report.Zip(report.Skip(1), (x, y) => x >= y).All(x => x);
		var diffs = report.Zip(report.Skip(1), (x, y) => Math.Abs(x - y)).ToList();

		var diffMaxThree = diffs.All(x => x <= 3);
		var diffMinOne = diffs.All(x => x >= 1);

		return (isAllIncreasing || isAllDecreasing) && diffMaxThree && diffMinOne;
	}

	public override string SolvePart1() => Parse().Count(IsSafe).ToString();


	private static bool IsSafeWithOneRemoval(List<int> report)
	{
		for (var i = 0; i < report.Count; i++)
		{
			var newReport = report.ToList();
			newReport.RemoveAt(i);

			if (IsSafe(newReport))
			{
				return true;
			}
		}

		return false;
	}

	public override string SolvePart2() => Parse().Count(IsSafeWithOneRemoval).ToString();
}
