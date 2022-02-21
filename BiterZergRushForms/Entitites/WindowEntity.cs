using System;
using System.Diagnostics;
using System.Drawing;

namespace BiterZergRushForms.Entities
{
    public class WindowEntity : GameEntity
    {
        public override int Width => windowRect.Width;
        public override int Height => windowRect.Height;

        public override GameVector Location => new GameVector(windowRect.X + windowRect.Width * 0.5f, windowRect.Y + windowRect.Height * 0.5f);

        IntPtr windowHandle;
        Rectangle windowRect;

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
            windowRect = lpRect;

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
