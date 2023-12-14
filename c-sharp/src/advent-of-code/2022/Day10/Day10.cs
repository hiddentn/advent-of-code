using AdventOfCode.Common;

namespace AdventOfCode._2022.Day10;

public class Day10Solver : DaySolver
{
	private readonly Dictionary<string, int> _actionCycles = new()
	{
		{ "addx", 2 },
		{ "noop", 1 }
	};

	private readonly List<int> _cycles = new()
	{
		20,
		60,
		100,
		140,
		180,
		220
	};

	public Day10Solver(DaySolverOptions options) : base(options)
	{
	}

	public override string Day => "10";
	public override string Year => "2022";

	public override string SolvePart1()
	{
		var actions = InputLines
			.Select(a => (a, _actionCycles[a.Split(" ").First()]))
			.ToList();

		var values = PlaybackActions(actions, 1);

		var res = 0;
		foreach (var cycle in _cycles)
		{
			// select the  previous to the first item where we hit this cycle
			var index = values.IndexOf(values.First(v => v.cycle >= cycle));
			var value = values[index - 1].value;
			res += cycle * value;
		}

		return res.ToString();
	}

	public override string SolvePart2()
	{
		var actions = InputLines
			.Select(a => (a, _actionCycles[a.Split(" ").First()]))
			.ToList();
		var values = PlaybackActions(actions, 1);

		var Width = 40;
		var Height = 6;

		var screen = new char[Height, Width];
		for (var i = 0; i < Height; i++)
		for (var j = 0; j < Width; j++)
			screen[i, j] = '.';

		var end = values.Last().cycle;
		// draw
		for (var i = 0; i < end; i++)
		{
		}


		// render
		for (var i = 0; i < Height; i++)
		{
			var line = "";
			for (var j = 0; j < Width; j++) line += screen[i, j];

			Console.WriteLine(line);
		}


		return "Not Implemented";

		var x = 1;
		Console.WriteLine(File.ReadAllText("Day10.txt").Split(new[] { Environment.NewLine, " " }, 0)
			.Select((x, i) => (index: i, addrx: int.TryParse(x, out var parsed) ? parsed : 0))
			.Select(y => (y.index, addrx: x += y.addrx)).Where(y => y.index % 40 == 19)
			.Sum(y => y.addrx * (y.index + 1)));
	}

	private List<(int cycle, int value)> PlaybackActions(List<(string action, int count)> actions, int defaut)
	{
		var values = new List<(int cycle, int value)>
		{
			(0, defaut)
		};
		foreach (var action in actions)
			if (action.action.StartsWith("addx"))
			{
				var value = int.Parse(action.action.Split(" ").Last());
				values.Add((values.Last().cycle + action.count, values.Last().value + value));
			}
			else
			{
				values.Add((values.Last().cycle + action.count, values.Last().value));
			}

		return values;
	}
}
