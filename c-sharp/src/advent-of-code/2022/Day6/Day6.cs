﻿using AdventOfCode.Common;

namespace AdventOfCode._2022.Day6;

public class Day6Solver(DaySolverOptions options) : DaySolver(options)
{
	public override string Day => "6";
	public override string Year => "2022";


	public override string SolvePart1()
	{
		var characters = InputLines.First();

		var sequence = Enumerable.Range(0, characters.Length - 4)
			.Select(i => characters.Substring(i, 4))
			.First(s => s.Distinct().Count() == 4);

		var pos = characters.IndexOf(sequence) + 4;
		return pos.ToString();
	}

	public override string SolvePart2()
	{
		var characters = InputLines.First();

		var sequence = Enumerable.Range(0, characters.Length - 14)
			.Select(i => characters.Substring(i, 14))
			.First(s => s.Distinct().Count() == 14);

		var pos = characters.IndexOf(sequence) + 14;
		return pos.ToString();
	}
}
