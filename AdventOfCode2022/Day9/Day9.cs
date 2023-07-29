using System.Diagnostics;
using System.Runtime.CompilerServices;
using AdventOfCode.Abstractions;
using AdventOfCode2022.Day1;

namespace AdventOfCode2022.Day9;

public class Day9Solver : DaySolver
{
	public Day9Solver(DaySolverOptions options) : base(options)
	{
	}

	public override string SolvePart1()
	{
		var commands = InputLines.Select(line =>
		{
			var parts = line.Split(" ");
			var direction = parts[0];
			var count = int.Parse(parts[1]);
			return (direction, count);
		});


		var hy = 0;
		var hx = 0;
		var ty = 0;
		var tx = 0;
		var visited = new HashSet<(int x, int y)> { (tx, ty) };
		foreach (var (direction, count) in commands)
		{
			for (int i = 0; i < count; i++)
			{
				(hx, hy) = Move(hx, hy, direction);
				(tx, ty) = UpdateTail(hx, hy, tx, ty);
				visited.Add((tx, ty));
			}
		}

		return visited.Count.ToString();
	}


	private (int x, int y) UpdateTail(int hx, int hy, int tx, int ty)
	{
		/*
		if (Math.Abs(hy - ty) > 1)
		{
			if (hy > ty)
			{
				ty++;
			}
			else
			{
				ty--;
			}

			if (Math.Abs(hx - tx) == 1)
			{
				if (hx > tx)
				{
					tx++;
				}
				else
				{
					tx--;
				}
			}


		}

		if (Math.Abs(hx - tx) > 1)
		{
			if (hx > tx)
			{
				tx++;
			}
			else
			{
				tx--;
			}

			if (Math.Abs(hy - ty) == 1)
			{
				if (hy > ty)
				{
					ty++;
				}
				else
				{
					ty--;
				}
			}
		}
		   */
		if (Math.Abs(hx - tx) > 1 || Math.Abs(hy - ty) > 1)
		{
			tx += Math.Min(Math.Max(-1, hx - tx), 1);
			ty += Math.Min(Math.Max(-1, hy - ty), 1);
		}

		return (tx, ty);
	}

	private static (int hx, int hy) Move(int hx, int hy, string direction) =>
		direction switch
		{
			"U" => (hx, hy - 1),
			"D" => (hx, hy + 1),
			"L" => (hx - 1, hy),
			"R" => (hx + 1, hy),
			_ => throw new UnreachableException()
		};

	public override string SolvePart2()
	{
		var commands = InputLines.Select(line =>
		{
			var parts = line.Split(" ");
			var direction = parts[0];
			var count = int.Parse(parts[1]);
			return (direction, count);
		});

		var rope = Enumerable.Range(0, 10).Select(_ => (0, 0)).ToList();
		var visited = new HashSet<(int x, int y)> { (0, 0) };


		foreach (var (direction, count) in commands)
		{
			for (var _ = 0; _ < count; _++)
			{
				var (x, y) = rope.First();
				rope[0] = Move(x, y, direction);
				// update all the children
				for (var i = 1; i < rope.Count; i++)
				{
					var (hx, hy) = rope[i - 1];
					var (tx, ty) = rope[i];
					(tx, ty) = UpdateTail(hx, hy, tx, ty);
					rope[i] = (tx, ty);
				}

				visited.Add(rope.Last());
			}
		}


		return visited.Count.ToString();
	}
}
