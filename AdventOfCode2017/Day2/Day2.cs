﻿using System.Diagnostics;
using AdventOfCode.Abstractions;

namespace AdventOfCode2017.Day2;

public class Day2Solver : DaySolver
{
	public Day2Solver(DaySolverOptions options) : base(options)
	{
	}

	public override string SolvePart1() =>
		InputLines
			.Select(line => line.Split('\t'))
			.Select(line => line.Select(int.Parse).ToList())
			.Select(numbers => numbers.Max() - numbers.Min())
			.Sum()
			.ToString();

	public override string SolvePart2() =>
		InputLines
			.Select(line => line.Split('\t'))
			.Select(line => line.Select(int.Parse).ToList())
			.Select(numbers =>
			{
				foreach (var x in numbers)
				{
					foreach (var y in numbers.Where(y => x != y && x % y == 0))
					{
						return x / y;
					}
				}

				throw new UnreachableException("help");
			})
			.Sum()
			.ToString();
}