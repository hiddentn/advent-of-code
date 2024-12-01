using AdventOfCode.Common;
using AdventOfCode.Common.Extensions;

namespace AdventOfCode._2015.Day1;

public class Day1Solver(DaySolverOptions options) : DaySolver(options)
{
	public override string Day => "1";
	public override string Year => "2015";


	public override string SolvePart1()
	{
		return InputLines
			.First()
			.Select(step => step switch
			{
				'(' => 1,
				')' => -1,
				_ => throw new InvalidOperationException($"Invalid step: {step}")
			})
			.Sum()
			.ToString();
	}

	public override string SolvePart2()
	{
		return InputLines
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

					if (floor < 0) return index + 1;
				}

				throw new UnreachableException("Santa never went to the basement");
			})
			.ToString();
	}
}
