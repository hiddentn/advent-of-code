using System.Numerics;

namespace AdventOfCode.Common.Geometry._3D;

// Operator definitions for <see cref="Vector3D{T}"/>.
public readonly partial struct Vector3D<T> :
	IEqualityOperators<Vector3D<T>, Vector3D<T>, bool>,
	IAdditionOperators<Vector3D<T>, Vector3D<T>, Vector3D<T>>,
	IAdditionOperators<Vector3D<T>, Point3D<T>, Point3D<T>>,
	ISubtractionOperators<Vector3D<T>, Vector3D<T>, Vector3D<T>>,
	IDivisionOperators<Vector3D<T>, T, Vector3D<T>>,
	IMultiplyOperators<Vector3D<T>, T, Vector3D<T>>,
	IUnaryNegationOperators<Vector3D<T>, Vector3D<T>>,
	IUnaryPlusOperators<Vector3D<T>, Vector3D<T>>
	where T : unmanaged, INumber<T>
{
	/// <inheritdoc />
	public static bool operator ==(Vector3D<T> left, Vector3D<T> right)
	{
		return left.Equals(right);
	}

	/// <inheritdoc />
	public static bool operator !=(Vector3D<T> left, Vector3D<T> right)
	{
		return !(left == right);
	}

	/// <inheritdoc />
	public static Vector3D<T> operator +(Vector3D<T> left, Vector3D<T> right)
	{
		return new Vector3D<T>(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
	}

	/// <inheritdoc />
	public static Point3D<T> operator +(Vector3D<T> left, Point3D<T> right)
	{
		return new Point3D<T>(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
	}

	/// <inheritdoc />
	public static Vector3D<T> operator -(Vector3D<T> left, Vector3D<T> right)
	{
		return new Vector3D<T>(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
	}

	/// <inheritdoc />
	public static Vector3D<T> operator /(Vector3D<T> left, T right)
	{
		return new Vector3D<T>(left.X / right, left.Y / right, left.Z / right);
	}

	/// <inheritdoc />
	public static Vector3D<T> operator *(Vector3D<T> left, T right)
	{
		return new Vector3D<T>(left.X * right, left.Y * right, left.Z * right);
	}

	/// <inheritdoc />
	public static Vector3D<T> operator *(T left, Vector3D<T> right)
	{
		return right * left;
	}

	/// <inheritdoc />
	public static Vector3D<T> operator -(Vector3D<T> value)
	{
		return new Vector3D<T>(-value.X, -value.Y, -value.Z);
	}

	/// <inheritdoc />
	public static Vector3D<T> operator +(Vector3D<T> value)
	{
		return new Vector3D<T>(+value.X, +value.Y, +value.Z);
	}
}
