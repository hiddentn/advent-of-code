using AdventOfCode.Common;

namespace AdventOfCode._2023.Day13;


public class Day13Solver(DaySolverOptions options) : DaySolver(options)
{
	public record Pattern(char[][] Grid);

	public override string Day => "13";
	public override string Year => "2023";

	public static List<Pattern> ParsePatterns(IEnumerable<string> input)
	{
		var patterns = new List<Pattern>();
		var grid = new List<char[]>();
		foreach (var line in input)
		{
			if (string.IsNullOrWhiteSpace(line))
			{
				patterns.Add(new Pattern(grid.ToArray()));
				grid.Clear();
				continue;
			}

			grid.Add(line.ToCharArray());
		}

		patterns.Add(new Pattern(grid.ToArray()));
		return patterns;
	}

	private int GetHorizontalMatches(Pattern pattern)
	{
		var grid = pattern.Grid;
		// find 2 horizontal matches near each other
		for (var i = 0; i < grid.Length - 1; i++)
		{
			var row = grid[i];
			var nextRow = grid[i + 1];
			// check if the 2 rows are the same or not
			if (row.SequenceEqual(nextRow))
			{
				var count = 1;
				for (var j = 1; j <= i; j++)
				{
					var r1 = i - j;
					var r2 = i + 1 + j;

					if (r2 < grid.Length)
					{
						var row1 = grid[r1];
						var row2 = grid[r2];
						if (row1.SequenceEqual(row2))
						{
							count++;
						}
						else
						{
							return 0;
						}
					}
					else
					{
						count++;
					}
				}

				return count;
			}
		}

		return 0;
	}

	private int GetVerticalMatches(Pattern pattern)
	{
		var grid = pattern.Grid;
		// find 2 vertical matches near each other
		for (var i = 0; i < grid[0].Length - 1; i++)
		{
			var column = grid.Select(row => row[i]).ToArray();
			var nextColumn = grid.Select(row => row[i + 1]).ToArray();
			// check if the 2 rows are the same or not
			if (column.SequenceEqual(nextColumn))
			{
				var count = 1;
				for (var j = 1; j <= i; j++)
				{
					var r1 = i - j;
					var r2 = i + 1 + j;

					if (r2 < grid[0].Length)
					{
						var column1 = grid.Select(row => row[r1]).ToArray();
						var column2 = grid.Select(row => row[r2]).ToArray();
						if (column1.SequenceEqual(column2))
						{
							count++;
						}
						else
						{
							return 0;
						}
					}
					else
					{
						count++;
					}
				}

				return count;
			}
		}

		return 0;
	}

	public override string SolvePart1()
	{
		var patterns = ParsePatterns(InputLines);
		var sum = 0;
		foreach (var pattern in patterns)
		{
			var verticalCount = GetVerticalMatches(pattern);
			var horizontalCount = GetHorizontalMatches(pattern);

			// var isVertical = verticalCount > 0 && horizontalCount == 0;
			// var isHorizontal = horizontalCount > 0 && verticalCount == 0;
			// Console.WriteLine($"Vertical: {isVertical} {verticalCount}, Horizontal: {isHorizontal} {horizontalCount}");
			//
			// var count =  Math.Max(verticalCount, horizontalCount);

			sum += verticalCount + (100 * horizontalCount);
		}

		return sum.ToString();
	}


	public override string SolvePart2()
	{
		return "";
	}
}
