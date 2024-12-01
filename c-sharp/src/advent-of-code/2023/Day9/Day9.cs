using AdventOfCode.Common;

namespace AdventOfCode._2023.Day9;

public record Seq(IEnumerable<int> History)
{
	private static int NextValue(IReadOnlyList<int> items)
	{
		if (items.All(x => x == 0)) return 0;

		var differences = new int[items.Count - 1];
		for (var i = 0; i < items.Count - 1; i++)
		{
			differences[i] = items[i + 1] - items[i];
		}

		return items[^1] + NextValue(differences);
	}

	private static int PrevValue(IReadOnlyList<int> items)
	{
		if (items.All(x => x == 0)) return 0;

		var differences = new int[items.Count - 1];
		for (var i = 0; i < items.Count - 1; i++)
		{
			differences[i] = items[i + 1] - items[i];
		}

		return items[0] - PrevValue(differences);
	}

	public int GetNextNumber() => NextValue(History.ToList());
	public int GetPreviousNumber() => PrevValue(History.ToList());
};

public class Day9Solver(DaySolverOptions options) : DaySolver(options)
{
	public override string Day => "9";
	public override string Year => "2023";


	private static IEnumerable<Seq> ParseInput(IEnumerable<string> input) =>
		input.Select(line => new Seq(line.Split(' ').Select(int.Parse)));


	public override string SolvePart1() =>
		ParseInput(InputLines)
			.Select(s => s.GetNextNumber())
			.Sum()
			.ToString();


	public override string SolvePart2() =>
		ParseInput(InputLines)
			.Select(s => s.GetPreviousNumber())
			.Sum()
			.ToString();
}
