using AdventOfCode.Common;

namespace AdventOfCode._2023.Day7;

public enum HandType
{
	FiveOfAKind,
	FourOfAKind,
	FullHouse,
	ThreeOfAKind,
	TwoPair,
	OnePair,
	HighCard
}

public record Hand(string HandStr, int Bid) : IComparable<Hand>
{
	private static readonly List<char> CardsOrder = new()
		{ 'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2' };

	private List<char> Cards => HandStr.AsEnumerable().ToList();
	private HandType HandType => GetHandType();

	private HandType GetHandType()
	{
		var cardGroups = HandStr.GroupBy(c => c).ToList();


		var fiveOfAKind = HandStr.GroupBy(c => c).Any(g => g.Count() == 5);

		// Five of a kind, where all five cards have the same label: AAAAA
		if (fiveOfAKind)
			return HandType.FiveOfAKind;


		// Four of a kind, where four cards have the same label and one card has a different label: AA8AA

		var fourOfAKind = cardGroups.Any(g => g.Count() == 4);


		if (fourOfAKind)
			return HandType.FourOfAKind;

		// Full house, where three cards have the same label, and the remaining two cards share a different label: 23332

		var fullHouse = cardGroups.Any(g => g.Count() == 3) && cardGroups.Any(g => g.Count() == 2);

		if (fullHouse)
			return HandType.FullHouse;

		// Three of a kind, where three cards have the same label, and the remaining two cards are each different from any other card in the hand: TTT98

		var threeOfAKind = cardGroups.Any(g => g.Count() == 3) && cardGroups.Count(g => g.Count() == 2) == 0;

		if (threeOfAKind)
			return HandType.ThreeOfAKind;

		// Two pair, where two cards share one label, two other cards share a second label, and the remaining card has a third label: 23432

		var twoPair = cardGroups.Count(g => g.Count() == 2) == 2;

		if (twoPair)
			return HandType.TwoPair;

		// One pair, where two cards share one label, and the other three cards have a different label from the pair and each other: A23A4

		var onePair = cardGroups.Count(g => g.Count() == 2) == 1;
		if (onePair)
			return HandType.OnePair;

		// High card, where all cards' labels are distinct: 23456
		return HandType.HighCard;
	}

	public int CompareTo(Hand? other)
	{
		if (HandType < other.HandType)
		{
			return 1;
		}

		if (HandType > other.HandType)
		{
			return -1;
		}

		for (var i = 0; i < Cards.Count; i++)
		{
			if (CardsOrder.IndexOf(Cards[i]) < CardsOrder.IndexOf(other.Cards[i]))
			{
				return 1;
			}

			if (CardsOrder.IndexOf(Cards[i]) > CardsOrder.IndexOf(other.Cards[i]))
			{
				return -1;
			}
		}

		return 0;
	}
};

public record Hand2(string HandStr, int Bid) : IComparable<Hand2>
{
	private static readonly List<char> CardsOrder = new()
		{ 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J' };

	private List<char> Cards => HandStr.AsEnumerable().ToList();
	private HandType HandType => GetHandType();

	private HandType GetHandType()
	{
		var cardGroups = Cards.GroupBy(x => x).ToList();

		if (Cards.All(x => x.Equals(Cards[0])) ||
		    Cards.Contains('J') && Cards.Distinct().Count() == 2)
		{
			return HandType.FiveOfAKind;
		}

		if (cardGroups.Any(group => group.Count() == 4) ||
		    cardGroups.Any(group =>
			    group.Count() == 3 && group.Key != 'J' && Cards.Contains('J')) || // 2333J
		    cardGroups.Any(group =>
			    group.Count() == 2 && group.Key != 'J' && Cards.Count(x => x == 'J') == 2) || // 233JJ
		    cardGroups.Any(group =>
			    group.Count() == 1 && group.Key != 'J' && Cards.Count(x => x == 'J') == 3)) // 23JJJ
		{
			return HandType.FourOfAKind;
		}

		if (cardGroups.Any(group => group.Count() == 3) && cardGroups.Any(group => group.Count() == 2) ||
		    (cardGroups.Count(group => group.Count() == 2) == 2) &&
		    Cards.Contains('J')) // two pairs and joker
		{
			return HandType.FullHouse;
		}

		if (cardGroups.Any(group => group.Count() == 3) ||
		    cardGroups.Any(group => group.Count() == 2) && Cards.Contains('J')) // one pair and joker
		{
			return HandType.ThreeOfAKind;
		}

		if (cardGroups.Count(group => group.Count() == 2) ==
		    2) // one joker here means three kind is always better than two pair
		{
			return HandType.TwoPair;
		}

		if (cardGroups.Any(group => group.Count() == 2) || Cards.Contains('J')) // one pair or one joker
		{
			return HandType.OnePair;
		}

		return HandType.HighCard;
	}

	public int CompareTo(Hand2? other)
	{
		if (HandType < other.HandType)
		{
			return 1;
		}

		if (HandType > other.HandType)
		{
			return -1;
		}

		for (var i = 0; i < Cards.Count; i++)
		{
			if (CardsOrder.IndexOf(Cards[i]) < CardsOrder.IndexOf(other.Cards[i]))
			{
				return 1;
			}

			if (CardsOrder.IndexOf(Cards[i]) > CardsOrder.IndexOf(other.Cards[i]))
			{
				return -1;
			}
		}

		return 0;
	}
};

public class Day7Solver : DaySolver
{
	public Day7Solver(DaySolverOptions options) : base(options)
	{
	}

	public override string Day => "7";
	public override string Year => "2023";

	public override string SolvePart1()
	{
		var hands = InputLines.Select(s =>
		{
			var parts = s.Split(" ");
			var hand = new Hand(parts[0], int.Parse(parts[1]));
			return hand;
		});

		var sorted = hands.Order().ToList();

		var results = new List<int>();
		for (var i = 0; i < sorted.Count; i++)
		{
			results.Add(sorted[i].Bid * (i + 1));
		}


		return results.Sum().ToString();
	}


	public override string SolvePart2()
	{
		var hands = InputLines.Select(s =>
		{
			var parts = s.Split(" ");
			var hand = new Hand2(parts[0], int.Parse(parts[1]));
			return hand;
		});

		var sorted = hands.Order().ToList();

		var results = new List<int>();
		for (var i = 0; i < sorted.Count; i++)
		{
			results.Add(sorted[i].Bid * (i + 1));
		}


		return results.Sum().ToString();
	}
}
