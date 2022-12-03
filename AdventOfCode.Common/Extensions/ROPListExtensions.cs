namespace AdventOfCode.Common.Extensions;

public static class RopListExtensions
{
	public static TResult Bind<T, TResult>(this IEnumerable<T> enumerable, Func<IEnumerable<T>, TResult> func) =>
		func(enumerable);
}
