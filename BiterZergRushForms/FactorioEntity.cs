using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OverlayEngine;

namespace BiterZergRushForms
{
    /// <summary>
    /// A game entity, but with health and a healthbar.
    /// </summary>
    public abstract class FactorioEntity : OverlayEntity
    {
        const float healthbarScale = 2;

        public float Health;
        public int MaxHealth;

        private bool ShowingHealthbar => Health != MaxHealth && MaxHealth != 0;

        public virtual Image GameSprite { get; protected set; }

        /// <summary>
        /// Returns the width of <c>GameSprite</c> or width of the healthbar, whatevers the greatest.
        /// </summary>
        public sealed override int Width => Math.Max(GameSprite?.Width ?? 0, (int)(HealthbarSprite.CalculateHealthbarDimensions(MaxHealth).Width * healthbarScale));

        /// <summary>
        /// Returns the height of <c>GameSprite</c> summed the height of the healthbar.
        /// </summary>
        public sealed override int Height => GameSprite?.Height ?? 0 + (int)(HealthbarSprite.CalculateHealthbarDimensions(MaxHealth).Height * healthbarScale);

        public sealed override void OnRender(Graphics graphics)
        {
            if (!(GameSprite is null))
            {
                graphics.DrawImage(GameSprite, 0, 0, GameSprite.Width, GameSprite.Height);
            }

            // Render healthbar
            if (!ShowingHealthbar)
            {
                return;
            }

            // TODO: I just realised that this new abstraction of the FactorioEntity will result in weird behavior
            // when using the scale property of the base-OverlayEntity since the Healthbar will be scaled as well.
            // maybe demote scale property to SpriteEntity?
            using (Bitmap healthbarSprite = HealthbarSprite.GenerateHealthbar(Math.Max((int)Math.Round(Health), 0), MaxHealth))
            {
                float scaledWidth = healthbarSprite.Width * healthbarScale;
                float scaledHeight = healthbarSprite.Height * healthbarScale;

                int healthbarLocX = 0;
                int healthbarLocY = (int)(Height - scaledHeight);
                graphics.DrawImage(healthbarSprite, healthbarLocX, healthbarLocY, scaledWidth, scaledHeight);
            }
        }
    }
}
