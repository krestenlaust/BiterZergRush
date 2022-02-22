using System;
using System.Drawing;

namespace BiterZergRushForms
{
    /// <summary>
    /// Struct based on PointF, converts implicitly to Point, allows for arithmetic.
    /// </summary>
    public readonly struct GameVector
    {
        public readonly float X;
        public readonly float Y;

        public GameVector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public GameVector(Point point)
        {
            X = point.X;
            Y = point.Y;
        }

        public float Length => (float)Math.Sqrt(X * X + Y * Y);

        public static GameVector Lerp(GameVector a, GameVector b, float t)
        {
            return (a * (1f - t)) + (b * t);
        }

        public static float Distance(GameVector a, GameVector b)
        {
            GameVector vectorBetween = a - b;
            return vectorBetween.Length;
        }

        public GameVector NearestPointOnRectangle(RectangleF rect)
        {
            float minX, maxX;
            minX = rect.X;
            maxX = rect.X + rect.Width;

            float minY, maxY;
            minY = rect.Y;
            maxY = rect.Y + rect.Height;

            float targetX = Math.Min(Math.Max(minX, X), maxX);
            float targetY = Math.Min(Math.Max(minY, Y), maxY);

            return new GameVector(targetX, targetY);
        }

        public static implicit operator Point(GameVector p) => new Point((int)Math.Round(p.X), (int)Math.Round(p.Y));

        public static GameVector operator +(GameVector a, GameVector b)
        {
            return new GameVector(a.X + b.X, a.Y + b.Y);
        }

        public static GameVector operator *(GameVector a, float b)
        {
            return new GameVector(a.X * b, a.Y * b);
        }

        public static GameVector operator -(GameVector a, GameVector b)
        {
            return new GameVector(a.X - b.X, a.Y - b.Y);
        }
    }
}
