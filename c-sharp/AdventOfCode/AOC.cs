using AdventOfCode.Common;

namespace AdventOfCode;

public static class AOC
{
	// Will make sure the files end up in the main project directory
	private static string ProjectDirectory =>
		Directory.GetParent(path: Environment.CurrentDirectory)?.Parent?.Parent?.FullName ??
		throw new FileNotFoundException();

	public static async Task<T> SetupContext<T>() where T : IDaySolver
	{
		// all IDaySolver implementations  have a Day and Year property
		// we need to get them without instantiating the class

		var day = typeof(T).GetProperty("Day")?.GetValue(null) as string ??
		          throw new InvalidOperationException("Day property not found");

		var year = typeof(T).GetProperty("Year")?.GetValue(null) as string ??
		           throw new InvalidOperationException("Year property not found");





		var options = new DaySolverOptions
		{
			InputFilepath = Path.Combine(ProjectDirectory, "Day10.txt")
		};
		return (T)Activator.CreateInstance(typeof(T), options);
	}
}
