using System.Drawing;

namespace BiterZergRushForms
{
    public class SpritesheetImage
    {
        private readonly Bitmap[] sprites;

        public SpritesheetImage(Bitmap spritesheet, int width, int height)
        {
            sprites = new Bitmap[width * height];
            int spriteWidth = spritesheet.Width / width;
            int spriteHeight = spritesheet.Height / height;

            int i = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Rectangle spriteArea = new Rectangle(x * spriteWidth, y * spriteHeight, spriteWidth, spriteHeight);

                    Bitmap target = new Bitmap(spriteArea.Width, spriteArea.Height);

                    using (Graphics g = Graphics.FromImage(target))
                    {
                        g.DrawImage(
                            spritesheet,
                            new Rectangle(0, 0, target.Width, target.Height),
                            spriteArea,
                            GraphicsUnit.Pixel);
                    }
                    sprites[i++] = target;
                }
            }
        }

        public int Length => sprites.Length;

        public Bitmap this[int index]
        {
            get {
                return sprites[index];
            }
        }
    }
}
