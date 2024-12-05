using AdventOfCode.Common;

namespace AdventOfCode._2024.Day5;

public class Day5Solver(DaySolverOptions options) : DaySolver(options)
{
	public override string Day => "5";
	public override string Year => "2024";


	private (List<(int X, int Y)>, List<List<int>> Order) Parse()
	{
		var constraints = new List<(int X, int Y)>();
		var order = new List<List<int>>();
		foreach (var line in InputLines)
		{
			if (!string.IsNullOrWhiteSpace(line))
			{
				if (line.Contains('|'))
				{
					var parts = line.Split('|');
					var x = int.Parse(parts[0]);
					var y = int.Parse(parts[1]);
					constraints.Add((x, y));
				}
				else
				{
					var lineOrder = line.Split(',').Select(int.Parse).ToList();
					order.Add(lineOrder);
				}
			}
		}

		return (constraints, order);
	}

	private static bool IsValidOrder(List<int> order, List<(int X, int Y)> constraints)
	{
		var valid = true;

		for (var i = 0; i < order.Count - 1; i++)
		{
			var current = order[i];

			var beforeConstraints = constraints.Where(c => c.Y == current);
			var afterConstraints = constraints.Where(c => c.X == current);

			// check is all the before beforeConstraints are before the current

			var before = order.Take(i);
			var after = order.Skip(i + 1);

			var beforeValid = beforeConstraints.All(c => !order.Contains(c.X) || before.Contains(c.X));
			var afterValid = afterConstraints.All(c => !order.Contains(c.Y) || after.Contains(c.Y));

			if (beforeValid && afterValid) continue;

			valid = false;
			break;
		}

		return valid;
	}

	public override string SolvePart1()
	{
		var (constraints, orders) = Parse();

		var validOrders = new List<List<int>>();
		foreach (var order in orders)
		{
			if (IsValidOrder(order, constraints))
			{
				validOrders.Add(order);
			}
		}

		var result = validOrders.Select(i =>
		{
			var middleOfOrder = i.Count / 2;
			return i[middleOfOrder];
		});

		return result.Sum().ToString();
	}

	private static List<int> FixInvalidOrder(List<int> order, List<(int X, int Y)> constraints)
	{
		order.Sort((a, b) =>
		{
			var constraintA = constraints.FirstOrDefault(c => c.X == a && c.Y == b);
			if (constraintA != default)
			{
				return -1;
			}

			var constraintB = constraints.FirstOrDefault(c => c.X == b && c.Y == a);

			return constraintB != default ? 1 : 0;
		});

		return order;
	}

	public override string SolvePart2()
	{
		var (constraints, orders) = Parse();

		var inValidOrders = new List<List<int>>();
		foreach (var order in orders)
		{
			if (!IsValidOrder(order, constraints))
			{
				inValidOrders.Add(order);
			}
		}

		var fixedInValidOrders = inValidOrders.Select(o => FixInvalidOrder(o, constraints)).ToList();


		var result = fixedInValidOrders.Select(i =>
		{
			var middleOfOrder = i.Count / 2;
			return i[middleOfOrder];
		});

		return result.Sum().ToString();
	}
}
