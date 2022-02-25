using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BiterZergRushForms
{
    public static class Engine
    {
        public const int FrameInterval = 1000 / 60;
        private static Game gameInstance;
        static DateTime previousTime;
        static readonly List<GameEntity> entitites = new List<GameEntity>();
        static readonly List<GameEntity> destroyedEntitites = new List<GameEntity>();

        public static void SetupGame(Game game)
        {
            gameInstance = game;
            game.OnLoad();
        }

        public static void Render(Graphics graphics)
        {
            foreach (var item in entitites)
            {
                Image sprite = item.Sprite;

                int spriteWidth = 0;
                int spriteHeight = 0;

                float locX = item.Location.X;
                float locY = item.Location.Y;

                if (!(sprite is null))
                {
                    spriteWidth = (int)(sprite.Width * item.Scale);
                    spriteHeight = (int)(sprite.Height * item.Scale);

                    graphics.DrawImage(sprite, locX - spriteWidth * 0.5f, locY - spriteHeight * 0.5f, spriteWidth, spriteHeight);
                }

                if (item.Health == item.MaxHealth || item.MaxHealth == 0)
                {
                    continue;
                }

                Bitmap healthbarSprite = HealthbarSprite.GenerateHealthbar(Math.Max((int)Math.Round(item.Health), 0), item.MaxHealth);
                const float healthbarScale = 2;
                int healthbarLocX = (int)(locX - healthbarSprite.Width * 0.5f * healthbarScale);
                int healthbarLocY = (int)(locY + spriteHeight * 0.5f);
                graphics.DrawImage(healthbarSprite, healthbarLocX, healthbarLocY, healthbarSprite.Width * healthbarScale, healthbarSprite.Height * healthbarScale);
            }
        }

        public static IEnumerable<T> GetEntititesByType<T>() where T : GameEntity
        {
            foreach (var item in entitites)
            {
                if (item is T itemCast)
                {
                    yield return itemCast;
                }
            }
        }

        public static void UpdateGame()
        {
            DateTime currentTime = DateTime.Now;
            float deltaSeconds = (float)(currentTime - previousTime).TotalSeconds;
            previousTime = currentTime;

            // Console.WriteLine(deltaSeconds);

            gameInstance.OnUpdate(deltaSeconds);

            foreach (var item in entitites)
            {
                item.OnUpdate(deltaSeconds);
            }

            foreach (var item in destroyedEntitites)
            {
                item.OnDestroy();
                entitites.Remove(item);
            }

            destroyedEntitites.Clear();
        }

        public static void KeyDown(KeyEventArgs keyEvent) => gameInstance.OnKeyDown(keyEvent);

        public static void KeyUp(KeyEventArgs keyEvent) => gameInstance.OnKeyUp(keyEvent);

        public static void Instantiate(GameEntity entity)
        {
            entitites.Add(entity);
        }

        public static void Destroy(GameEntity entity)
        {
            destroyedEntitites.Add(entity);
        }
    }
}
