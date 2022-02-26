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

        /// <summary>
        /// Make a <see cref="Spritesheet"/> from a <see cref="Bitmap"/>, and the dimensions of the sprites
        /// </summary>
        /// <param name="spritesheetImage">The spritesheet image</param>
        /// <param name="spriteWidth">The width of the sprites on the spritesheet</param>
        /// <param name="spriteHeight">The height of the sprites on the spritesheet</param>
        /// <returns>A <see cref="Spritesheet"/> which contains all the individual sprites of the <see cref="Bitmap"/></returns>
        public static Spritesheet MakeSpritesheetFromSpriteDimensions(Bitmap spritesheetImage, int spriteWidth, int spriteHeight)
        {
            int columnCount = spriteWidth / spritesheetImage.Width;
            int rowCount = spriteHeight / spritesheetImage.Height;

            return new Spritesheet(spritesheetImage, spriteWidth, spriteHeight, rowCount * columnCount);
        }

        /// <summary>
        /// Make a <see cref="Spritesheet"/> from a <see cref="Bitmap"/>, and the dimensions of the sprites
        /// </summary>
        /// <param name="spritesheetImage">The spritesheet image</param>
        /// <param name="columnCount">How many columns of sprites are in the spritesheet (how many are in 1 row)</param>
        /// <param name="rowCount">How many rows of sprites are in the spritesheet (how many are in 1 column)</param>
        /// <returns>A <see cref="Spritesheet"/> which contains all the individual sprites of the <see cref="Bitmap"/></returns>
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

        /// <summary>
        /// Returns an enumerator that iterates through the sprites
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object which iterates through the sprites in the spritesheet</returns>
        public IEnumerator<Bitmap> GetEnumerator()
        {
            foreach (Bitmap sprite in sprites)
            {
                yield return sprite;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the sprites
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> object which iterates through the sprites in the spritesheet</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
