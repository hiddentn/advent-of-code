using AdventOfCode._2017.Day2;

namespace AdventOfCode.Tests._2017;

public class Day2Tests
{
	[Fact]
	public void Part1Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "2017/Day2/input.txt"; });
		var solver = new Day2Solver(options);
		var result = solver.SolvePart1();
		result.Should().Be("44670");
	}


	[Fact]
	public void Part2Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "2017/Day2/input.txt"; });
		var solver = new Day2Solver(options);
		var result = solver.SolvePart2();
		result.Should().Be("285");
	}
}
