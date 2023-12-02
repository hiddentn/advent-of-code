using AdventOfCode.Common;

namespace AdventOfCode.Tests._2022;

using AdventOfCode._2022.Day5;

public class Day5Tests
{
	[Fact]
	public void Part1Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "2022/Day5/input.txt"; });
		var solver = new Day5Solver(options);
		var result = solver.SolvePart1();
		result.Should().Be("WHTLRMZRC");
	}


	[Fact]
	public void Part2Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "2022/Day5/input.txt"; });
		var solver = new Day5Solver(options);
		var result = solver.SolvePart2();
		result.Should().Be("GMPMLWNMG");
	}
}
