using System.Diagnostics;
using AdventOfCode.Abstractions;

namespace AdventOfCode2022.Day2;

public class Day2Solver : DaySolver
{
	public Day2Solver(DaySolverOptions options) : base(options)
	{
	}


	private readonly IDictionary<string, int> _moveScoreMap = new Dictionary<string, int>
	{
		{ "X", 1 },
		{ "Y", 2 },
		{ "Z", 3 },
		// part 2
		{ "A", 1 },
		{ "B", 2 },
		{ "C", 3 },
	};


	private readonly IDictionary<string, int> _outComeScoreMap = new Dictionary<string, int>
	{
		{ "X", 0 },
		{ "Y", 3 },
		{ "Z", 6 },
	};


	public override string SolvePart1() =>
		InputLines
			.Select(line => line.Split(' '))
			.Select(game => _moveScoreMap[game[1]] + GetMyScore(game[0], game[1]))
			.Sum()
			.ToString();

	public override string SolvePart2() =>
		InputLines
			.Select(line => line.Split(' '))
			.Select(game => _moveScoreMap[GetMyMove(game[0], game[1])] + _outComeScoreMap[game[1]])
			.Sum()
			.ToString();

	/// <summary>
	/// for opponent
	/// A for Rock,
	/// B for Paper,
	/// C for Scissors.
	/// for me
	/// X for Rock,
	/// Y for Paper,
	/// Z for Scissors.
	/// </summary>
	/// <param name="opponent"></param>
	/// <param name="me"></param>
	/// <returns></returns>
	/// <exception cref="UnreachableException"></exception>
	private static int GetMyScore(string opponent, string me)
	{
		return opponent switch
		{
			"B" when me == "Y" => 3,
			"A" when me == "X" => 3,
			"C" when me == "Z" => 3,

			"C" when me == "X" => 6,
			"A" when me == "Y" => 6,
			"B" when me == "Z" => 6,

			"A" when me == "Z" => 0,
			"B" when me == "X" => 0,
			"C" when me == "Y" => 0,

			_ => throw new UnreachableException("welp ? ")
		};
	}


	/// <summary>
	/// A for Rock,
	/// B for Paper,
	/// C for Scissors.
	/// X means lose,
	/// Y means  draw,
	/// Z win.
	/// </summary>
	/// <param name="opponentMove"></param>
	/// <param name="gameOutcome"></param>
	/// <returns></returns>
	/// <exception cref="UnreachableException"></exception>
	private static string GetMyMove(string opponentMove, string gameOutcome)
	{
		return opponentMove switch
		{
			"A" when gameOutcome == "Y" => "A",
			"B" when gameOutcome == "X" => "A",
			"C" when gameOutcome == "Z" => "A",

			"A" when gameOutcome == "Z" => "B",
			"B" when gameOutcome == "Y" => "B",
			"C" when gameOutcome == "X" => "B",

			"A" when gameOutcome == "X" => "C",
			"B" when gameOutcome == "Z" => "C",
			"C" when gameOutcome == "Y" => "C",

			_ => throw new UnreachableException("welp ? ")
		};
	}
}
