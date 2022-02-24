using System;
using System.Drawing;

namespace EngineProject
{
    public readonly struct GameVector : IEquatable<GameVector>
    {
        public readonly float X;
        public readonly float Y;

        public GameVector(float x, float y)
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
        public static float Distance(GameVector a, GameVector b)
        {
            return (b - a).Magnitude;
        }

        public static GameVector Lerp(GameVector a, GameVector b, float t)
        {
            return (a * (1f - t)) + (b * t);
        }

        /// <summary>
        /// The nearest point on the <paramref name="rectangle"/> to the <paramref name="position"/>
        /// </summary>
        /// <param name="position">A position</param>
        /// <param name="rectangle">The rectangle</param>
        /// <returns>The point on (or in) the <paramref name="rectangle"/> nearest to <paramref name="position"/></returns>
        public static GameVector NearestPointOnRectangle(GameVector position, RectangleF rectangle)
        {
            float minX, maxX;
            minX = rectangle.X;
            maxX = rectangle.X + rectangle.Width;

            float minY, maxY;
            minY = rectangle.Y;
            maxY = rectangle.Y + rectangle.Height;

            float targetX = Math.Min(Math.Max(minX, position.X), maxX);
            float targetY = Math.Min(Math.Max(minY, position.Y), maxY);

            return new GameVector(targetX, targetY);
        }

        public static implicit operator GameVector(PointF point) => new GameVector(point.X, point.Y);
        public static implicit operator PointF(GameVector point) => new PointF(point.X, point.Y);

        /// <summary>
        /// Addition of two <see cref="GameVector"/> structures
        /// </summary>
        /// <param name="a">The first <see cref="GameVector"/></param>
        /// <param name="b">The second <see cref="GameVector"/></param>
        /// <returns>A new <see cref="GameVector"/> with dimensions
        /// (<paramref name="a"/>.X + <paramref name="b"/>.X; <paramref name="a"/>.Y + <paramref name="b"/>.Y)</returns>
        public static GameVector operator +(GameVector a, GameVector b)
        {
            return new GameVector(a.X + b.X, a.Y + b.Y);
        }

        /// <summary>
        /// Subtraction of two <see cref="GameVector"/> structures
        /// </summary>
        /// <param name="a">The first <see cref="GameVector"/></param>
        /// <param name="b">The second <see cref="GameVector"/></param>
        /// <returns>A new <see cref="GameVector"/> with dimensions
        /// (<paramref name="a"/>.X - <paramref name="b"/>.X; <paramref name="a"/>.Y - <paramref name="b"/>.Y)</returns>
        public static GameVector operator -(GameVector a, GameVector b)
        {
            return new GameVector(a.X - b.X, a.Y - b.Y);
        }

        /// <summary>
        /// Extension of a <see cref="GameVector"/> structure by a specific factor
        /// </summary>
        /// <param name="vector">The <see cref="GameVector"/> structure to extend</param>
        /// <param name="factor">The factor to extend the <see cref="GameVector"/> by</param>
        /// <returns>A new <see cref="GameVector"/> structure with dimensions
        /// (<paramref name="vector"/>.X * <paramref name="factor"/>; <paramref name="vector"/>.Y * <paramref name="factor"/>)</returns>
        public static GameVector operator *(GameVector vector, float factor)
        {
            return new GameVector(vector.X * factor, vector.Y * factor);
        }

        /// <summary>
        /// Shortening of a <see cref="GameVector"/> structure by a specific factor
        /// </summary>
        /// <param name="vector">The <see cref="GameVector"/> structure to shorten</param>
        /// <param name="factor">The factor to shorten the <see cref="GameVector"/> by</param>
        /// <returns>A new <see cref="GameVector"/> structure with dimensions
        /// (<paramref name="vector"/>.X / <paramref name="factor"/>; <paramref name="vector"/>.Y / <paramref name="factor"/>)</returns>
        public static GameVector operator /(GameVector vector, float factor)
        {
            return new GameVector(vector.X / factor, vector.Y / factor);
        }

        /// <summary>
        /// The dot product (scalar product) of two <see cref="GameVector"/> structures
        /// </summary>
        /// <param name="a">The first <see cref="GameVector"/></param>
        /// <param name="b">The second <see cref="GameVector"/></param>
        /// <returns>The dot product of <paramref name="a"/> and <paramref name="b"/></returns>
        public static float operator *(GameVector a, GameVector b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        /// <summary>
        /// Compares two <see cref="GameVector"/>s. The result specifies whether the values
        /// of <see cref="GameVector.X"/> and <see cref="GameVector.Y"/> of the two <see cref="GameVector"/>s are equal.
        /// </summary>
        /// <param name="a">The <see cref="GameVector"/> struct to the left of <see langword="=="/></param>
        /// <param name="b">The <see cref="GameVector"/> struct to the right of <see langword="=="/></param>
        /// <returns><see langword="true"/> if the <see cref="GameVector.X"/> and <see cref="GameVector.Y"/> values of the
        /// <paramref name="a"/> and <paramref name="b"/> <see cref="GameVector"/> structures are equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator ==(GameVector a, GameVector b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Compares two <see cref="GameVector"/>s. The result specifies whether the values
        /// of <see cref="GameVector.X"/> and <see cref="GameVector.Y"/> of the two <see cref="GameVector"/>s are equal.
        /// </summary>
        /// <param name="a">The <see cref="GameVector"/> struct to the left of <see langword="!="/></param>
        /// <param name="b">The <see cref="GameVector"/> struct to the right of <see langword="!="/></param>
        /// <returns><see langword="true"/> if the <see cref="GameVector.X"/> and <see cref="GameVector.Y"/> values of the
        /// <paramref name="a"/> and <paramref name="b"/> <see cref="GameVector"/> structures are not equal; otherwise, <see langword="false"/>.</returns>
        public static bool operator !=(GameVector a, GameVector b)
        {
            return !(a == b);
        }

        /// <summary>
        /// Specifies whether this <see cref="GameVector"/> contains the same coordinates as <paramref name="other"/>
        /// </summary>
        /// <param name="other">The <see cref="GameVector"/> to compare to</param>
        /// <returns>This method returns <see langword="true"/> if this <see cref="GameVector"/>
        /// has the same coordinates as <paramref name="other"/></returns>
        public bool Equals(GameVector other)
        {
            return this.X == other.X && this.Y == other.Y;
        }

        /// <summary>
        /// Specifies whether this <see cref="GameVector"/> contains the same coordinates as the specified <see langword="object"/>
        /// </summary>
        /// <param name="obj">The <see langword="object"/> to test</param>
        /// <returns>This method returns <see langword="true"/> if <paramref name="obj"/> is a <see cref="GameVector"/>
        /// and has the same coordinates as this <see cref="GameVector"/>.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is GameVector))
                return false;

            return Equals((GameVector)obj);
        }

        /// <summary>
        /// Returns a hash code for this <see cref="GameVector"/> struct.
        /// </summary>
        /// <returns>An integer value that specifies a hash value for this <see cref="GameVector"/> struct.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Returns the <see langword="string"/> representation of this <see cref="GameVector"/> struct.
        /// </summary>
        /// <returns>The <see langword="string"/> representation of this <see cref="GameVector"/>.</returns>
        public override string ToString()
        {
            return $"({X}; {Y})";
        }
    }
}
