using AdventOfCode.Common;

namespace AdventOfCode._2023.Day14;

public class Day14Solver(DaySolverOptions options) : DaySolver(options)
{
	public override string Day => "14";
	public override string Year => "2023";

	private static char[][] ParseInput(string input)
	{
		return input.Split("\n").SkipLast(1).Select(line => line.ToCharArray()).ToArray();
	}

	private static char[][] TiltNorth(char[][] platform)
	{
		for (var i = 0; i < platform.Length; i++)
		{
			var row = platform[i];
			for (var j = 0; j < row.Length; j++)
			{
				var cell = row[j];
				if (cell == 'O')
				{
					var index = i;
					while (index > 0 && platform[index - 1][j] == '.')
					{
						platform[index - 1][j] = 'O';
						platform[index][j] = '.';
						index--;
					}
				}
			}
		}

		return platform;
	}

	private static char[][] TiltSouth(char[][] platform)
	{
		for (var i = platform.Length - 1; i >= 0; i--)
		{
			var row = platform[i];
			for (var j = 0; j < row.Length; j++)
			{
				var cell = row[j];
				if (cell == 'O')
				{
					var index = i;
					while (index < platform.Length - 1 && platform[index + 1][j] == '.')
					{
						platform[index + 1][j] = 'O';
						platform[index][j] = '.';
						index++;
					}
				}
			}
		}

		return platform;
	}

	private static char[][] TiltEast(char[][] platform)
	{
		for (var i = 0; i < platform.Length; i++)
		{
			var row = platform[i];
			for (var j = row.Length - 1; j >= 0; j--)
			{
				var cell = row[j];
				if (cell == 'O')
				{
					var index = j;
					while (index < row.Length - 1 && platform[i][index + 1] == '.')
					{
						platform[i][index + 1] = 'O';
						platform[i][index] = '.';
						index++;
					}
				}
			}
		}

		return platform;
	}

	private static char[][] TiltWest(char[][] platform)
	{
		for (var i = 0; i < platform.Length; i++)
		{
			var row = platform[i];
			for (var j = 0; j < row.Length; j++)
			{
				var cell = row[j];
				if (cell == 'O')
				{
					var index = j;
					while (index > 0 && platform[i][index - 1] == '.')
					{
						platform[i][index - 1] = 'O';
						platform[i][index] = '.';
						index--;
					}
				}
			}
		}

		return platform;
	}

	private static char[][] Cycle(char[][] platform)
	{
		return TiltEast(TiltSouth(TiltWest(TiltNorth(platform))));
	}

	private static char[][] Cycle(char[][] platform, int count)
	{
		var cache = new Dictionary<string, int>();
		for (var currentCycleIndex = 0; currentCycleIndex < count; currentCycleIndex++)
		{
			platform = Cycle(platform);
			var key = string.Join("", platform.SelectMany(row => row));
			if (cache.TryGetValue(key, out var cycleIndex))
			{
				var cycleLength = currentCycleIndex - cycleIndex;
				var cyclesLeft = count - currentCycleIndex;
				var cyclesToSkip = (cyclesLeft / cycleLength);
				currentCycleIndex += cyclesToSkip * cycleLength;
				cache.Clear();
			}
			else
			{
				cache[key] = currentCycleIndex;
			}
		}

		return platform;
	}

	private static int SumWeights(char[][] platform)
	{
		var weights = new List<int>();
		for (var i = 0; i < platform.Length; i++)
		{
			for (var j = 0; j < platform[0].Length; j++)
			{
				var cell = platform[i][j];
				if (cell == 'O')
				{
					weights.Add(platform.Length - i);
				}
			}
		}

		return weights.Sum();
	}

	public override string SolvePart1() => SumWeights(TiltNorth(ParseInput(Input))).ToString();

	public override string SolvePart2() => SumWeights(Cycle(ParseInput(Input), 1000000000)).ToString();
}
