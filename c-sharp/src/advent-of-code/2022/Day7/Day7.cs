using AdventOfCode.Common;

namespace AdventOfCode._2022.Day7;

public class Day7Solver(DaySolverOptions options) : DaySolver(options)
{
	public override string Day => "7";
	public override string Year => "2022";

	public override string SolvePart1()
	{
		// find all of the directories with a total size of at most 100000
		var commands = InputLines.ToList();
		var files = CreateFilesTree(commands);
		var directories = GetDirectorySizes(files);
		return directories.Where(x => x.Value <= 100000)
			.Sum(x => x.Value)
			.ToString();
	}

	public override string SolvePart2()
	{
		const int total = 70000000;
		const int needed = 30000000;
		// find all of the directories with a total size of at most 100000
		var commands = InputLines.ToList();
		var files = CreateFilesTree(commands);
		var directories = GetDirectorySizes(files);
		var root = directories.MaxBy(x => x.Value);
		var current = total - root.Value;
		return directories.Where(x => x.Value + current >= needed)
			.MinBy(x => x.Value)
			.Value
			.ToString();
	}

	private static IEnumerable<(string FilePath, int Size)> CreateFilesTree(IReadOnlyList<string> commands)
	{
		var files = new List<(string FilePath, int Size)>();
		var currentPath = string.Empty;
		for (var index = 0; index < commands.Count; index++)
		{
			var command = commands[index];
			if (!command.StartsWith("$")) continue;
			var parts = command.Split(' ');
			switch (parts[1])
			{
				case "cd" when parts[2] == "/":
					break;
				case "cd" when parts[2] == "..":
				{
					var lastSlash = currentPath.LastIndexOf('/');
					currentPath = currentPath[..lastSlash];
					break;
				}
				case "cd":
					currentPath = currentPath + "/" + parts[2];
					break;

				case "ls":
				{
					var nextLine = commands[index + 1];
					while (!nextLine.StartsWith("$"))
					{
						if (!nextLine.StartsWith("dir"))
						{
							var fileParts = nextLine.Split(' ');
							var fileName = fileParts[1];
							var fileSize = int.Parse(fileParts[0]);
							files.Add((currentPath + "/" + fileName, fileSize));
						}

						if (index + 1 == commands.Count - 1) break;
						index++;
						nextLine = commands[index + 1];
					}

					break;
				}
			}
		}

		return files;
	}

	private static Dictionary<string, int> GetDirectorySizes(IEnumerable<(string FilePath, int Size)> files)
	{
		// we have all of the files, now we recursively need to find the total size of each directory
		var directories = new Dictionary<string, int>();
		foreach (var file in files)
		{
			var paths = file.FilePath.Split("/").SkipLast(1).ToList();

			var currentDirectory = string.Empty;
			foreach (var t in paths)
			{
				if (t != string.Empty) ;
				{
					currentDirectory += "/" + t;
				}
				if (!directories.ContainsKey(currentDirectory))
					directories.Add(currentDirectory, file.Size);
				else
					directories[currentDirectory] += file.Size;
			}
		}

		return directories;
	}
}
