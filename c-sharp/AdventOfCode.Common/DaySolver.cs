using System.Text;

namespace AdventOfCode.Common;

/// <summary>
///     Abstract class that represents a generic solver for a day of Advent of Code.
///     It is a base class for all solvers and provides a common interface of methods for them,
///     as well as a common implementation for input file reading.
/// </summary>
public abstract class DaySolver : IDaySolver
{
	/// <summary>
	///     A constructor that reads the input text according to <paramref name="options" /> and stores its content.
	/// </summary>
	protected DaySolver(DaySolverOptions options)
	{
		try
		{
			using var reader = options.InputReader ?? File.OpenText(options.InputFilepath);
			Input = reader.ReadToEnd();
		}
		catch (FileNotFoundException e)
		{
			throw new ArgumentException($"Input file \"{e.FileName}\" was not found.", e);
		}
		catch (IOException e)
		{
			throw new ArgumentException($"An error occurred while reading the input file \"{options.InputFilepath}\".",
				e);
		}
	}

	/// <summary>
	///     The content that was read from the input file.
	/// </summary>
	protected string Input { get; }

	/// <summary>
	///     The lines of the input file (excluding one last empty line after newline break).
	/// </summary>
	protected IEnumerable<string> InputLines
	{
		get
		{
			using StringReader reader = new(Input);
			while (reader.ReadLine() is { } line) yield return line;
		}
	}


	public abstract string Day { get; }
	public abstract string Year { get; }

	/// <summary>
	///     A method that solves the first part of the day puzzle.
	/// </summary>
	/// <returns>The solution of the first part of the day puzzle.</returns>
	public abstract string SolvePart1();

	/// <summary>
	///     A method that solves the second part of the day puzzle.
	/// </summary>
	/// <returns>The solution of the second part of the day puzzle.</returns>
	public abstract string SolvePart2();

	/// <summary>
	///     Returns an enumerator that iterates over <see cref="Input" /> lines using <see cref="ReadOnlySpan{char}" />.
	/// </summary>
	/// <remarks>
	///     Note that the enumerator does not skip the last empty line after newline break as <see cref="InputLines" /> does.
	/// </remarks>
	protected SpanLineEnumerator EnumerateInputLines()
	{
		return Input.AsSpan().EnumerateLines();
	}
}
