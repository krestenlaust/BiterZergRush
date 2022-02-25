using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverlayEngine
{
    public abstract class SpriteEntity : OverlayEntity
    {
        /// <summary>
        /// Can be null, to not display anything.
        /// </summary>
        public virtual Image Sprite { get; protected set; }

        public override int Width => Sprite.Width;
        public override int Height => Sprite.Width;

        public sealed override void OnRender(Graphics graphics)
        {
            if (Sprite is null)
            {
                return;
            }

            graphics.DrawImage(Sprite, 0, 0, Sprite.Width, Sprite.Height);
        }
    }
}
