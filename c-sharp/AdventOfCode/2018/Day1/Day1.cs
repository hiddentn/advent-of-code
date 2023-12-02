using AdventOfCode.Common;

namespace AdventOfCode._2018.Day1;

public class Day1Solver : DaySolver
{
	public override string Day => "1";
	public override string Year => "2018";
	public Day1Solver(DaySolverOptions options) : base(options)
	{
	}

	public override string SolvePart1() =>
		InputLines
			.Aggregate(0, (acc, frequency) =>
			{
				var sign = frequency[0];
				var value = int.Parse(frequency.Substring(1));
				return sign == '+' ? acc + value : acc - value;
			})
			.ToString();

	public override string SolvePart2()
	{
		var items = InputLines.ToList();
		var frequencies = new HashSet<int>();
		var currentFrequency = 0;
		var index = 0;
		while (true)
		{
			var frequency = items[index];
			var sign = frequency[0];
			var value = int.Parse(frequency.Substring(1));
			currentFrequency = sign == '+' ? currentFrequency + value : currentFrequency - value;
			if (!frequencies.Add(currentFrequency))
			{
				return currentFrequency.ToString();
			}

			frequencies.Add(currentFrequency);
			index = (index + 1) % items.Count;
		}
	}
}
