using System.Diagnostics;
using AdventOfCode.Abstractions;

namespace AdventOfCode2022.Day2;

public class Day2 : DaySolver
{
	public Day2(DaySolverOptions options) : base(options)
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
		if (opponent == "A")
		{
			if (me == "X") return 3;
			if (me == "Y") return 6;
			if (me == "Z") return 0;
		}

		if (opponent == "B")
		{
			if (me == "X") return 0;
			if (me == "Y") return 3;
			if (me == "Z") return 6;
		}

		if (opponent == "C")
		{
			if (me == "X") return 6;
			if (me == "Y") return 0;
			if (me == "Z") return 3;
		}

		throw new UnreachableException("welp ? ");
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
		if (opponentMove == "A")
		{
			if (gameOutcome == "X") return "C";
			if (gameOutcome == "Y") return "A";
			if (gameOutcome == "Z") return "B";
		}

		if (opponentMove == "B")
		{
			if (gameOutcome == "X") return "A";
			if (gameOutcome == "Y") return "B";
			if (gameOutcome == "Z") return "C";
		}

		if (opponentMove == "C")
		{
			if (gameOutcome == "X") return "B";
			if (gameOutcome == "Y") return "C";
			if (gameOutcome == "Z") return "A";
		}

		throw new UnreachableException("welp ? ");
	}
}
