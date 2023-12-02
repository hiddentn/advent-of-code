using AdventOfCode.Common;

namespace AdventOfCode.Tests._2015;

using AdventOfCode._2015.Day2;

public class Day2Tests
{
	[Fact]
	public void Part1Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "2015/Day2/input.txt"; });
		var solver = new Day2Solver(options);
		var result = solver.SolvePart1();
		result.Should().Be("1606483");
	}


	[Fact]
	public void Part2Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "2015/Day2/input.txt"; });
		var solver = new Day2Solver(options);
		var result = solver.SolvePart2();
		result.Should().Be("3842356");
	}
}
