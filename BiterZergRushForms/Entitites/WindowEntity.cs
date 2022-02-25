using System;
using System.Drawing;
using OverlayEngine;

namespace BiterZergRushForms.Entities
{
    public class WindowEntity : FactorioEntity
    {
        public override Vector Location => new Vector(WindowRect.X + WindowRect.Width * 0.5f, WindowRect.Y + WindowRect.Height * 0.5f);
        public Rectangle WindowRect { get; private set; }

        readonly IntPtr windowHandle;

        public WindowEntity(IntPtr windowHandle)
        {
            this.windowHandle = windowHandle;
            MaxHealth = 15;
            Health = 15;
            GameSprite = null;
        }

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
