namespace AdventOfCode2022.Day1;

public static class Extensions
{
	public static IEnumerable<IList<TSource>> Split<TSource>(this IEnumerable<TSource> source,
		Predicate<TSource> splitOn)
	{
		ArgumentNullException.ThrowIfNull(source);
		var current = new List<TSource>();
		foreach (var item in source)
		{
			if (splitOn(item))
			{
				yield return current;
				current = new List<TSource>();
			}
			else
			{
				current.Add(item);
			}
		}

		if (current.Any())
		{
			yield return current;
		}
	}
}
