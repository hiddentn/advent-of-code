using AdventOfCode.Common;

namespace AdventOfCode._2023.Day8;

public record Node(string Name, string Left, string Right);

public class Day8Solver(DaySolverOptions options) : DaySolver(options)
{
	public override string Day => "8";
	public override string Year => "2023";


	private static (List<char> instructions, List<Node> nodes) Parse(IEnumerable<string> inputLines)
	{
		var lines = inputLines.ToList();
		var instructions = lines[0].Trim().AsEnumerable().ToList();
		var nodes = new List<Node>();
		var nodesStr = lines.Skip(2);
		foreach (var nodeStr in nodesStr)
		{
			var nodeParts = nodeStr.Split("=");
			var node = nodeParts[0].Trim();
			var lr = nodeParts[1].Replace("(", "").Replace(")", "").Split(",");
			var left = lr[0].Trim();
			var right = lr[1].Trim();
			nodes.Add(new Node(node, left, right));
		}

		return (instructions, nodes);
	}

	public override string SolvePart1()
	{
		var (instructions, nodes) = Parse(InputLines);
		var leftMaps = nodes.ToDictionary(n => n.Name, n => n.Left);
		var rightMaps = nodes.ToDictionary(n => n.Name, n => n.Right);

		var steps = 0;
		var currentNode = "AAA";
		const string targetNode = "ZZZ";

		while (true)
		{
			var instruction = instructions[steps % instructions.Count];
			currentNode = instruction == 'L' ? leftMaps[currentNode] : rightMaps[currentNode];
			steps++;
			if (currentNode == targetNode)
			{
				return steps.ToString();
			}
		}
	}


	public string SolvePart2Old()
	{
		var (instructions, nodes) = Parse(InputLines);

		var stepCount = 0;
		var currentNodes = nodes.Where(n => n.Name.EndsWith('A')).ToArray();
		while (true)
		{
			var instruction = instructions[stepCount % instructions.Count];

			currentNodes = instruction == 'L'
				? currentNodes.Select(n => nodes.First(n1 => n1.Name == n.Left)).ToArray()
				: currentNodes.Select(n => nodes.First(n1 => n1.Name == n.Right)).ToArray();
			stepCount++;

			if (stepCount % 10000 == 0)
			{
				Console.WriteLine($" s-> {stepCount}");
			}

			if (currentNodes.All(n => n.Name.EndsWith('Z')))
			{
				return stepCount.ToString();
			}
		}
	}

	public override string SolvePart2()
	{
		var (instructions, nodes) = Parse(InputLines);

		var leftMaps = nodes.ToDictionary(n => n.Name, n => n.Left);
		var rightMaps = nodes.ToDictionary(n => n.Name, n => n.Right);

		var cycles = new List<long>();
		var currentNodes = nodes.Where(n => n.Name.EndsWith('A')).Select(n => n.Name).ToArray();

		foreach (var name in currentNodes)
		{
			var cycle = 0;
			var currentNode = name;
			while (true)
			{
				var instruction = instructions[cycle % instructions.Count];
				currentNode = instruction == 'L' ? leftMaps[currentNode] : rightMaps[currentNode];
				cycle++;
				if (!currentNode.EndsWith('Z')) continue;
				cycles.Add(cycle);
				break;
			}
		}

		// largest common multiple
		return MathNet.Numerics.Euclid.LeastCommonMultiple(cycles).ToString();
	}
}
