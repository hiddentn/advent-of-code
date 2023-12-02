using AdventOfCode._2022.Day1;
using AdventOfCode.Common;

namespace AdventOfCode._2022.Day11;

public enum Operation

{
	Add = 1,
	Subtract,
	Multiply,
}

public class Monkey
{
	public readonly int Id;

	private readonly Operation _operation;
	private readonly int? _operationValue;

	private readonly int _testValue;
	private readonly int _whenTrueMonkeyId;
	private readonly int _whenFalseMonkeyId;

	private int _inspections = 0;
	private List<int> _items;


	public Monkey(List<string> instructions)
	{
		Id = instructions[0]
			.Split(' ')
			.Last()
			.Split(':')
			.First()
			.AsInt();
		_items = instructions[1]
			.Split(':')
			.Last()
			.Split(',')
			.Select(x => x.Trim().AsInt())
			.ToList();
		(_operation, _operationValue) = GetOpAndValue(instructions[2]
			.Trim()
			.Split('=')
			.Last()
			.Trim());

		_testValue = instructions[3]
			.Trim()
			.Split(' ')
			.Last()
			.AsInt();

		_whenTrueMonkeyId = instructions[4]
			.Trim()
			.Split(' ')
			.Last()
			.AsInt();

		_whenFalseMonkeyId = instructions[5]
			.Trim()
			.Split(' ')
			.Last()
			.AsInt();
	}


	public void AddItem(int item)
	{
		_items.Add(item);
	}

	public void Action(List<Monkey> monkies)
	{
		if (_items.Count == 0)
		{
			return;
		}


		for (var i = 0; i < _items.Count; i++)
		{
			_items[i] = ApplyOperation(_items[i], _operation, _operationValue);
			_inspections++;
		}

		for (var i = 0; i < _items.Count; i++)
		{
			_items[i] = Convert.ToInt32(Math.Round(_items[i] / 3.0));
		}

		foreach (var t in _items)
		{
			if (Test(t, _testValue))
			{
				// when true
				monkies.First(x => x.Id == _whenTrueMonkeyId).AddItem(t);
			}
			else
			{
				// when false
				monkies.First(x => x.Id == _whenFalseMonkeyId).AddItem(t);
			}
		}

		_items = new List<int>();
	}

	private static (Operation op, int? value ) GetOpAndValue(string op)
	{
		var parts = op.Split(' ');
		var operation = parts[1] switch
		{
			"+" => Operation.Add,
			"*" => Operation.Multiply,
			"-" => Operation.Subtract,
			_ => throw new Exception("Unknown operation")
		};

		if (parts[2] == "old")
		{
			return (operation, null);
		}

		return (operation, parts[2].AsInt());
	}

	private static int ApplyOperation(int value, Operation operation, int? operationValue)
	{
		return operation switch
		{
			Operation.Add when operationValue is not null => (int)(value + operationValue),
			Operation.Subtract when operationValue is not null => (int)(value - operationValue),
			Operation.Multiply when operationValue is not null => (int)(value * operationValue),
			Operation.Multiply when operationValue is null => (value * value),
			_ => throw new Exception("Unknown operation")
		};
	}

	private static bool Test(int value, int testValue)
	{
		return value % testValue == 0;
	}
}

public class Day11Solver : DaySolver
{

	public override string Day => "11";
	public override string Year => "2022";

	private readonly List<Monkey> _monkeys;

	public Day11Solver(DaySolverOptions options) : base(options)
	{
		_monkeys = InputLines.Split(s => s.Length == 0)
			.Select(x => new Monkey(x.ToList()))
			.ToList();
	}

	public override string SolvePart1()
	{
		const int rounds = 20;


		foreach (var round in Enumerable.Range(1, rounds))
		{
			foreach (var monkey in _monkeys)
			{
				monkey.Action(_monkeys);
			}
		}


		throw new NotImplementedException();
	}

	public override string SolvePart2()
	{
		throw new NotImplementedException();
	}
}
