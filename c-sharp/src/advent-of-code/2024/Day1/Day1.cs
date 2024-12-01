using AdventOfCode.Common;

namespace AdventOfCode._2024.Day1;

public class Day1Solver(DaySolverOptions options) : DaySolver(options)
{
	public override string Day => "1";
	public override string Year => "2024";

	private (IList<int> ListOne, List<int> ListTow) Parse()
	{
		var listOne = new List<int>();
		var listTwo = new List<int>();
		foreach (var line in InputLines)
		{
			// the parts are separated by any number of spaces
			var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
			listOne.Add(int.Parse(parts[0]));
			listTwo.Add(int.Parse(parts[1]));
		}

		return (listOne, listTwo);
	}

	public override string SolvePart1()
	{
		var (listOne, listTwo) = Parse();

		var sortedListOne = listOne.OrderBy(x => x);
		var sortedListTwo = listTwo.OrderBy(x => x);

		return sortedListOne.Zip(sortedListTwo, (x, y) => Math.Abs(x - y)).Sum().ToString();
	}

	public override string SolvePart2()
	{
		var (listOne, listTwo) = Parse();
		var sum = 0;

		foreach (var i in listOne)
		{
			var countInlistTwo = listTwo.Count(x => x == i);
			sum += i * countInlistTwo;
		}

		return sum.ToString();
	}
}
