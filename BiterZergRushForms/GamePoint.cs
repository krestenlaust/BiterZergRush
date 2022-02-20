using System;
using System.Drawing;

namespace BiterZergRushForms
{
    public partial class FormMain
    {
        readonly struct GamePoint
        {
            public readonly float X;
            public readonly float Y;

            public GamePoint(float x, float y)
            {
                X = x;
                Y = y;
            }

            public static GamePoint Lerp(GamePoint a, GamePoint b, float t)
            {
                return (a * (1f - t)) + (b * t);
            }

            public static implicit operator Point(GamePoint p) => new Point((int)Math.Round(p.X), (int)Math.Round(p.Y));

            public static GamePoint operator +(GamePoint a, GamePoint b)
            {
                return new GamePoint(a.X + b.X, a.Y + b.Y);
            }

            public static GamePoint operator *(GamePoint a, float b)
            {
                return new GamePoint(a.X * b, a.Y * b);
            }

            public static GamePoint operator -(GamePoint a, GamePoint b)
            {
                return new GamePoint(a.X - b.X, a.Y - b.Y);
            }
        }
    }
}
