using AdventOfCode.Abstractions;
using AdventOfCode2022.Day3;
using FluentAssertions;

namespace AdventOfCode2022.Tests;

public class Day3Tests
{
	[Fact]
	public void Part1Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "Day3/input.txt"; });
		var solver = new Day3Solver(options);
		var result = solver.SolvePart1();
		result.Should().Be("8039");
	}


	[Fact]
	public void Part2Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "Day3/input.txt"; });
		var solver = new Day3Solver(options);
		var result = solver.SolvePart2();
		result.Should().Be("2510");
	}
}
