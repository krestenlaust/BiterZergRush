using System;
using System.Drawing;

namespace BiterZergRushForms
{
    public class HealthbarSprite
    {
        readonly static Bitmap cellGrey, cellGreen;

        static HealthbarSprite()
        {
            cellGrey = Properties.Resources.health_bar_grey_cell;
            cellGreen = Properties.Resources.health_bar_green_cell;
        }

        public static Bitmap GenerateHealthbar(int green, int total)
        {
            if (green > total)
            {
                throw new ArgumentOutOfRangeException($"{nameof(green)} has to be lower than or equal to {nameof(total)}");
            }

            int cellLength = cellGrey.Width;
            Bitmap healthbar = new Bitmap(cellLength * total, cellLength);

            using (Graphics g = Graphics.FromImage(healthbar))
            {
                for (int i = 0; i < total; i++)
                {
                    Rectangle targetRect = new Rectangle(i * cellLength, 0, cellLength, cellLength);
                    Bitmap sourceImage;

                    sourceImage = i < green ? cellGreen : cellGrey;

                    g.DrawImage(
                        healthbar,
                        targetRect,
                        new Rectangle(0, 0, sourceImage.Width, sourceImage.Height),
                        GraphicsUnit.Pixel);
                }
            }

            return healthbar;
        }
    }
}
