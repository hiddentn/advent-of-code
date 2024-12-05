using AdventOfCode._2024.Day5;

namespace AdventOfCode.Tests._2024;

public class Day4Tests
{
	[Fact]
	public void Part1Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "2024/Day5/input.txt"; });
		var solver = new Day5Solver(options);
		var result = solver.SolvePart1();
		result.Should().Be("6498");
	}


	[Fact]
	public void Part2Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "2024/Day5/input.txt"; });
		var solver = new Day5Solver(options);
		var result = solver.SolvePart2();
		result.Should().Be("5017");
	}
}
