using System;
using System.Drawing;
using OverlayEngine;

namespace BiterZergRushForms
{
    /// <summary>
    /// A game entity, but with health and a healthbar.
    /// </summary>
    public abstract class FactorioEntity : OverlayEntity
    {
        const float HealthbarScale = 2;

        float health;
        int maxHealth;
        SizeF healthbarDimensions;

        public float Health
        {
            get => health;
            set => health = value;
        }

        public int MaxHealth
        {
            get => maxHealth;
            set
            {
                maxHealth = value;
                healthbarDimensions = HealthbarSprite.CalculateHealthbarDimensions(value);
            }
        }

        public virtual Image GameSprite { get; protected set; }

        /// <summary>
        /// Returns the width of <c>GameSprite</c> or width of the healthbar, whatevers the greatest.
        /// </summary>
        public sealed override int Width => Math.Max(GameSprite?.Width ?? 0, (int)(healthbarDimensions.Width * HealthbarScale));

        /// <summary>
        /// Returns the height of <c>GameSprite</c> summed the height of the healthbar.
        /// </summary>
        public sealed override int Height => GameSprite?.Height ?? 0 + (int)(healthbarDimensions.Height * HealthbarScale);

        bool ShowingHealthbar => Health != MaxHealth && MaxHealth != 0;

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
                float scaledWidth = healthbarSprite.Width * HealthbarScale;
                float scaledHeight = healthbarSprite.Height * HealthbarScale;

                int healthbarLocX = 0;
                int healthbarLocY = (int)(Height - scaledHeight);
                graphics.DrawImage(healthbarSprite, healthbarLocX, healthbarLocY, scaledWidth, scaledHeight);
            }
        }
    }
}
