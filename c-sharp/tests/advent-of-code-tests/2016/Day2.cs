using AdventOfCode._2016.Day2;

namespace AdventOfCode.Tests._2016;

public class Day2Tests
{
	[Fact]
	public void Part1Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "2016/Day2/input.txt"; });
		var solver = new Day2Solver(options);
		var result = solver.SolvePart1();
		result.Should().Be("61529");
	}


	[Fact]
	public void Part2Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "2016/Day2/input.txt"; });
		var solver = new Day2Solver(options);
		var result = solver.SolvePart2();
		result.Should().Be("C2C28");
	}
}
