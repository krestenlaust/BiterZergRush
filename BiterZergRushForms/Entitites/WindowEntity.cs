using System;
using System.Drawing;
using EngineProject;

namespace BiterZergRushForms.Entities
{
    public class WindowEntity : GameEntity
    {
        public override int Width => WindowRect.Width;
        public override int Height => WindowRect.Height;

        public override GameVector Location => new GameVector(WindowRect.X + WindowRect.Width * 0.5f, WindowRect.Y + WindowRect.Height * 0.5f);
        public Rectangle WindowRect { get; private set; }

        IntPtr windowHandle;

        public WindowEntity(IntPtr windowHandle)
        {
            this.windowHandle = windowHandle;
            MaxHealth = 15;
            Health = 15;
        }

        public override Image Sprite => null;

        public override void OnUpdate(float deltaSeconds)
        {
            NativeFunctions.GetWindowRect(windowHandle, out NativeFunctions.RECT lpRect);
            WindowRect = lpRect;

            if (Health <= 0)
            {
                Engine.Destroy(this);
            }
        }

        public override void OnDestroy()
        {
            //Process.GetProcessById((int)windowHandle).Kill();
        }
    }
}
