using System;
using System.Drawing;
using Aud.IO;
using OverlayEngine;

namespace BiterZergRushForms.Entities
{
    /// <summary>
    /// This entity represents a Windows window and tracks its position accordingly.
    /// It is used to provide the window with healthbar GUI and an in-game representation of position.
    /// </summary>
    public class WindowEntity : FactorioEntity
    {
        public override Vector Location => new Vector(WindowRect.X + WindowRect.Width * 0.5f, WindowRect.Y + WindowRect.Height * 0.5f);
        public Rectangle WindowRect { get; private set; }
        public bool IsMinimized { get; private set; }
        public IntPtr WindowHandle { get; }
        
        float lastHealth;
        float timeSinceDamaged;

        public WindowEntity(IntPtr windowHandle)
        {
            this.WindowHandle = windowHandle;
            MaxHealth = 5;
            Health = 5;
            lastHealth = Health;
            GameSprite = null;
        }

        static readonly AudioFile alertDestroyedAudio;

        static WindowEntity()
        {
            alertDestroyedAudio = Audio.LoadWaveFile(Properties.Resources.alert_destroyed);
        }

        public override void OnUpdate(float deltaSeconds)
        {
            timeSinceDamaged += deltaSeconds;

            NativeFunctions.GetWindowRect(WindowHandle, out NativeFunctions.RECT lpRect);
            WindowRect = lpRect;
            IsMinimized = NativeFunctions.IsIconic(WindowHandle);

            if (lastHealth > Health && timeSinceDamaged > 1.5f)
            {
                lastHealth = Health;
                timeSinceDamaged = 0;
                Audio.PlayAudio(alertDestroyedAudio);
            }

            if (Health <= 0)
            {
                Engine.Destroy(this);
            }
        }

        public override void OnDestroy()
        {
            NativeFunctions.ShowWindow(WindowHandle, 11);
            //Process.GetProcessById((int)WindowHandle).Kill();
        }
    }
}
