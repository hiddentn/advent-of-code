using AdventOfCode.Abstractions;
using AdventOfCode2022.Day6;
using FluentAssertions;

namespace AdventOfCode2022.Tests;

public class Day6Tests
{
	[Fact]
	public void Part1Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "Day6/input.txt"; });
		var solver = new Day6Solver(options);
		var result = solver.SolvePart1();
		result.Should().Be("1198");
	}


	[Fact]
	public void Part2Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "Day6/input.txt"; });
		var solver = new Day6Solver(options);
		var result = solver.SolvePart2();
		result.Should().Be("3120");
	}
}
