using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using AdventOfCode.Common.Helpers;

namespace AdventOfCode.Common.Geometry._3D;

// Adds parsing support to Point3D<T>.
public readonly partial struct Point3D<T>
	: ISpanParsable<Point3D<T>>, IParsable<Point3D<T>>
	where T : unmanaged, INumber<T>
{
	/// <summary>The count of values used in parsing.</summary>
	private const int _parsingValueCount = 3;

	/// <summary>The separator <see cref="char" /> used in parsing.</summary>
	private const char _parsingSeparatorChar = ',';

	/// <summary>The opening bracket <see cref="char" /> used in parsing.</summary>
	private const char _parsingOpeningBracketChar = '(';

	/// <summary>The closing bracket <see cref="char" /> used in parsing.</summary>
	private const char _parsingClosingBracketChar = ')';

	/// <summary>
	///     Parses specified string into a <see cref="Point3D{T}" />.
	/// </summary>
	/// <param name="s">The string to parse.</param>
	/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s" />.</param>
	/// <exception cref="ArgumentNullException"><paramref name="s" /> is <see langword="null" />.</exception>
	/// <exception cref="FormatException"><paramref name="s" /> is not in the correct format.</exception>
	/// <returns>A new <see cref="Point3D{T}" /> parsed from <paramref name="s" />.</returns>
	/// <remarks>
	///     The format of <paramref name="s" /> can be either
	///     <list type="bullet">
	///         <item>
	///             <c>(X, Y, Z)</c>
	///         </item>
	///         <item>
	///             <c>X, Y, Z</c>
	///         </item>
	///     </list>
	///     with whitespace between elements ignored where <c>X</c>, <c>Y</c> and <c>Z</c> are
	///     the string representations of the <see cref="X" />, <see cref="Y" /> and <see cref="Z" /> values.
	/// </remarks>
	public static Point3D<T> Parse(string s, IFormatProvider? provider = null)
	{
		ArgumentNullException.ThrowIfNull(s);
		return Parse(s.AsSpan(), provider);
	}

	/// <summary>
	///     Parses specified span of characters into a <see cref="Point3D{T}" />.
	/// </summary>
	/// <param name="s">The span of characters to parse.</param>
	/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s" />.</param>
	/// <exception cref="FormatException"><paramref name="s" /> is not in the correct format.</exception>
	/// <returns>A new <see cref="Point3D{T}" /> parsed from <paramref name="s" />.</returns>
	/// <remarks>
	///     The format of <paramref name="s" /> can be either
	///     <list type="bullet">
	///         <item>
	///             <c>(X, Y, Z)</c>
	///         </item>
	///         <item>
	///             <c>X, Y, Z</c>
	///         </item>
	///     </list>
	///     with whitespace between elements ignored where <c>X</c>, <c>Y</c> and <c>Z</c> are
	///     the string representations of the <see cref="X" />, <see cref="Y" /> and <see cref="Z" /> values.
	/// </remarks>
	public static Point3D<T> Parse(ReadOnlySpan<char> s, IFormatProvider? provider = null)
	{
		Span<T> values = stackalloc T[_parsingValueCount];
		try
		{
			TupleParsing.ParseValueTupleIntoSpan(
				s,
				provider,
				values,
				new TupleParsing.TupleParsingOptions(_parsingValueCount, _parsingSeparatorChar,
					_parsingOpeningBracketChar, _parsingClosingBracketChar)
			);
		}
		catch (FormatException e)
		{
			throw new FormatException($"Could not parse Point3D from \"{s}\".", e);
		}

		return new Point3D<T>(values[0], values[1], values[2]);
	}

	/// <summary>
	///     Tries to parse a string into a <see cref="Point3D{T}" />.
	/// </summary>
	/// <param name="s">The string to try to parse.</param>
	/// <param name="result">The result of parsing <paramref name="s" />.</param>
	/// <returns><see langword="true" /> if <paramref name="s" /> was successfully parsed; otherwise, <see langword="false" />.</returns>
	/// <remarks>
	///     The format of <paramref name="s" /> can be either
	///     <list type="bullet">
	///         <item>
	///             <c>(X, Y, Z)</c>
	///         </item>
	///         <item>
	///             <c>X, Y, Z</c>
	///         </item>
	///     </list>
	///     with whitespace between elements ignored where <c>X</c>, <c>Y</c> and <c>Z</c> are
	///     the string representations of the <see cref="X" />, <see cref="Y" /> and <see cref="Z" /> values.
	/// </remarks>
	public static bool TryParse([NotNullWhen(true)] string? s, out Point3D<T> result)
	{
		return TryParse(s, null, out result);
	}

	/// <summary>
	///     Tries to parse a string into a <see cref="Point3D{T}" />.
	/// </summary>
	/// <param name="s">The string to try to parse.</param>
	/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s" />.</param>
	/// <param name="result">The result of parsing <paramref name="s" />.</param>
	/// <returns><see langword="true" /> if <paramref name="s" /> was successfully parsed; otherwise, <see langword="false" />.</returns>
	/// <remarks>
	///     The format of <paramref name="s" /> can be either
	///     <list type="bullet">
	///         <item>
	///             <c>(X, Y, Z)</c>
	///         </item>
	///         <item>
	///             <c>X, Y, Z</c>
	///         </item>
	///     </list>
	///     with whitespace between elements ignored where <c>X</c>, <c>Y</c> and <c>Z</c> are
	///     the string representations of the <see cref="X" />, <see cref="Y" /> and <see cref="Z" /> values.
	/// </remarks>
	public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, out Point3D<T> result)
	{
		ArgumentNullException.ThrowIfNull(s);
		return TryParse(s.AsSpan(), provider, out result);
	}

	/// <summary>
	///     Tries to parse a span of characters into a <see cref="Point3D{T}" />.
	/// </summary>
	/// <param name="s">The span of characters to try to parse.</param>
	/// <param name="result">The result of parsing <paramref name="s" />.</param>
	/// <returns><see langword="true" /> if <paramref name="s" /> was successfully parsed; otherwise, <see langword="false" />.</returns>
	/// <remarks>
	///     The format of <paramref name="s" /> can be either
	///     <list type="bullet">
	///         <item>
	///             <c>(X, Y, Z)</c>
	///         </item>
	///         <item>
	///             <c>X, Y, Z</c>
	///         </item>
	///     </list>
	///     with whitespace between elements ignored where <c>X</c>, <c>Y</c> and <c>Z</c> are
	///     the string representations of the <see cref="X" />, <see cref="Y" /> and <see cref="Z" /> values.
	/// </remarks>
	public static bool TryParse(ReadOnlySpan<char> s, out Point3D<T> result)
	{
		return TryParse(s, null, out result);
	}

	/// <summary>
	///     Tries to parse a span of characters into a <see cref="Point3D{T}" />.
	/// </summary>
	/// <param name="s">The span of characters to try to parse.</param>
	/// <param name="provider">An object that provides culture-specific formatting information about <paramref name="s" />.</param>
	/// <param name="result">The result of parsing <paramref name="s" />.</param>
	/// <returns><see langword="true" /> if <paramref name="s" /> was successfully parsed; otherwise, <see langword="false" />.</returns>
	/// <remarks>
	///     The format of <paramref name="s" /> can be either
	///     <list type="bullet">
	///         <item>
	///             <c>(X, Y, Z)</c>
	///         </item>
	///         <item>
	///             <c>X, Y, Z</c>
	///         </item>
	///     </list>
	///     with whitespace between elements ignored where <c>X</c>, <c>Y</c> and <c>Z</c> are
	///     the string representations of the <see cref="X" />, <see cref="Y" /> and <see cref="Z" /> values.
	/// </remarks>
	public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out Point3D<T> result)
	{
		result = default;
		Span<T> values = stackalloc T[_parsingValueCount];
		var parsed = TupleParsing.TryParseValueListIntoSpan(
			s,
			provider,
			in values,
			new TupleParsing.TupleParsingOptions(_parsingValueCount, _parsingSeparatorChar, _parsingOpeningBracketChar,
				_parsingClosingBracketChar)
		);
		if (parsed) result = new Point3D<T>(values[0], values[1], values[2]);
		return parsed;
	}
}