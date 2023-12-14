using AdventOfCode.Common;

namespace AdventOfCode._2023.Day13;

public static class EnumerableExtensions
{
	public static IEnumerable<IEnumerable<T>> SplitWhen<T>(this IEnumerable<T> source, Func<T, bool> predicate)
	{
		var sublist = new List<T>();
		foreach (var item in source)
		{
			if (predicate(item))
			{
				if (sublist.Any())
				{
					yield return sublist;
					sublist = new List<T>();
				}
			}
			else
			{
				sublist.Add(item);
			}
		}

		if (sublist.Any())
		{
			yield return sublist;
		}
	}
}

class Day13SolverV2 : DaySolver
{
	public record Pattern(char[][] FieldData)
	{
		public int Width => FieldData[0].Length;
		public int Height => FieldData.Length;

		public List<ulong> Horizontal =>
			Enumerable.Range(0, Width).Select(x =>
					Convert.ToUInt64(new string(Enumerable.Range(0, Height).Select(y => FieldData[y][x]).ToArray()), 2))
				.ToList();

		public List<ulong> Vertical =>
			Enumerable.Range(0, Height).Select(y =>
					Convert.ToUInt64(
						new string(Enumerable.Range(0, Width).Select(x => FieldData[y][x]).Reverse().ToArray()), 2))
				.ToList();

		public List<int> HorizontalPalindromes => FindPalindrome(Horizontal);
		public List<int> VerticalPalindromes => FindPalindrome(Vertical);

		public int FirstHorizontalPalindromeIndex => HorizontalPalindromes.Count == 0 ? -1 : HorizontalPalindromes[0];
		public int FirstVerticalPalindromeIndex => VerticalPalindromes.Count == 0 ? -1 : VerticalPalindromes[0];

		public int Score => (FirstVerticalPalindromeIndex + 1) * 100 + FirstHorizontalPalindromeIndex + 1;

		public Pattern GetAlternative(int x, int y)
		{
			var newFieldData = FieldData.Select(row => row.ToArray()).ToArray();
			newFieldData[y][x] = newFieldData[y][x] == '0' ? '1' : '0';
			return new Pattern(newFieldData);
		}

		public bool IsPalindrome(List<ulong> list, int index)
		{
			int minLength = Math.Min(index + 1, list.Count - index - 1);
			return list.Skip(index - minLength + 1).Take(minLength)
				.SequenceEqual(list.Skip(index + 1).Take(minLength).Reverse());
		}

		public List<int> FindPalindrome(List<ulong> list) =>
			Enumerable.Range(0, list.Count - 1).Where(i => list[i] == list[i + 1] && IsPalindrome(list, i)).ToList();

		public IEnumerable<Pattern> FindAlternativeReflections() =>
			Enumerable.Range(0, Width).SelectMany(x => Enumerable.Range(0, Height), (x, y) => GetAlternative(x, y))
				.Where(newField => HasDifferentPalindromes(newField));

		private bool HasDifferentPalindromes(Pattern newPattern)
		{
			return (newPattern.HorizontalPalindromes.Any() &&
			        !newPattern.HorizontalPalindromes.SequenceEqual(HorizontalPalindromes)) ||
			       (newPattern.VerticalPalindromes.Any() &&
			        !newPattern.VerticalPalindromes.SequenceEqual(VerticalPalindromes));
		}

		public int GetAlternativeScore(Pattern original)
		{
			var newHorizontalPalindromes =
				HorizontalPalindromes.Where(o => !original.HorizontalPalindromes.Contains(o)).ToList();
			var newVerticalPalindromes =
				VerticalPalindromes.Where(o => !original.VerticalPalindromes.Contains(o)).ToList();

			var numCols = newHorizontalPalindromes.Count == 0 ? 0 : newHorizontalPalindromes[0] + 1;
			var numRows = newVerticalPalindromes.Count == 0 ? 0 : newVerticalPalindromes[0] + 1;

			return numRows * 100 + numCols;
		}
	}


	public override string Day => "13";
	public override string Year => "2023";

	public Day13SolverV2(DaySolverOptions options) : base(options)
	{
	}


	public Pattern ParseFieldLines(IEnumerable<string> lines) =>
		new(lines.Select(line => line.ToCharArray()).ToArray());

	public List<Pattern> ParsePatterns(string input) =>
		input.Replace(".", "0").Replace("#", "1").Split("\n").SplitWhen(line => line.Length == 0)
			.Select(ParseFieldLines).ToList();

	public override string SolvePart1()
	{
		return ParsePatterns(Input)
			.Sum(field => field.Score)
			.ToString();
	}


	public override string SolvePart2()
	{
		return ParsePatterns(Input)
			.Sum(field => field.FindAlternativeReflections().First().GetAlternativeScore(field))
			.ToString();
	}
}
