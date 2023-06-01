using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OverlayEngine
{
    public static class Engine
    {
        public const int FrameInterval = 1000 / 60;
        public static IntPtr WindowHandle { get; internal set; }
        static readonly List<OverlayEntity> entitites = new List<OverlayEntity>();
        static readonly List<OverlayEntity> destroyedEntitites = new List<OverlayEntity>();
        static Game gameInstance;
        static DateTime previousTime;

        public static void StartGameLoop(IGame game)
        {
            gameInstance = game;
            game.OnLoad();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new OverlayForm());
        }

        public static IEnumerable<T> GetEntititesByType<T>() where T : OverlayEntity
        {
            foreach (var item in entitites)
            {
                if (item is T itemCast)
                {
                    yield return itemCast;
                }
            }
        }

        public static void KeyDown(KeyEventArgs keyEvent) => gameInstance.OnKeyDown(keyEvent);

        public static void KeyUp(KeyEventArgs keyEvent) => gameInstance.OnKeyUp(keyEvent);

        public static void Instantiate(OverlayEntity entity)
        {
            entitites.Add(entity);
        }

        public static void Destroy(OverlayEntity entity)
        {
            destroyedEntitites.Add(entity);
        }

        internal static void Render(Graphics graphics)
        {
            foreach (var item in entitites)
            {
                if (item.IsDestroyed)
                {
                    // don't render destroyed items.
                    continue;
                }

                if (!item.Visible)
                {
                    continue;
                }

                using (Image renderSprite = new Bitmap(item.Width, item.Height))
                {
                    // Call render method to draw to image
                    using (Graphics g = Graphics.FromImage(renderSprite))
                        item.OnRender(g);

                    int scaledWidth = (int)(renderSprite.Width * item.Scale);
                    int scaledHeight = (int)(renderSprite.Height * item.Scale);
                    Size scaledSize = new Size(scaledWidth, scaledHeight);

                    Vector location = item.Location - new Vector(scaledWidth, scaledHeight) * 0.5f;

                    graphics.DrawImage(renderSprite, new RectangleF(location, scaledSize));

                    Vector borderLocation = location - new Vector(1, 1);
                    Pen borderPen = new Pen(Brushes.Red, 1)
                    {
                        Alignment = System.Drawing.Drawing2D.PenAlignment.Center
                    };

                    graphics.DrawRectangle(borderPen, new Rectangle(borderLocation, scaledSize + new Size(1, 1)));
                }
            }
        }

        internal static void UpdateGame()
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
    }
}
