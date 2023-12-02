using AdventOfCode.Common;
using static System.Char;

namespace AdventOfCode._2023.Day1;

public class Day1Solver : DaySolver
{
	public override string Day => "1";
	public override string Year => "2023";

	public Day1Solver(DaySolverOptions options) : base(options)
	{
	}


	public override string SolvePart1()
	{
		return InputLines
			.Select(l => l.First(IsDigit) + l.Last(IsDigit).ToString())
			.Select(int.Parse)
			.Sum()
			.ToString();
	}

	public override string SolvePart2()
	{
		return InputLines.Select(l => l
				.Replace("one", "o1e")
				.Replace("two", "t2o")
				.Replace("three", "t3e")
				.Replace("four", "f4r")
				.Replace("five", "f5e")
				.Replace("six", "s6x")
				.Replace("seven", "s7n")
				.Replace("eight", "e8t")
				.Replace("nine", "n9e"))
				// .Replace("two", "2")
				// .Replace("eight", "8")
				// .Replace("one", "1")
				// .Replace("three", "3")
				// .Replace("four", "4")
				// .Replace("five", "5")
				// .Replace("six", "6")
				// .Replace("seven", "7")
				// .Replace("nine", "9"))
			.Select(l => l.First(IsDigit) + l.Last(IsDigit).ToString())
			.Select(int.Parse)
			.Sum()
			.ToString();
	}
}
