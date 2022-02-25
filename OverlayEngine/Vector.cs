using System;
using System.Drawing;

namespace OverlayEngine
{
    public readonly struct Vector : IEquatable<Vector>
    {
        public readonly float X;
        public readonly float Y;

        public Vector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float Magnitude => (float)Math.Sqrt(X * X + Y * Y);

        /// <summary>
        /// Distance between 2 points
        /// </summary>
        /// <param name="a">First point</param>
        /// <param name="b">Second point</param>
        /// <returns>The distance between the points</returns>
        public static float Distance(Vector a, Vector b)
        {
            return (b - a).Magnitude;
        }

        public static Vector Lerp(Vector a, Vector b, float t)
        {
            return (a * (1f - t)) + (b * t);
        }

        /// <summary>
        /// The nearest point on the <paramref name="rectangle"/> to the <paramref name="position"/>
        /// </summary>
        /// <param name="position">A position</param>
        /// <param name="rectangle">The rectangle</param>
        /// <returns>The point on (or in) the <paramref name="rectangle"/> nearest to <paramref name="position"/></returns>
        public static Vector NearestPointOnRectangle(Vector position, RectangleF rectangle)
        {
            float minX, maxX;
            minX = rectangle.X;
            maxX = rectangle.X + rectangle.Width;

            float minY, maxY;
            minY = rectangle.Y;
            maxY = rectangle.Y + rectangle.Height;

            float targetX = Math.Min(Math.Max(minX, position.X), maxX);
            float targetY = Math.Min(Math.Max(minY, position.Y), maxY);

            return new Vector(targetX, targetY);
        }

        public static implicit operator Vector(PointF point) => new Vector(point.X, point.Y);
        public static implicit operator PointF(Vector point) => new PointF(point.X, point.Y);
        public static implicit operator Point(Vector point) => new Point((int)point.X, (int)point.Y);

        /// <summary>
        /// Addition of two <see cref="Vector"/> structures
        /// </summary>
        /// <param name="a">The first <see cref="Vector"/></param>
        /// <param name="b">The second <see cref="Vector"/></param>
        /// <returns>A new <see cref="Vector"/> with dimensions
        /// (<paramref name="a"/>.X + <paramref name="b"/>.X; <paramref name="a"/>.Y + <paramref name="b"/>.Y)</returns>
        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y);
        }

        /// <summary>
        /// Subtraction of two <see cref="Vector"/> structures
        /// </summary>
        /// <param name="a">The first <see cref="Vector"/></param>
        /// <param name="b">The second <see cref="Vector"/></param>
        /// <returns>A new <see cref="Vector"/> with dimensions
        /// (<paramref name="a"/>.X - <paramref name="b"/>.X; <paramref name="a"/>.Y - <paramref name="b"/>.Y)</returns>
        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y);
        }

        /// <summary>
        /// Extension of a <see cref="Vector"/> structure by a specific factor
        /// </summary>
        /// <param name="vector">The <see cref="Vector"/> structure to extend</param>
        /// <param name="factor">The factor to extend the <see cref="Vector"/> by</param>
        /// <returns>A new <see cref="Vector"/> structure with dimensions
        /// (<paramref name="vector"/>.X * <paramref name="factor"/>; <paramref name="vector"/>.Y * <paramref name="factor"/>)</returns>
        public static Vector operator *(Vector vector, float factor)
        {
            return new Vector(vector.X * factor, vector.Y * factor);
        }

        /// <summary>
        /// Shortening of a <see cref="Vector"/> structure by a specific factor
        /// </summary>
        /// <param name="vector">The <see cref="Vector"/> structure to shorten</param>
        /// <param name="factor">The factor to shorten the <see cref="Vector"/> by</param>
        /// <returns>A new <see cref="Vector"/> structure with dimensions
        /// (<paramref name="vector"/>.X / <paramref name="factor"/>; <paramref name="vector"/>.Y / <paramref name="factor"/>)</returns>
        public static Vector operator /(Vector vector, float factor)
        {
            return new Vector(vector.X / factor, vector.Y / factor);
        }

        /// <summary>
        /// The dot product (scalar product) of two <see cref="Vector"/> structures
        /// </summary>
        /// <param name="a">The first <see cref="Vector"/></param>
        /// <param name="b">The second <see cref="Vector"/></param>
        /// <returns>The dot product of <paramref name="a"/> and <paramref name="b"/></returns>
        public static float operator *(Vector a, Vector b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        /// <summary>
        /// Compares two <see cref="Vector"/>s. The result specifies whether the values
        /// of <see cref="Vector.X"/> and <see cref="Vector.Y"/> of the two <see cref="Vector"/>s are equal.
        /// </summary>
        /// <param name="a">The <see cref="Vector"/> struct to the left of <see langword="=="/></param>
        /// <param name="b">The <see cref="Vector"/> struct to the right of <see langword="=="/></param>
        /// <returns><see langword="true"/> if the <see cref="Vector.X"/> and <see cref="Vector.Y"/> values of the
        /// <paramref name="a"/> and <paramref name="b"/> <see cref="Vector"/> structures are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(Vector a, Vector b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Compares two <see cref="Vector"/>s. The result specifies whether the values
        /// of <see cref="Vector.X"/> and <see cref="Vector.Y"/> of the two <see cref="Vector"/>s are equal.
        /// </summary>
        /// <param name="a">The <see cref="Vector"/> struct to the left of <see langword="!="/></param>
        /// <param name="b">The <see cref="Vector"/> struct to the right of <see langword="!="/></param>
        /// <returns><see langword="true"/> if the <see cref="Vector.X"/> and <see cref="Vector.Y"/> values of the
        /// <paramref name="a"/> and <paramref name="b"/> <see cref="Vector"/> structures are not equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(Vector a, Vector b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Specifies whether this <see cref="Vector"/> contains the same coordinates as <paramref name="other"/>
        /// </summary>
        /// <param name="other">The <see cref="Vector"/> to compare to</param>
        /// <returns>This method returns <see langword="true"/> if this <see cref="Vector"/>
        /// has the same coordinates as <paramref name="other"/></returns>
        public bool Equals(Vector other)
        {
            return this.X == other.X && this.Y == other.Y;
        }

        /// <summary>
        /// Specifies whether this <see cref="Vector"/> contains the same coordinates as the specified <see langword="object"/>
        /// </summary>
        /// <param name="obj">The <see langword="object"/> to test</param>
        /// <returns>This method returns <see langword="true"/> if <paramref name="obj"/> is a <see cref="Vector"/>
        /// and has the same coordinates as this <see cref="Vector"/>.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Vector))
                return false;

            return Equals((Vector)obj);
        }

        /// <summary>
        /// Returns a hash code for this <see cref="Vector"/> struct.
        /// </summary>
        /// <returns>An integer value that specifies a hash value for this <see cref="Vector"/> struct.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Returns the <see langword="string"/> representation of this <see cref="Vector"/> struct.
        /// </summary>
        /// <returns>The <see langword="string"/> representation of this <see cref="Vector"/>.</returns>
        public override string ToString()
        {
            return $"({X}; {Y})";
        }
    }
}
