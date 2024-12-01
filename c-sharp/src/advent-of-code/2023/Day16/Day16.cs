using AdventOfCode.Common;

namespace AdventOfCode._2023.Day16;

public class Day16Solver(DaySolverOptions options) : DaySolver(options)
{
	private enum Moving
	{
		Up,
		Down,
		Left,
		Right
	}

	private record Beam(int X, int Y, Moving Moving);

	public override string Day => "16";
	public override string Year => "2023";

	private static char[][] ParseInput(string input)
	{
		return input.Split("\n").SkipLast(1).Select(line => line.ToCharArray()).ToArray();
	}

	private static List<Beam> GetNextBeams(char[][] grid, Beam beam)
	{
		var nextBeams = new List<Beam>();
		var (x, y, direction) = beam;
		var value = grid[x][y];
		switch (value)
		{
			case '.':
			{
				switch (direction)
				{
					case Moving.Up:
						if (x - 1 >= 0) nextBeams.Add(new Beam(x - 1, y, Moving.Up));
						break;
					case Moving.Down:
						if (x + 1 < grid.Length) nextBeams.Add(new Beam(x + 1, y, Moving.Down));
						break;
					case Moving.Left:
						if (y - 1 >= 0) nextBeams.Add(new Beam(x, y - 1, Moving.Left));
						break;
					case Moving.Right:
						if (y + 1 < grid[x].Length) nextBeams.Add(new Beam(x, y + 1, Moving.Right));
						break;
				}

				break;
			}
			case '/':
			{
				switch (direction)
				{
					case Moving.Down:
						if (y - 1 >= 0) nextBeams.Add(new Beam(x, y - 1, Moving.Left));
						break;
					case Moving.Up:
						if (y + 1 < grid[x].Length) nextBeams.Add(new Beam(x, y + 1, Moving.Right));
						break;
					case Moving.Right:
						if (x - 1 >= 0) nextBeams.Add(new Beam(x - 1, y, Moving.Up));
						break;
					case Moving.Left:
						if (x + 1 < grid.Length) nextBeams.Add(new Beam(x + 1, y, Moving.Down));
						break;
				}

				break;
			}
			case '\\':
			{
				switch (direction)
				{
					case Moving.Down:
						if (y + 1 < grid[x].Length) nextBeams.Add(new Beam(x, y + 1, Moving.Right));
						break;
					case Moving.Up:
						if (y - 1 >= 0) nextBeams.Add(new Beam(x, y - 1, Moving.Left));
						break;
					case Moving.Left:
						if (x - 1 >= 0) nextBeams.Add(new Beam(x - 1, y, Moving.Up));
						break;
					case Moving.Right:
						if (x + 1 < grid.Length) nextBeams.Add(new Beam(x + 1, y, Moving.Down));
						break;
				}

				break;
			}
			case '|':
			{
				switch (direction)
				{
					case Moving.Up:
						if (x - 1 >= 0) nextBeams.Add(new Beam(x - 1, y, Moving.Up));
						break;
					case Moving.Down:
						if (x + 1 < grid.Length) nextBeams.Add(new Beam(x + 1, y, Moving.Down));
						break;
					case Moving.Left or Moving.Right:
						if (x - 1 >= 0) nextBeams.Add(new Beam(x - 1, y, Moving.Up));
						if (x + 1 < grid.Length) nextBeams.Add(new Beam(x + 1, y, Moving.Down));
						break;
				}

				break;
			}
			case '-':
			{
				switch (direction)
				{
					case Moving.Left:
						if (y - 1 >= 0) nextBeams.Add(new Beam(x, y - 1, Moving.Left));
						break;
					case Moving.Right:
						if (y + 1 < grid[x].Length) nextBeams.Add(new Beam(x, y + 1, Moving.Right));
						break;
					case Moving.Up or Moving.Down:
						if (y - 1 >= 0) nextBeams.Add(new Beam(x, y - 1, Moving.Left));
						if (y + 1 < grid[x].Length) nextBeams.Add(new Beam(x, y + 1, Moving.Right));
						break;
				}

				break;
			}
		}

		return nextBeams;
	}

	private static int GetEnergy(char[][] grid, Beam initialBeam)
	{
		var queue = new Queue<Beam>();
		queue.Enqueue(initialBeam);
		var visited = new HashSet<(int, int)>();
		var visitedWithDirection = new HashSet<Beam>();
		while (queue.Count > 0)
		{
			var beam = queue.Dequeue();
			visited.Add((beam.X, beam.Y));
			visitedWithDirection.Add(beam);
			var beams = GetNextBeams(grid, beam);
			foreach (var nextBeam in beams)
			{
				if (!visitedWithDirection.Contains(nextBeam))
				{
					queue.Enqueue(nextBeam);
				}
			}
		}

		return visited.Count;
	}

	public override string SolvePart1()
	{
		var grid = ParseInput(Input);
		var initialBeam = new Beam(0, 0, Moving.Right);
		var count = GetEnergy(grid, initialBeam);
		return count.ToString();
	}


	public override string SolvePart2()
	{
		var grid = ParseInput(Input);
		var initialBeams = new List<Beam>();

		for (int y = 0; y < grid[0].Length; y++)
		{
			initialBeams.Add(new Beam(0, y, Moving.Down));
			initialBeams.Add(new Beam(grid.Length - 1, y, Moving.Up));
		}

		for (int x = 0; x < grid.Length; x++)
		{
			initialBeams.Add(new Beam(x, 0, Moving.Right));
			initialBeams.Add(new Beam(x, grid[x].Length - 1, Moving.Left));
		}

		var count = 0;
		foreach (var initialBeam in initialBeams)
		{
			var beamCount = GetEnergy(grid, initialBeam);
			if (beamCount > count)
			{
				count = beamCount;
			}
		}

		return count.ToString();
	}
}
