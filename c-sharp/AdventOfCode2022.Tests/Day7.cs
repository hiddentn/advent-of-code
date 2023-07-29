using AdventOfCode.Abstractions;
using AdventOfCode2022.Day7;
using FluentAssertions;

namespace AdventOfCode2022.Tests;

public class Day7Tests
{
	[Fact]
	public void Part1Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "Day7/input.txt"; });
		var solver = new Day7Solver(options);
		var result = solver.SolvePart1();
		result.Should().Be("1844187");
	}


	[Fact]
	public void Part2Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "Day7/input.txt"; });
		var solver = new Day7Solver(options);
		var result = solver.SolvePart2();
		result.Should().Be("4978279");
	}
}
