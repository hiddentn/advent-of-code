﻿using AdventOfCode.Abstractions;

namespace AdventOfCode2019.Day1;

public class Day1Solver : DaySolver
{
	public Day1Solver(DaySolverOptions options) : base(options)
	{
	}

	public override string SolvePart1() =>
		InputLines
			.Select(i => int.Parse(i))
			.Aggregate(0, (acc, current) => acc + (current / 3) - 2)
			.ToString();

	public override string SolvePart2() =>
		InputLines
			.Select(i => int.Parse(i))
			.Aggregate(0, (acc, current) =>
			{
				var moduleFuel = (current / 3) - 2;
				var currentFuel = int.Parse(moduleFuel.ToString());
				var totalFuel = currentFuel;
				while (currentFuel > 0)
				{
					currentFuel = (currentFuel / 3) - 2;
					if (currentFuel > 0)
					{
						totalFuel += currentFuel;
					}
				}

				return acc + totalFuel;
			})
			.ToString();
}