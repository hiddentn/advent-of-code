namespace AdventOfCode2022.Day3;

public static class Extensions
{
	public static IEnumerable<string> SplitBy(this string str, int chunkLength)
	{
		if (string.IsNullOrEmpty(str)) throw new ArgumentException();
		if (chunkLength < 1) throw new ArgumentException();

		for (var i = 0; i < str.Length; i += chunkLength)
		{
			if (chunkLength + i > str.Length)
				chunkLength = str.Length - i;

			yield return str.Substring(i, chunkLength);
		}
	}


	public static IEnumerable<char> Intersect(this IEnumerable<string> items)
	{
		// return the intersection of all the strings
		return items.Aggregate((a, b) => new string(a.Intersect(b).ToArray()));
	}
}
