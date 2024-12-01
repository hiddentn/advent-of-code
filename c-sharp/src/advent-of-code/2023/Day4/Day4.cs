using AdventOfCode.Common;

namespace AdventOfCode._2023.Day4;

public record Card(int Number, int[] WiningNumbers, int[] Numbers)
{
	public int Score => (int)Math.Pow(2, WinningCardCount - 1);

	public int WinningCardCount => WiningNumbers.Intersect(Numbers).Count();
};

public class Day4Solver(DaySolverOptions options) : DaySolver(options)
{
	public override string Day => "4";
	public override string Year => "2023";


	private static Card GetCard(string input)
	{
		// formaat : "Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53"

		var lines = input.Split(':');

		var nstr = lines[0].Replace("   ", " ").Replace("  ", " ").Split(" ");
		var number = int.Parse(nstr[1]);

		var allNumbers = lines[1].Split("|").Select(s => s.Trim()).ToArray();

		var winningNumbers = allNumbers[0].Replace("  ", " ").Split(" ").Select(int.Parse).ToArray();
		var numbers = allNumbers[1].Replace("  ", " ").Split(" ").Select(int.Parse).ToArray();


		return new Card(
			number,
			winningNumbers,
			numbers
		);
	}

	public override string SolvePart1()
	{
		var cards = InputLines.Select(GetCard).Select(c => c.Score);
		return cards.Sum().ToString();
	}


	public override string SolvePart2()
	{
		var count = 0;
		var cards = InputLines.Select(GetCard).ToList();
		var cardsDict = cards.ToDictionary(c => c.Number, c => c);
		var queue = new Queue<Card>(cards);

		while (queue.Count != 0)
		{
			count++;
			var card = queue.Dequeue();
			for (var i = 1; i <= card.WinningCardCount; i++)
			{
				queue.Enqueue(cardsDict[card.Number + i]);
			}
		}


		return count.ToString();
	}
}
