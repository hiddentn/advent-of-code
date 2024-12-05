using AdventOfCode.Common;
using CommunityToolkit.HighPerformance;

namespace AdventOfCode._2024.Day4;

public class Day4Solver(DaySolverOptions options) : DaySolver(options)
{
	public override string Day => "4";
	public override string Year => "2024";


	private char[][] ParseInput() => InputLines.Select(x => x.ToCharArray()).ToArray();


	public int GetWorkCount(char[][] grid, string word)
	{
		var count = 0;
		var wordLength = word.Length;
		for (int i = 0; i < grid.Length; i++)
		{
			for (int j = 0; j < grid[i].Length; j++)
			{
				var end = j + wordLength;

				if (end > grid[i].Length)
				{
					continue;
				}

				var slice = grid[i][j..end];
				if (slice.SequenceEqual(word))
				{
					count++;
				}
			}
		}

		return count;
	}


	public override string SolvePart1()
	{
		var matrix = ParseInput();
		var rows = matrix.Length;
		var cols = matrix[0].Length;

		var word = new [] { 'M', 'A', 'S' };

		bool IsInvalidRow(int indexR) => indexR >= rows || indexR < 0;
		bool IsInvalidCol(int indexC) => indexC >= cols || indexC < 0;

		int FindInDirection(int r, int c, int dirR, int dirC)
		{
			for(int i = 0; i < word.Length; i++)
			{
				var indexR = r + (i * dirR) + dirR;
				var indexC = c + (i * dirC) + dirC;

				if (IsInvalidRow(indexR) ||
				    IsInvalidCol(indexC) ||
				    matrix[indexR][indexC] != word[i])
				{
					return 0;
				}
			}

			return 1;
		}

		int Find(int r, int c)
		{
			var dirs = new [] { -1, 0, 1 };

			var result = 0;
			for (int dirR = 0; dirR < dirs.Length; dirR++)
			{
				for (int dirC = 0; dirC < dirs.Length; dirC++)
				{
					result += FindInDirection(r, c, dirs[dirR], dirs[dirC]);
				}
			}

			return result;
		}

		bool FindXChar(int r, int c, int dirR, int dirC, char toFind)
		{
			var indexR = r + dirR;
			var indexC = c + dirC;
			return !(IsInvalidRow(indexR) ||
			         IsInvalidCol(indexC) ||
			         matrix[indexR][indexC] != toFind);
		}

		bool FindXHalf(int r, int c, int dirR, int dirC)
		{
			return
				FindXChar(r, c, dirR, dirC, 'M') &&
				FindXChar(r, c, dirR * -1, dirC * -1, 'S');
		}

		int FindX(int r, int c)
		{
			var dirs = new [] { 1, -1 };

			var result = 0;
			for (int dirR = 0; dirR < dirs.Length; dirR++)
			{
				for (int dirC = 0; dirC < dirs.Length; dirC++)
				{
					result += FindXHalf(r, c, dirs[dirR], dirs[dirC]) ? 1 : 0;
				}
			}

			return result == 2 ? 1 : 0;
		}

		var part1Count = 0;
		var part2Count = 0;
		for (int r = 0; r < rows; r++)
		{
			for (int c = 0; c < cols; c++)
			{
				if (matrix[r][c] == 'X')
				{
					part1Count += Find(r, c);
				}
				else if (matrix[r][c] == 'A')
				{
					part2Count += FindX(r, c);
				}
			}
		}

		return part1Count.ToString();
	}


	public override string SolvePart2()
	{
		var matrix = ParseInput();
		var rows = matrix.Length;
		var cols = matrix[0].Length;

		var word = new [] { 'M', 'A', 'S' };

		bool IsInvalidRow(int indexR) => indexR >= rows || indexR < 0;
		bool IsInvalidCol(int indexC) => indexC >= cols || indexC < 0;

		int FindInDirection(int r, int c, int dirR, int dirC)
		{
			for(int i = 0; i < word.Length; i++)
			{
				var indexR = r + (i * dirR) + dirR;
				var indexC = c + (i * dirC) + dirC;

				if (IsInvalidRow(indexR) ||
				    IsInvalidCol(indexC) ||
				    matrix[indexR][indexC] != word[i])
				{
					return 0;
				}
			}

			return 1;
		}

		int Find(int r, int c)
		{
			var dirs = new [] { -1, 0, 1 };

			var result = 0;
			for (int dirR = 0; dirR < dirs.Length; dirR++)
			{
				for (int dirC = 0; dirC < dirs.Length; dirC++)
				{
					result += FindInDirection(r, c, dirs[dirR], dirs[dirC]);
				}
			}

			return result;
		}

		bool FindXChar(int r, int c, int dirR, int dirC, char toFind)
		{
			var indexR = r + dirR;
			var indexC = c + dirC;
			return !(IsInvalidRow(indexR) ||
			         IsInvalidCol(indexC) ||
			         matrix[indexR][indexC] != toFind);
		}

		bool FindXHalf(int r, int c, int dirR, int dirC)
		{
			return
				FindXChar(r, c, dirR, dirC, 'M') &&
				FindXChar(r, c, dirR * -1, dirC * -1, 'S');
		}

		int FindX(int r, int c)
		{
			var dirs = new [] { 1, -1 };

			var result = 0;
			for (int dirR = 0; dirR < dirs.Length; dirR++)
			{
				for (int dirC = 0; dirC < dirs.Length; dirC++)
				{
					result += FindXHalf(r, c, dirs[dirR], dirs[dirC]) ? 1 : 0;
				}
			}

			return result == 2 ? 1 : 0;
		}

		var part1Count = 0;
		var part2Count = 0;
		for (int r = 0; r < rows; r++)
		{
			for (int c = 0; c < cols; c++)
			{
				if (matrix[r][c] == 'X')
				{
					part1Count += Find(r, c);
				}
				else if (matrix[r][c] == 'A')
				{
					part2Count += FindX(r, c);
				}
			}
		}

		return part2Count.ToString();
	}
}
