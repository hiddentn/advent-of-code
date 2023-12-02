using AdventOfCode._2022.Day7;
using AdventOfCode.Common;

namespace AdventOfCode.Tests._2022;

public class Day7Tests
{
	[Fact]
	public void Part1Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "2022/Day7/input.txt"; });
		var solver = new Day7Solver(options);
		var result = solver.SolvePart1();
		result.Should().Be("1844187");
	}


	[Fact]
	public void Part2Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "2022/Day7/input.txt"; });
		var solver = new Day7Solver(options);
		var result = solver.SolvePart2();
		result.Should().Be("4978279");
	}
}
