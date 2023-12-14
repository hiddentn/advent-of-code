using AdventOfCode._2022.Day1;
using AdventOfCode.Common;

namespace AdventOfCode._2023.Day3;

public class Day3Solver : DaySolver
{
	public Day3Solver(DaySolverOptions options) : base(options)
	{
	}

	public override string Day => "3";
	public override string Year => "2023";


	public override string SolvePart1()
	{
		return "";
		var validNumbers = new List<int>();
		var grid = InputLines.Select(s => s.ToCharArray()).ToArray();

		for (var i = 0; i < grid.Length; i++)
		{
			var currentNumber = "";
			for (var j = 0; j < grid[i].Length; j++)
			{
				if (char.IsDigit(grid[i][j]))
				{
					currentNumber += grid[i][j];
				}
				else
				{
					if (currentNumber.Length == 0)
					{
						continue;
					}

					var numberEnd = j - 1;
					var numberStart = j - currentNumber.Length;

					// check if any item surrounding the number is a special character in the grid
					var hasSpecialChar = HasSpecialChar(grid, (i, numberStart), (i, numberEnd));

					if (hasSpecialChar)
					{
						var number = int.Parse(currentNumber);
						validNumbers.Add(number);
					}

					currentNumber = "";
				}

				if (j + 1 == grid[i].Length && currentNumber.Length > 0)
				{
					var numberEnd = j;
					var numberStart = j - currentNumber.Length;

					// check if any item surrounding the number is a special character in the grid
					var hasSpecialChar = HasSpecialChar(grid, (i, numberStart), (i, numberEnd));

					if (hasSpecialChar)
					{
						var number = int.Parse(currentNumber);
						validNumbers.Add(number);
					}
				}
			}
		}


		return validNumbers.Sum().ToString();
	}

	private static bool HasSpecialChar(IReadOnlyList<char[]> grid, (int x, int y) start, (int x, int y) end)
	{
		var xStart = start.x - 1 >= 0 ? start.x - 1 : 0;
		var yStart = start.y - 1 >= 0 ? start.y - 1 : 0;

		var xEnd = end.x + 1 < grid.Count ? end.x + 1 : end.x;
		var yEnd = end.y + 1 < grid[0].Length ? end.y + 1 : end.y;

		for (var i = xStart; i <= xEnd; i++)
		{
			for (var j = yStart; j <= yEnd; j++)
			{
				if (!char.IsDigit(grid[i][j]) && grid[i][j] != '.')
				{
					return true;
				}
			}
		}

		return false;
	}

	public override string SolvePart2()
	{
		var validNumbers = new List<int>();
		var grid = InputLines.Select(s => s.ToCharArray()).ToArray();

		for (var i = 0; i < grid.Length; i++)
		{
			for (var j = 0; j < grid[i].Length; j++)
			{
				if (grid[i][j] == '*')
				{
					var xStart = i - 1 >= 0 ? i - 1 : 0;
					var yStart = j - 1 >= 0 ? j - 1 : 0;

					var xEnd = i + 1 < grid.Length ? i + 1 : i;
					var yEnd = j + 1 < grid[0].Length ? j + 1 : j;

					var adjacentNumbers = new List<int>();
					for (var ii = xStart; i <= xEnd; i++)
					{
						for (var jj = yStart; j <= yEnd; j++)
						{
							if (char.IsDigit(grid[ii][jj]))
							{
								var number = "" + grid[ii][jj];
								while (jj+1 < grid[i].Length  && char.IsDigit(grid[ii][jj+1]))
								{
									number += grid[ii][jj+1];
									jj++;
								}
								adjacentNumbers.Add(int.Parse(number));
							}
						}
					}

					if (adjacentNumbers.Count == 2)
					{
						validNumbers.Add(adjacentNumbers[0] * adjacentNumbers[1]);
					}
				}
			}
		}

		return validNumbers.Sum().ToString();
	}
}
