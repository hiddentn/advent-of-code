using System.Numerics;

namespace AdventOfCode.Common.Geometry._4D;

// Operator definitions for <see cref="Vector4D{T}"/>.
public readonly partial struct Vector4D<T> :
	IEqualityOperators<Vector4D<T>, Vector4D<T>, bool>,
	IAdditionOperators<Vector4D<T>, Vector4D<T>, Vector4D<T>>,
	IAdditionOperators<Vector4D<T>, Point4D<T>, Point4D<T>>,
	ISubtractionOperators<Vector4D<T>, Vector4D<T>, Vector4D<T>>,
	IDivisionOperators<Vector4D<T>, T, Vector4D<T>>,
	IMultiplyOperators<Vector4D<T>, T, Vector4D<T>>,
	IUnaryNegationOperators<Vector4D<T>, Vector4D<T>>,
	IUnaryPlusOperators<Vector4D<T>, Vector4D<T>>
	where T : unmanaged, INumber<T>
{
	/// <inheritdoc />
	public static bool operator ==(Vector4D<T> left, Vector4D<T> right)
	{
		return left.Equals(right);
	}

	/// <inheritdoc />
	public static bool operator !=(Vector4D<T> left, Vector4D<T> right)
	{
		return !(left == right);
	}

	/// <inheritdoc />
	public static Vector4D<T> operator +(Vector4D<T> left, Vector4D<T> right)
	{
		return new Vector4D<T>(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
	}

	/// <inheritdoc />
	public static Point4D<T> operator +(Vector4D<T> left, Point4D<T> right)
	{
		return new Point4D<T>(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
	}

	/// <inheritdoc />
	public static Vector4D<T> operator -(Vector4D<T> left, Vector4D<T> right)
	{
		return new Vector4D<T>(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
	}

	/// <inheritdoc />
	public static Vector4D<T> operator /(Vector4D<T> left, T right)
	{
		return new Vector4D<T>(left.X / right, left.Y / right, left.Z / right, left.W / right);
	}

	/// <inheritdoc />
	public static Vector4D<T> operator *(Vector4D<T> left, T right)
	{
		return new Vector4D<T>(left.X * right, left.Y * right, left.Z * right, left.W * right);
	}

	/// <inheritdoc />
	public static Vector4D<T> operator *(T left, Vector4D<T> right)
	{
		return right * left;
	}

	/// <inheritdoc />
	public static Vector4D<T> operator -(Vector4D<T> value)
	{
		return new Vector4D<T>(-value.X, -value.Y, -value.Z, -value.W);
	}

	/// <inheritdoc />
	public static Vector4D<T> operator +(Vector4D<T> value)
	{
		return new Vector4D<T>(+value.X, +value.Y, +value.Z, +value.W);
	}
}
