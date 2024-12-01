using AdventOfCode.Common;

namespace AdventOfCode._2023.Day2;

public record Set(int Red, int Blue, int Green)
{
	public bool IsValid => Red <= 12 && Green <= 13 && Blue <= 14;
}

public record Game(int Id, List<Set> Sets)
{
	public int MaxRed => Sets.Max(s => s.Red);
	public int MaxBlue => Sets.Max(s => s.Blue);
	public int MaxGreen => Sets.Max(s => s.Green);

	public bool IsValid => Sets.All(s => s.IsValid);

	public int Power => MaxRed * MaxBlue * MaxGreen;
}

public class Day2Solver(DaySolverOptions options) : DaySolver(options)
{
	public override string Day => "2";
	public override string Year => "2023";

	private static Game Parse(string input)
	{
		var parts = input.Split(":");
		var id = int.Parse(parts[0].Replace("Game ", "").Trim());

		var sets = new List<Set>();
		var gameSets = parts[1].Split(";").Select(s => s.Trim());
		foreach (var gameSet in gameSets)
		{
			var colors = gameSet.Split(",").Select(s => s.Trim());
			var red = 0;
			var blue = 0;
			var green = 0;
			foreach (var color in colors)
				if (color.EndsWith("red"))
					red += int.Parse(color.Split(" ")[0]);
				else if (color.EndsWith("blue"))
					blue += int.Parse(color.Split(" ")[0]);
				else if (color.EndsWith("green")) green += int.Parse(color.Split(" ")[0]);

			sets.Add(new Set(red, blue, green));
		}

		return new Game(id, sets);
	}

	public override string SolvePart1()
	{
		var games = InputLines.Select(l => Parse(l));
		// only 12 red cubes, 13 green cubes, and 14 blue cubes
		return games.Where(g => g.IsValid)
			.Select(g => g.Id)
			.Sum()
			.ToString();
	}

	public override string SolvePart2()
	{
		var games = InputLines.Select(l => Parse(l));
		// only 12 red cubes, 13 green cubes, and 14 blue cubes
		return games.Select(g => g.Power)
			.Sum()
			.ToString();
	}
}
