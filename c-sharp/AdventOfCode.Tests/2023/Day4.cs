using AdventOfCode._2023.Day4;

namespace AdventOfCode.Tests._2023;

public class Day4Tests
{
	[Fact]
	public void Part1Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "2023/Day4/input.txt"; });
		var solver = new Day4Solver(options);
		var result = solver.SolvePart1();
		result.Should().Be("23028");
	}


	[Fact]
	public void Part2Solution()
	{
		var options = DaySolverOptions.Configure(opt => { opt.InputFilepath = "2023/Day4/input.txt"; });
		var solver = new Day4Solver(options);
		var result = solver.SolvePart2();
		result.Should().Be("9236992");
	}
}
