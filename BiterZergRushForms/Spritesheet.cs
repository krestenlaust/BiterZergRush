using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;

namespace BiterZergRushForms
{
    public class Spritesheet : IEnumerable<Bitmap>
    {
        private readonly Bitmap[] sprites;

        public Spritesheet(Bitmap spritesheetImage, int spriteWidth, int spriteHeight, int spriteCount)
        {
            sprites = new Bitmap[spriteCount];

            int columnCount = spritesheetImage.Width / spriteWidth;
            for (int i = 0; i < spriteCount; i++)
            {
                int row = Math.DivRem(i, columnCount, out int column);
                Rectangle spriteArea = new Rectangle(column * spriteWidth, row * spriteHeight, spriteWidth, spriteHeight);

                Bitmap target = new Bitmap(spriteWidth, spriteHeight);

                using (Graphics g = Graphics.FromImage(target))
                {
                    g.DrawImage(
                        spritesheetImage,
                        new Rectangle(0, 0, spriteWidth, spriteHeight),
                        spriteArea,
                        GraphicsUnit.Pixel);
                }

                sprites[i] = target;
            }
        }

        public static Spritesheet MakeSpritesheetFromSpriteDimensions(Bitmap spritesheetImage, int spriteWidth, int spriteHeight)
        {
            int columnCount = spriteWidth / spritesheetImage.Width;
            int rowCount = spriteHeight / spritesheetImage.Height;

            return new Spritesheet(spritesheetImage, spriteWidth, spriteHeight, rowCount * columnCount);
        }

        public static Spritesheet MakeSpritesheetFromSpriteCount(Bitmap spritesheetImage, int columnCount, int rowCount)
        {
            int spriteWidth = spritesheetImage.Width / columnCount;
            int spriteHeight = spritesheetImage.Height / rowCount;

            return new Spritesheet(spritesheetImage, spriteWidth, spriteHeight, rowCount * columnCount);
        }

        public int Length => sprites.Length;

        public Bitmap this[int index]
        {
            get
            {
                return sprites[index];
            }
        }

        public IEnumerator<Bitmap> GetEnumerator()
        {
            foreach (Bitmap sprite in sprites)
            {
                yield return sprite;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
