using System;
using System.Drawing;

namespace BiterZergRushForms
{
    public class HealthbarSprite
    {
        const float redFraction     = 1/3f;
        const float yellowFraction  = 2/3f;
        const float greenFraction   = 3/3f;

        readonly static Bitmap cellGrey, cellGreen, cellYellow, cellRed;
        readonly static int cellSize;

        static HealthbarSprite()
        {
            cellGrey = Properties.Resources.health_bar_grey_cell;
            cellRed = Properties.Resources.health_bar_red_cell;
            cellYellow = Properties.Resources.health_bar_yellow_cell;
            cellGreen = Properties.Resources.health_bar_green_cell;

            cellSize = cellGrey.Width;
        }

        public static Bitmap GenerateHealthbar(int health, int total)
        {
            if (health > total)
            {
                throw new ArgumentOutOfRangeException($"{nameof(health)} has to be lower than or equal to {nameof(total)}");
            }

            Bitmap healthbar = new Bitmap(cellSize * total, cellSize);

            Bitmap coloredCell;
            float healthFraction = health / (float)total;
            if (healthFraction >= yellowFraction)
            {
                coloredCell = cellGreen;
            }
            else if (healthFraction >= redFraction)
            {
                coloredCell = cellYellow;
            }
            else
            {
                coloredCell = cellRed;
            }

            using (Graphics g = Graphics.FromImage(healthbar))
            {
                for (int i = 0; i < total; i++)
                {
                    Rectangle targetRect = new Rectangle(i * cellSize, 0, cellSize, cellSize);
                    Bitmap sourceImage = i < health ? coloredCell : cellGrey;

                    g.DrawImage(
                        sourceImage,
                        targetRect,
                        new Rectangle(0, 0, sourceImage.Width, sourceImage.Height),
                        GraphicsUnit.Pixel);
                }
            }

            return healthbar;
        }
    }
}
