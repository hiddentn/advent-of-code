namespace AdventOfCode.Common;

public interface IDaySolver
{
	string Day { get; }
	string Year { get; }
	string SolvePart1();
	string SolvePart2();
}
