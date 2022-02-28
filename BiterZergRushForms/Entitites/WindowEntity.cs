﻿using System;
using System.Drawing;
using Aud.IO;
using Aud.IO.Formats;
using OverlayEngine;

namespace BiterZergRushForms.Entities
{
    public class WindowEntity : FactorioEntity
    {
        public override Vector Location => new Vector(WindowRect.X + WindowRect.Width * 0.5f, WindowRect.Y + WindowRect.Height * 0.5f);
        public Rectangle WindowRect { get; private set; }

        readonly IntPtr windowHandle;
        float lastHealth;
        float timeSinceDamaged;

        public WindowEntity(IntPtr windowHandle)
        {
            this.windowHandle = windowHandle;
            MaxHealth = 15;
            Health = 15;
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

            NativeFunctions.GetWindowRect(windowHandle, out NativeFunctions.RECT lpRect);
            WindowRect = lpRect;

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
            //Process.GetProcessById((int)windowHandle).Kill();
        }
    }
}
