using AdventOfCode.Common;

namespace AdventOfCode._2022.Day8;

public class Day8Solver : DaySolver
{
	public Day8Solver(DaySolverOptions options) : base(options)
	{
	}

	public override string Day => "8";
	public override string Year => "2022";

	public override string SolvePart1()
	{
		var lines = InputLines.ToList();
		var grid = lines.Select(l => l.Select(c => int.Parse(c.ToString())).ToList()).ToList();

		var count = 0;
		for (var y = 0; y < lines.Count; y++)
		for (var x = 0; x < lines[y].Length; x++)
		{
			var current = grid[y][x];

			// w
			var visible = true;
			for (var j = 0; visible && j < x; j++)
				if (grid[y][j] >= current)
					visible = false;

			if (visible)
			{
				count++;
				continue;
			}

			// e
			visible = true;
			for (var j = grid[y].Count - 1; visible && x < j; j--)
				if (grid[y][j] >= current)
					visible = false;

			if (visible)
			{
				count++;
				continue;
			}

			// n
			visible = true;
			for (var j = 0; visible && j < y; j++)
				if (grid[j][x] >= current)
					visible = false;

			if (visible)
			{
				count++;
				continue;
			}

			// s
			visible = true;
			for (var j = grid.Count - 1; visible && y < j; j--)
				if (grid[j][x] >= current)
					visible = false;

			if (visible)
			{
				count++;
			}
		}

		return count.ToString();
	}

	public override string SolvePart2()
	{
		var lines = InputLines.ToList();
		var grid = lines.Select(l => l.Select(c => int.Parse(c.ToString())).ToList()).ToList();
		var part2 = 0L;
		for (var y = 0; y < grid.Count; y++)
		for (var x = 0; x < grid[y].Count; x++)
		{
			var height = grid[y][x];
			var product = 1L;

			// w
			var cnt = 0L;
			for (var j = x - 1; j >= 0; j--)
			{
				cnt++;
				if (grid[y][j] >= height)
					break;
			}

			product *= cnt;

			// e
			cnt = 0;
			for (var j = x + 1; j < grid[y].Count; j++)
			{
				cnt++;
				if (grid[y][j] >= height)
					break;
			}

			product *= cnt;

			// n
			cnt = 0;
			for (var j = y - 1; j >= 0; j--)
			{
				cnt++;
				if (grid[j][x] >= height)
					break;
			}

			product *= cnt;

			// s
			cnt = 0;
			for (var j = y + 1; j < grid.Count; j++)
			{
				cnt++;
				if (grid[j][x] >= height)
					break;
			}

			product *= cnt;
			if (product > part2)
				part2 = product;
		}

		return part2.ToString();
	}
}
