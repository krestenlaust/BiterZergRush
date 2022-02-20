using System;
using System.Drawing;

namespace BiterZergRushForms
{
    /// <summary>
    /// Class based on PointF, converts implicitly to Point, allows for arithmetic.
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

        public static GameVector Lerp(GameVector a, GameVector b, float t)
        {
            return (a * (1f - t)) + (b * t);
        }

        public static float Distance(GameVector a, GameVector b)
        {
            GameVector vectorBetween = a - b;
            return (float)Math.Sqrt(vectorBetween.X * vectorBetween.X + vectorBetween.Y * vectorBetween.Y);
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
