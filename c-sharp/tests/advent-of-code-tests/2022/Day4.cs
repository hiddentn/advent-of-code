using AdventOfCode._2022.Day4;

namespace AdventOfCode.Tests._2022;

public class Day4Tests
{
	[Fact]
	public void Part1Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "2022/Day4/input.txt"; });
		var solver = new Day4Solver(options);
		var result = solver.SolvePart1();
		result.Should().Be("528");
	}


	[Fact]
	public void Part2Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "2022/Day4/input.txt"; });
		var solver = new Day4Solver(options);
		var result = solver.SolvePart2();
		result.Should().Be("881");
	}
}