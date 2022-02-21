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

                if (sprite is null)
                {
                    continue;
                }

                int spriteWidth = (int)(sprite.Width * item.Scale);
                int spriteHeight = (int)(sprite.Height * item.Scale);

                float locX = item.Location.X - (spriteWidth * 0.5f);
                float locY = item.Location.Y - (spriteHeight * 0.5f);

                graphics.DrawImage(sprite, locX, locY, spriteWidth, spriteHeight);
            }
        }

        public static void UpdateGame()
        {
            DateTime currentTime = DateTime.Now;
            float deltaSeconds = (float)(currentTime - previousTime).TotalSeconds;
            previousTime = currentTime;

            Console.WriteLine(deltaSeconds);

            gameInstance.OnUpdate(deltaSeconds);

            foreach (var item in entitites)
            {
                item.OnUpdate(deltaSeconds);
            }
        }

        public static void KeyDown(KeyEventArgs keyEvent) => gameInstance.OnKeyDown(keyEvent);

        public static void KeyUp(KeyEventArgs keyEvent) => gameInstance.OnKeyUp(keyEvent);

        public static void Instantiate(GameEntity entity)
        {
            entitites.Add(entity);
        }
    }
}
