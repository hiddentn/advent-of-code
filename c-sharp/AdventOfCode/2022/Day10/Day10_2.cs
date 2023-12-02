using System.Text;
using AdventOfCode.Common;

namespace AdventOfCode._2022.Day10;

public enum Operation
{
	Noop,
	Addx
}

public class Instruction
{
	public readonly int Argument;
	public readonly Operation Operation;

	public Instruction(Operation operation, int argument)
	{
		Operation = operation;
		Argument = argument;
	}
}

public abstract class Cpu
{
	public int X = 1;
	public abstract void Tick();

	public void Exec(Instruction ins)
	{
		switch (ins.Operation)
		{
			case Operation.Noop:
				Tick();
				break;
			case Operation.Addx:
				Tick();
				Tick();
				X += ins.Argument;
				break;
		}
	}
}

public class Cpu1 : Cpu
{
	private int _cycle = 20;
	public int Str;

	public override void Tick()
	{
		if (++_cycle % 40 == 0) Str += (_cycle - 20) * X;
	}
}

public class Cpu2 : Cpu
{
	public readonly StringBuilder Sb = new();
	public int Cycle;

	public override void Tick()
	{
		if (Cycle % 40 == 0) Sb.Append('\n');
		var posix = Cycle++ % 40;
		if (posix == X || posix == X - 1 || posix == (X + 1) % 40)
			Sb.Append('█');
		else
			Sb.Append(' ');
	}
}

public class Day10_2Solver : DaySolver
{
	public readonly List<Instruction> Program = new();

	public Day10_2Solver(DaySolverOptions options) : base(options)
	{
		foreach (var s in InputLines)
		{
			var parts = s.Split(' ');
			var value = parts.Length > 1 ? int.Parse(parts[1]) : 0;
			Program.Add(new Instruction(Enum.Parse<Operation>(parts[0].ToUpper()), value));
		}
	}

	public override string Day => "10";
	public override string Year => "2022";


	public override string SolvePart1()
	{
		var cpu = new Cpu1();
		foreach (var ins in Program) cpu.Exec(ins);
		return cpu.Str.ToString();
	}

	public override string SolvePart2()
	{
		var cpu = new Cpu2();
		foreach (var ins in Program) cpu.Exec(ins);
		return cpu.Sb.ToString();
	}
}
