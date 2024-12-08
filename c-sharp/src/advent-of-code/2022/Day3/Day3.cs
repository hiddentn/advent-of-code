﻿using AdventOfCode.Common;

namespace AdventOfCode._2022.Day3;

public class Day3Solver(DaySolverOptions options) : DaySolver(options)
{
	public override string Day => "3";
	public override string Year => "2022";

	public override string SolvePart1()
	{
		return InputLines
			.SelectMany(line =>
				line
					.SplitBy(line.Length / 2)
					.Intersect()
			)
			.Sum(x => char.IsUpper(x) ? x - 'A' + 27 : x - 'a' + 1)
			.ToString();
	}

	public override string SolvePart2()
	{
		return InputLines
			.Chunk(3)
			.SelectMany(group =>
				group
					.Intersect()
			)
			.Sum(x => char.IsUpper(x) ? x - 'A' + 27 : x - 'a' + 1)
			.ToString();
	}
}
