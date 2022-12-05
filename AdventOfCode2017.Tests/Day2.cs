using AdventOfCode.Abstractions;
using AdventOfCode2017.Day2;
using FluentAssertions;

namespace AdventOfCode2017.Tests;

public class Day2Tests
{
	[Fact]
	public void Part1Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "Day2/input.txt"; });
		var solver = new Day2Solver(options);
		var result = solver.SolvePart1();
		result.Should().Be("44670");
	}


	[Fact]
	public void Part2Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "Day2/input.txt"; });
		var solver = new Day2Solver(options);
		var result = solver.SolvePart2();
		result.Should().Be("285");
	}
}
