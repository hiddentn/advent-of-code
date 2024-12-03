using System.Text.RegularExpressions;
using AdventOfCode.Common;

namespace AdventOfCode._2024.Day3;

public partial class Day3Solver(DaySolverOptions options) : DaySolver(options)
{
	public override string Day => "3";
	public override string Year => "2024";


	[GeneratedRegex(@"mul\((\d{1,3}),(\d{1,3})\)", RegexOptions.Multiline)]
	private static partial Regex MultiplyRegex();


	[GeneratedRegex(@"do\(\)|don't\(\)|mul\((\d{1,3}),(\d{1,3})\)", RegexOptions.Multiline)]
	private static partial Regex MultiplyRegexV2();

	private IEnumerable<(int X, int Y)> Parse()
	{
		var regex = MultiplyRegex();

		var matches = InputLines.SelectMany(x => regex.Matches(x));
		foreach (var match in matches)
		{
			var x = int.Parse(match.Groups[1].Value);
			var y = int.Parse(match.Groups[2].Value);
			yield return (x, y);
		}
	}

	private IEnumerable<(bool DO, int X, int Y)> ParseV2()
	{
		var regex = MultiplyRegexV2();

		var matches = InputLines.SelectMany(x => regex.Matches(x));
		var doOrDont = true;
		foreach (var match in matches)
		{
			if (match.Value == "do()")
			{
				doOrDont = true;
				continue;
			}

			if (match.Value == "don't()")
			{
				doOrDont = false;
				continue;
			}

			// if (string.IsNullOrWhiteSpace(match.Groups["X"].Value) || string.IsNullOrWhiteSpace(match.Groups["Y"].Value))
			// {
			// 	Console.WriteLine(match.Value);
			// }

			var x = int.Parse(match.Groups["1"].Value);
			var y = int.Parse(match.Groups["2"].Value);
			yield return (doOrDont, x, y);
		}
	}

	public override string SolvePart1()
	{
		return Parse().Sum(p => p.X * p.Y).ToString();
	}


	public override string SolvePart2()
	{
		return ParseV2().Sum(p => p.DO ? p.X * p.Y : 0).ToString();
	}
}
