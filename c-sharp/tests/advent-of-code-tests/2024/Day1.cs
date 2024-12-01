using AdventOfCode._2024.Day1;

namespace AdventOfCode.Tests._2024;

public class Day1Tests
{
	[Fact]
	public void Part1Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "2024/Day1/input.txt"; });
		var solver = new Day1Solver(options);
		var result = solver.SolvePart1();
		result.Should().Be("2742123");
	}


	[Fact]
	public void Part2Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "2024/Day1/input.txt"; });
		var solver = new Day1Solver(options);
		var result = solver.SolvePart2();
		result.Should().Be("21328497");
	}
}
