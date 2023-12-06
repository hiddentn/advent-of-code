using AdventOfCode.Common;

namespace AdventOfCode._2023.Day6;

public class Day6Solver : DaySolver
{
	public Day6Solver(DaySolverOptions options) : base(options)
	{
	}

	public override string Day => "6";
	public override string Year => "2023";


	public override string SolvePart1()
	{

		return "";
		var input = InputLines.Select(s => s.Split(":")[1]).Select(s => s.Trim())
			.Select(s => s.Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Replace("  ", " ").Split(" ").Select(int.Parse).ToList()).ToList();

		var times = input[0];
		var distances = input[1];
		var value = 1;

		for (int i = 0; i < times.Count; i++)
		{
			var count = 0;
			var time = times[i];

			var distance = distances[i];
			for (int j = 0; j < time; j++)
			{
				var traveled = (time - j) * j;
				if (traveled >distance)
				{
					count++;
				}
			}

			if (count > 1)
				value *= count;
		}


		return value.ToString();
	}


	public override string SolvePart2()
	{
		var input = InputLines
			.Select(s => s.Split(":")[1])
			.Select(s => s.Trim())
			.Select(s => s.Replace(" ", ""))
			.Select(long.Parse)
			.ToList();

		var time = input[0];
		var distance = input[1];


		long count = 0;

		for (long j = 0; j < time; j++)
		{
			var traveled = (time - j) * j;
			if (traveled >distance)
			{
				count++;
			}
		}
		return count.ToString();
	}
}
