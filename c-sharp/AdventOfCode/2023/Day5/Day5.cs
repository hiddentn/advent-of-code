using System.Globalization;
using AdventOfCode.Common;

namespace AdventOfCode._2023.Day5;

public record MappingRange(long Destination, long Source, long Interval)
{
	public bool InRange(long input)
	{
		return input >= Source && input <= Source + Interval;
	}


	public bool InReverseRange(long input)
	{
		return input >= Destination && input <= Destination + Interval;
	}

	public long Map(long input)
	{
		return Destination + input - Source;
	}

	public long ReverseMap(long input)
	{
		return Source + input - Destination;
	}
};

public class Day5Solver : DaySolver
{
	private static readonly List<string> ConversionTable = new()
	{
		"seed-to-soil",
		"soil-to-fertilizer",
		"fertilizer-to-water",
		"water-to-light",
		"light-to-temperature",
		"temperature-to-humidity",
		"humidity-to-location",
	};

	private static Dictionary<string, List<MappingRange>> MappingTable;

	public Day5Solver(DaySolverOptions options) : base(options)
	{
	}

	public override string Day => "5";
	public override string Year => "2023";


	public override string SolvePart1()
	{
		var seedsStr = InputLines.First().Split(":")[1].Trim().Split(" ")
			.ToList();
		var seeds = seedsStr.Select(long.Parse).ToArray();
		var mappingTable = GetMappingTable(InputLines.Skip(2).ToList());
		var min = long.MaxValue;
		foreach (var seed in seeds)
		{
			var number = seed;
			foreach (var conversion in ConversionTable)
			{
				var mappingRange = mappingTable[conversion];
				var mappingRangeForNumber = mappingRange.FirstOrDefault(x => x.InRange(number));
				number = mappingRangeForNumber?.Map(number) ?? number;
			}

			min = Math.Min(min, number);
		}


		return min.ToString();
	}

	public override string SolvePart2()
	{
		var seedsRanges = InputLines.First().Split(":")[1].Trim().Split(" ").Select(long.Parse).Chunk(2).ToList();
		MappingTable = GetMappingTable(InputLines.Skip(2).ToList());

		var min = long.MaxValue;
		foreach (var seedsRange in seedsRanges)
		{
			var start = seedsRange[0];
			var count = seedsRange[1];

			var max = start + count - 1;

			for (var i = 0; i < count; i++)
			{
				var number = start + i;
				var init = start + i;

				foreach (var conversion in ConversionTable)
				{
					var mappingRange = MappingTable[conversion];
					var mappingRangeForNumber = mappingRange.FirstOrDefault(x => x.InRange(number));
					number = mappingRangeForNumber?.Map(number) ?? number;
				}

				min = Math.Min(min, number);
			}
		}

		return min.ToString();
	}


	public string SolvePart2BruteForce()
	{
		var seedsRanges = InputLines.First().Split(":")[1].Trim().Split(" ").Select(long.Parse).Chunk(2).ToList();
		MappingTable = GetMappingTable(InputLines.Skip(2).ToList());

		var min = long.MaxValue;
		foreach (var seedsRange in seedsRanges)
		{
			var start = seedsRange[0];
			var count = seedsRange[1];

			var max = start + count - 1;

			for (var i = 0; i < count; i++)
			{
				var number = start + i;
				var init = start + i;

				foreach (var conversion in ConversionTable)
				{
					var mappingRange = MappingTable[conversion];
					var mappingRangeForNumber = mappingRange.FirstOrDefault(x => x.InRange(number));
					number = mappingRangeForNumber?.Map(number) ?? number;
				}

				min = Math.Min(min, number);
			}
		}

		return min.ToString();
	}


	public string SolvePart2Var1()
	{
		var seedsRanges = InputLines.First().Split(":")[1].Trim().Split(" ").Select(long.Parse).Chunk(2).ToList();
		MappingTable = GetMappingTable(InputLines.Skip(2).ToList());


		var number = MappingTable["humidity-to-location"].Min(x => x.Destination);
		foreach (var reverseConversion in ConversionTable.AsEnumerable().Reverse())
		{
			var mappingRange = MappingTable[reverseConversion];
			var mappingRangeForNumber = mappingRange.FirstOrDefault(x => x.InReverseRange(number));
			number = mappingRangeForNumber?.ReverseMap(number) ?? number;
		}

		// find the range of the number in seed seedsRanges
		var seedsRangesForNumber = seedsRanges.Where(x => x[0] <= number && x[0] + x[1] >= number).ToList();


		Console.WriteLine($"Number: {number}");
		return seedsRangesForNumber.ToString();
	}


	private List<long[]> MergeOverlaps(List<long[]> seedsRanges)
	{
		var merged = new List<long[]>();

		var sorted = seedsRanges.OrderBy(x => x[0]).ToList();

		var current = seedsRanges[0];
		for (var i = 1; i < sorted.Count; i++)
		{
			var next = sorted[i];
			if (current[0] + current[1] >= next[0])
			{
				current[1] = next[0] + next[1] - current[0];
			}
			else
			{
				merged.Add(current);
				current = next;
			}
		}

		merged.Add(current);
		return merged;
	}


	private static Dictionary<string, List<MappingRange>> GetMappingTable(IReadOnlyList<string> input)
	{
		var mappingTables = new Dictionary<string, List<MappingRange>>();

		var index = 0;
		while (index < input.Count)
		{
			var currentMap = input[index].Split(" ")[0];
			var currentMappingTable = new List<MappingRange>();
			index++;
			while (index < input.Count && !string.IsNullOrWhiteSpace(input[index]))
			{
				var line = input[index];
				var parts = line.Split(" ");
				var sourceCategory = long.Parse(parts[0]);
				var destinationCategory = long.Parse(parts[1]);
				var interval = long.Parse(parts[2]);
				currentMappingTable.Add(new MappingRange(sourceCategory, destinationCategory, interval));
				index++;
			}

			mappingTables.Add(currentMap, currentMappingTable);
			index++;
		}

		return mappingTables;
	}
}
