﻿using AdventOfCode.Abstractions;

namespace AdventOfCode2022.Day1;

public class Day1Solver : DaySolver
{
	public Day1Solver(DaySolverOptions options) : base(options)
	{
	}

	public override string SolvePart1()
	{
		return InputLines
			.Split(x => x == "")
			.Select(s => s.Select(int.Parse).Sum())
			.Max()
			.ToString();
	}

	public override string SolvePart2()
	{
		return InputLines
			.Split(x => x == "")
			.Select(s => s.Select(int.Parse).Sum())
			.OrderDescending()
			.Take(3)
			.Sum()
			.ToString();
	}
}