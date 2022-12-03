using AdventOfCode.Abstractions;
using AdventOfCode2020.Day1;
using FluentAssertions;

namespace AdventOfCode2020.Tests;

public class Day1Tests
{
	[Fact]
	public void Part1Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "Day1/input.txt"; });
		var solver = new Day1Solver(options);
		var result = solver.SolvePart1();
		result.Should().Be("542619");
	}


	[Fact]
	public void Part2Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "Day1/input.txt"; });
		var solver = new Day1Solver(options);
		var result = solver.SolvePart2();
		result.Should().Be("32858450");
	}
}
