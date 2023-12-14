using System.Text.RegularExpressions;
using AdventOfCode.Common;

namespace AdventOfCode._2023.Day12;

public record Row(string Springs, int[] Groups);

public class Day12Solver : DaySolver
{
	public Day12Solver(DaySolverOptions options) : base(options)
	{
	}

	public override string Day => "X";
	public override string Year => "2023";

	private static List<Row> Parse(IEnumerable<string> input)
	{
		var rows = new List<Row>();
		foreach (var line in input)
		{
			var parts = line.Split(" ");
			var config = parts[0].Trim();
			var ranges = parts[1].Trim().Split(",").Select(int.Parse).ToList();
			rows.Add(new Row(config, ranges.ToArray()));
		}

		return rows;
	}

	private static readonly char[] Options = { '.', '#' };

	private static IEnumerable<string> Generate(string input, int startIndex)
	{
		int questionMarkIndex = input.IndexOf('?', startIndex);
		if (questionMarkIndex == -1)
		{
			// No more question marks, yield return the current string
			yield return input;
			yield break;
		}


		foreach (var option in Options)
		{
			// Replace the '?' with each option and recurse
			char[] inputArray = input.ToCharArray();
			inputArray[questionMarkIndex] = option;
			string replaced = new string(inputArray);

			foreach (var combination in Generate(replaced, questionMarkIndex + 1))
			{
				yield return combination;
			}
		}
	}

	private static int GetCombinations(Row row)
	{
		var pattern = row.Groups.Aggregate("^(\\.*", (current, group) => current + $"#{{{group}}}\\.+");
		// replace the last + with a ? to make it non-greedy
		pattern = pattern[..^1] + "*";
		pattern += ")$";

		var regex = new Regex(pattern, RegexOptions.Compiled);

		var possibilities = Generate(row.Springs, 0);
		return possibilities.AsParallel().Count(possibility => regex.IsMatch(possibility));
	}

	private static Row FoldRow(Row input, int multiplier)
	{
		var springs = input.Springs;
		var groups = input.Groups.ToList();

		for (int i = 0; i < multiplier; i++)
		{
			springs += "?" + input.Springs;
			groups.AddRange(input.Groups);
		}

		return new Row(springs, groups.ToArray());
	}

	private static readonly Dictionary<Tuple<string, int[]>, int> Cache = new();
	private static readonly Dictionary<Tuple<string, IReadOnlyList<int>, int>, int> Cache2 = new();

	private static int CountWays(string input, int[] nums)
	{
		var key = Tuple.Create(input, nums);
		if (Cache.TryGetValue(key, out var ways))
		{
			return ways;
		}

		if (nums.Length == 0)
		{
			return input.Contains('#') ? 0 : 1;
		}

		var size = nums[0];
		var total = 0;

		for (var i = 0; i < input.Length; i++)
		{
			if (i + size <= input.Length && !input.Substring(i, size).Contains('.') &&
			    (i == 0 || input[i - 1] != '#') &&
			    (i + size == input.Length || input[i + size] != '#'))
			{
				var newNums = new int[nums.Length - 1];
				Array.Copy(nums, 1, newNums, 0, nums.Length - 1);
				total += CountWays(input[(i + size)..], newNums);
			}

			if (input[i] == '#')
			{
				break;
			}
		}

		Cache[key] = total;
		return total;
	}


	private static int NumSolutions(string s, IReadOnlyList<int> sizes, int numDoneInGroup = 0)
	{
		var key = Tuple.Create(s, sizes, numDoneInGroup);
		if (Cache2.TryGetValue(key, out var cachedResult))
		{
			return cachedResult;
		}

		if (string.IsNullOrEmpty(s))
		{
			// Is this a solution? Did we handle and close all groups?
			return sizes.Count == 0 && numDoneInGroup == 0 ? 1 : 0;
		}

		var numSols = 0;
		// If next letter is a "?", we branch
		var possible = s[0] == '?' ? new[] { '.', '#' } : new[] { s[0] };

		foreach (var c in possible)
		{
			if (c == '#')
			{
				// Extend current group
				numSols += NumSolutions(s[1..], sizes, numDoneInGroup + 1);
			}
			else
			{
				if (numDoneInGroup > 0)
				{
					// If we were in a group that can be closed, close it
					if (sizes.Count > 0 && sizes[0] == numDoneInGroup)
					{
						var newSizes = new List<int>(sizes);
						newSizes.RemoveAt(0);
						numSols += NumSolutions(s[1..], newSizes);
					}
				}
				else
				{
					// If we are not in a group, move on to next symbol
					numSols += NumSolutions(s[1..], sizes);
				}
			}
		}

		Cache2[key] = numSols;
		return numSols;
	}

	public override string SolvePart1()
	{
		return "";
		var rows = Parse(InputLines);
		var total = 0;


		for (var i = 0; i < rows.Count; i++)
		{
			var row = rows[i];
			var combination = GetCombinations(row);
			Console.WriteLine($"Row {i + 1}: {combination}");
			total += combination;
		}

		return total.ToString();
	}


	public override string SolvePart2()
	{
		var rows = Parse(InputLines).Select(s => FoldRow(s, 5 - 1)).ToList();
		var total = 0;

		for (var i = 0; i < rows.Count; i++)
		{
			var row = rows[i];
			var combination = NumSolutions(row.Springs + ".", row.Groups);
			Console.WriteLine($"Row 2 {i + 1}: {combination}");
			total += combination;
		}

		return total.ToString();
	}
}
