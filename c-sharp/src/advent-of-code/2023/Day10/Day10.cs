using AdventOfCode.Common;

namespace AdventOfCode._2023.Day10;

// | is a vertical pipe connecting north and south.
// - is a horizontal pipe connecting east and west.
// 	L is a 90-degree bend connecting north and east.
// 	J is a 90-degree bend connecting north and west.
// 7 is a 90-degree bend connecting south and west.
// 	F is a 90-degree bend connecting south and east.
// 	. is ground; there is no pipe in this tile.
// 	S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.
enum TerrainType
{
	Vertical, // |,
	Horizontal, // -,
	TopRight, // L,
	TopLeft, // J,
	BottomLeft, // 7,
	BottomRight, // F,
	Empty, // .,
	Start, // S
}

class Node(int x, int y, TerrainType terrainType, Node? parent = null)
{
	public int X { get; } = x;
	public int Y { get; } = y;
	public TerrainType TerrainType { get; } = terrainType;
	public Node? Parent { get; } = parent;
	public bool isParent(int x, int y) => Parent != null && x == Parent.X && y == Parent.Y;

	public int Cost()
	{
		if (Parent == null) return 0;
		return Parent.Cost() + 1;
	}

	public List<Node> GetAdjacentNodes(TerrainType[][] terrainMap)
	{
		var adjacentNodes = new List<Node>();

		//  can go top or not
		if (TerrainType is TerrainType.Start or TerrainType.Vertical or TerrainType.TopRight or TerrainType.TopLeft)
		{
			// top
			// check if to the top node accepts bottom node
			if (X > 0 && terrainMap[X - 1][Y] is TerrainType.Vertical or TerrainType.BottomLeft
				    or TerrainType.BottomRight or TerrainType.Start && !isParent(X - 1, Y))
				adjacentNodes.Add(new Node(X - 1, Y, terrainMap[X - 1][Y], this));
		}

		// can go bottom or not
		if (TerrainType is TerrainType.Start or TerrainType.Vertical or TerrainType.BottomLeft
		    or TerrainType.BottomRight)
		{
			// bottom
			if (X < terrainMap.Length - 1 &&
			    terrainMap[X + 1][Y] is TerrainType.Vertical or TerrainType.TopRight or TerrainType.TopLeft
				    or TerrainType.Start && !isParent(X + 1, Y))
				adjacentNodes.Add(new Node(X + 1, Y, terrainMap[X + 1][Y], this));
		}

		// can go left or not
		if (TerrainType is TerrainType.Start or TerrainType.Horizontal or TerrainType.TopLeft or TerrainType.BottomLeft)
		{
			// left
			if (Y > 0 && terrainMap[X][Y - 1] is TerrainType.Horizontal or TerrainType.TopRight
				    or TerrainType.BottomRight or TerrainType.Start && !isParent(X, Y - 1))
				adjacentNodes.Add(new Node(X, Y - 1, terrainMap[X][Y - 1], this));
		}

		// can go right or not
		if (TerrainType is TerrainType.Start or TerrainType.Horizontal or TerrainType.TopRight
		    or TerrainType.BottomRight)
		{
			// right
			if (Y < terrainMap[X].Length - 1 &&
			    terrainMap[X][Y + 1] is TerrainType.Horizontal or TerrainType.TopLeft or TerrainType.BottomLeft
				    or TerrainType.Start && !isParent(X, Y + 1))
				adjacentNodes.Add(new Node(X, Y + 1, terrainMap[X][Y + 1], this));
		}

		return adjacentNodes;
	}
}

public class Day10Solver(DaySolverOptions options) : DaySolver(options)
{
	private static char[][] ParseInput(string input)
	{
		return input.Split('\n').Select(x => x.ToCharArray()).ToArray();
	}

	private static TerrainType[][] GetTerrainMap(char[][] map)
	{
		var terrainMap = new TerrainType[map.Length][];
		for (var i = 0; i < map.Length; i++)
		{
			terrainMap[i] = new TerrainType[map[i].Length];
			for (var j = 0; j < map[i].Length; j++)
			{
				terrainMap[i][j] = map[i][j] switch
				{
					'|' => TerrainType.Vertical,
					'-' => TerrainType.Horizontal,
					'L' => TerrainType.TopRight,
					'J' => TerrainType.TopLeft,
					'7' => TerrainType.BottomLeft,
					'F' => TerrainType.BottomRight,
					'.' => TerrainType.Empty,
					'S' => TerrainType.Start,
					_ => throw new ArgumentOutOfRangeException()
				};
			}
		}

		return terrainMap;
	}

	private static Node GetStartNode(TerrainType[][] terrainMap)
	{
		for (var i = 0; i < terrainMap.Length; i++)
		{
			for (var j = 0; j < terrainMap[i].Length; j++)
			{
				if (terrainMap[i][j] == TerrainType.Start)
				{
					return new Node(i, j, TerrainType.Start);
				}
			}
		}

		throw new Exception();
	}

	private static List<Node> GetDfsPath(TerrainType[][] terrainMap, Node startingNode)
	{
		// do a dfs to find the a path from start back to start
		var stack = new Stack<Node>();
		stack.Push(startingNode);
		while (stack.Count > 0)
		{
			var node = stack.Pop();
			var adjacentNodes = node.GetAdjacentNodes(terrainMap);
			foreach (var adjacentNode in adjacentNodes)
			{
				if (adjacentNode.TerrainType == TerrainType.Start)
				{
					var path = new List<Node>();
					var current = adjacentNode;
					while (current.Parent != null)
					{
						path.Add(current);
						current = current.Parent;
					}

					return path;
				}

				stack.Push(adjacentNode);
			}
		}

		throw new Exception();
	}

	private static bool IsTileInsidePolygon((int X, int Y) tilePosition, IReadOnlyList<(int X, int Y)> polygonVertices)
	{
		// Ray Casting Algorithm to determine if a point is inside a polygon (pipe loop)
		var isInside = false;
		// Iterates over adjacent positions of the polygon
		// For each pair, it checks if the horizontal line extending to the right intersects with the polygon edge
		var j = polygonVertices.Count - 1;
		for (var i = 0; i < polygonVertices.Count; i++)
		{
			var vertexA = polygonVertices[i];
			var vertexB = polygonVertices[j];

			if (vertexA.X < tilePosition.X && vertexB.X >= tilePosition.X
			    || vertexB.X < tilePosition.X && vertexA.X >= tilePosition.X)
			{
				if (vertexA.Y
				    + (tilePosition.X - vertexA.X)
				    / (vertexB.X - vertexA.X)
				    * (vertexB.Y - vertexA.Y)
				    < tilePosition.Y)
				{
					isInside = !isInside;
				}
			}

			j = i;
		}

		return isInside;
	}

	public override string Day => "10";
	public override string Year => "2023";


	public override string SolvePart1()
	{
		var map = ParseInput(Input);
		var terrainMap = GetTerrainMap(map);
		// get the x and y of the start node
		var start = GetStartNode(terrainMap);
		var path = GetDfsPath(terrainMap, start);
		var cost = path.First().Cost() / 2;
		return cost.ToString();
	}


	public override string SolvePart2()
	{
		var map = ParseInput(Input);
		var terrainMap = GetTerrainMap(map);
		// get the x and y of the start node
		var start = GetStartNode(terrainMap);

		var path = GetDfsPath(terrainMap, start);
		var polygon = path.Select(n => (n.X, n.Y)).ToArray();
		var total = 0;
		for (var i = 0; i < terrainMap.Length; i++)
		{
			for (var j = 0; j < terrainMap[i].Length; j++)
			{
				var position = (i, j);

				// Random pipes that are not connected to the main loop also count
				if (!polygon.Contains(position) && IsTileInsidePolygon(position, polygon))
				{
					total++;
				}
			}
		}

		return total.ToString();
	}
}
