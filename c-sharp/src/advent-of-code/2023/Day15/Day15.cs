using AdventOfCode.Common;

namespace AdventOfCode._2023.Day15;

public class Day15Solver : DaySolver
{
	private record Step
	{
		private string Input { get; init; }
		public string Label { get; init; }
		public char Operation { get; init; }
		public int? FocalLength { get; init; }

		public Step(string input)
		{
			Input = input;
			if (input.Contains('='))
			{
				var parts = input.Split("=");
				Label = parts[0].Trim();
				FocalLength = int.Parse(parts[1].Trim());
				Operation = '=';
			}
			else
			{
				var parts = input.Split("-");
				Label = parts[0].Trim();
				Operation = '-';
			}
		}

		public int LabelHash => GetHash(Label);
		public int InputHash => GetHash(Input);

		private static int GetHash(string text)
		{
			var current = 0;
			foreach (var c in text)
			{
				var ascii = (int)c;
				current += ascii;
				current *= 17;
				current %= 256;
			}

			return current;
		}
	};

	private record Lens(string Label, int FocalLength);

	private class Box
	{
		public int Index { get; init; }
		private readonly List<Lens> _lenses = new();
		public bool HasLens => _lenses.Count != 0;

		public void RemoveLensWithLabel(string label)
		{
			var index = _lenses.FindIndex(l => l.Label == label);
			if (index >= 0)
			{
				_lenses.RemoveAt(index);
			}
		}

		public void AddOrReplaceLens(Lens lens)
		{
			var index = _lenses.FindIndex(l => l.Label == lens.Label);
			if (index >= 0)
			{
				// replace
				_lenses[index] = lens;
			}
			else
			{
				_lenses.Add(lens);
			}
		}

		public int FocusingPower()
		{
			return _lenses.Select((lens, i) => (1 + Index) * (i + 1) * lens.FocalLength).Sum();
		}
	}

	public Day15Solver(DaySolverOptions options) : base(options)
	{
	}

	public override string Day => "15";
	public override string Year => "2023";

	private static List<Step> Parse(string input) =>
		input
			.Split("\n")
			.SkipLast(1)
			.First()
			.Split(",")
			.Select(s => new Step(s.Trim()))
			.ToList();

	public override string SolvePart1()
	{
		var sum = 0;
		var commands = Parse(Input);
		foreach (var command in commands)
		{
			sum += command.InputHash;
		}

		return sum.ToString();
	}

	public override string SolvePart2()
	{
		var commands = Parse(Input);
		var boxes = new Dictionary<int, Box>();
		foreach (var command in commands)
		{
			var hash = command.LabelHash;
			var box = boxes.TryGetValue(hash, out var b) ? b : new Box { Index = hash };

			if (command.Operation == '-')
			{
				box.RemoveLensWithLabel(command.Label);
			}
			else
			{
				var lens = new Lens(command.Label, command.FocalLength!.Value);
				box.AddOrReplaceLens(lens);
			}

			boxes[hash] = box;
		}

		return boxes
			.Values
			.Where(b => b.HasLens)
			.Select((b) => b.FocusingPower())
			.Sum()
			.ToString();
	}
}
