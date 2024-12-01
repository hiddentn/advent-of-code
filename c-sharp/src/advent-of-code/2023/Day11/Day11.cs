using AdventOfCode.Common;

namespace AdventOfCode._2023.Day11;

public class Day11Solver(DaySolverOptions options) : DaySolver(options)
{
	public override string Day => "11";
	public override string Year => "2023";

	private static char[][] ParseInput(string input)
	{
		return input.Split("\n").SkipLast(1).Select(x => x.ToCharArray()).ToArray();
	}

	private static char[][] ExpandMap(char[][] map)
	{
		// copy the map to a new List<List<char>>
		var expandedMap = new List<List<char>>();


		var expandedRowIndex = 0;
		for (int i = 0; i < map.Length; i++)
		{
			var row = map[i];

			expandedMap.Insert(expandedRowIndex, row.ToList());
			expandedRowIndex += 1;
			if (!row.Contains('#'))
			{
				expandedMap.Insert(expandedRowIndex, row.ToList());
				expandedRowIndex += 1;
			}
		}

		// expand columns
		var expandedColumnIndex = -1;
		for (int i = 0; i < map[0].Length; i++)
		{
			expandedColumnIndex += 1;
			var column = expandedMap.Select(x => x[expandedColumnIndex]).ToArray();
			if (!column.Contains('#'))
			{
				expandedColumnIndex += 1;
				foreach (var row in expandedMap)
				{
					row.Insert(expandedColumnIndex, '.');
				}
			}
		}

		return expandedMap.Select(x => x.ToArray()).ToArray();
	}

	private static (List<int> verticals, List<int> horizontals) GetVerticalsAndHorizontals(char[][] map)
	{
		var verticals = new List<int>();
		var horizontals = new List<int>();

		for (int x = 0; x < map.Length; x++)
		{
			var row = map[x];
			if (!row.Contains('#'))
			{
				verticals.Add(x);
			}
		}

		for (int y = 0; y < map[0].Length; y++)
		{
			var column = map.Select(x => x[y]).ToArray();
			if (!column.Contains('#'))
			{
				horizontals.Add(y);
			}
		}

		return (verticals, horizontals);
	}

	private static List<(int X, int Y)[]> GetPairs((int X, int Y)[] points)
	{
		// get the shortest distance between each 2 galaxies
		// each pair must be unique
		var pairs = new List<(int X, int Y)[]>();

		// add each pair only one the order doesn't matter
		for (var i = 0; i < points.Length; i++)
		{
			for (var j = i + 1; j < points.Length; j++)
			{
				if (i != j)
				{
					pairs.Add(new[] { points[i], points[j] });
				}
			}
		}

		// remove duplicates the order doesn't matter
		return pairs.Select(x => x.OrderBy(y => y.X).ThenBy(y => y.Y).ToArray()).Distinct().ToList();
	}

	private static (int X, int Y)[] GetGalaxiesPositions(char[][] map)
	{
		var galaxies = new List<(int X, int Y)>();
		for (int x = 0; x < map.Length; x++)
		{
			var row = map[x];
			for (int y = 0; y < row.Length; y++)
			{
				if (row[y] == '#')
				{
					galaxies.Add((x, y));
				}
			}
		}

		return galaxies.ToArray();
	}

	public override string SolvePart1()
	{
		var map = ParseInput(Input);
		var expandedMap = ExpandMap(map);
		var galaxies = GetGalaxiesPositions(expandedMap);
		var pairs = GetPairs(galaxies);
		var distances = pairs.Select(p => Math.Abs(p[1].X - p[0].X) + Math.Abs(p[1].Y - p[0].Y)).ToArray();
		return distances.Sum().ToString();
	}


	public override string SolvePart2()
	{
		var map = ParseInput(Input);
		var (verticals, horizontals) = GetVerticalsAndHorizontals(map);
		var galaxies = GetGalaxiesPositions(map);
		var pairs = GetPairs(galaxies);

		var expansion = 2 - 1;
		var distances = new List<long>();
		foreach (var p in pairs)
		{
			var distance = Math.Abs(p[1].X - p[0].X) + Math.Abs(p[1].Y - p[0].Y);
			// how many verticals intersect the 2 points
			var verticalsCount = verticals.Count(x =>
			{
				var min = Math.Min(p[0].X, p[1].X);
				var max = Math.Max(p[0].X, p[1].X);
				return x > min && x < max;
			});
			// how many horizontals intersect the 2 points
			var horizontalsCount = horizontals.Count(x =>
			{
				var min = Math.Min(p[0].Y, p[1].Y);
				var max = Math.Max(p[0].Y, p[1].Y);
				return x > min && x < max;
			});
			distance += (verticalsCount + horizontalsCount) * expansion;
			distances.Add(distance);
		}

		return distances.Sum().ToString();
	}
}
