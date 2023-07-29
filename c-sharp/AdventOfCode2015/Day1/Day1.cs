using System.Diagnostics;
using AdventOfCode.Abstractions;
using AdventOfCode.Common.EnumerableExtensions;
using AdventOfCode.Common.Extensions;

namespace AdventOfCode2015.Day1;

public class Day1Solver : DaySolver
{
	public Day1Solver(DaySolverOptions options) : base(options)
	{
	}

	public override string SolvePart1() =>
		InputLines
			.First()
			.Select(step => step switch
			{
				'(' => 1,
				')' => -1,
				_ => throw new InvalidOperationException($"Invalid step: {step}")
			})
			.Sum()
			.ToString();

	public override string SolvePart2() =>
		InputLines
			.First()
			.Bind(steps =>
			{
				var floor = 0;
				foreach (var (step, index) in steps.WithIndex())
				{
					floor += step switch
					{
						'(' => 1,
						')' => -1,
						_ => throw new InvalidOperationException($"Invalid step: {step}")
					};

					if (floor < 0)
					{
						return index + 1;
					}
				}

				throw new UnreachableException("Santa never went to the basement");
			})
			.ToString();
}
