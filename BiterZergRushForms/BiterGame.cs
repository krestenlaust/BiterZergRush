﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BiterZergRushForms.Entities;
using OverlayEngine;


namespace BiterZergRushForms
{
    public class BiterGame : IGame
    {
        const int manualMoveMultiplier = 50;
        readonly Dictionary<IntPtr, WindowEntity> windows = new Dictionary<IntPtr, WindowEntity>();
        readonly Vector spawnPoint = new Vector(10, 10);
        bool rotating = false;
        BiterEntity controlledBiter;
        float timeSinceLastRefreshedActiveWindow;

        public void OnKeyDown(KeyEventArgs keyEvent)
        {
            if (controlledBiter is null)
            {
                return;
            }

            if (keyEvent.KeyCode == Keys.Enter)
            {
                rotating = true;
            }
        }

        public void OnKeyUp(KeyEventArgs keyEvent)
        {
            if (controlledBiter is null)
            {
                return;
            }

            if (keyEvent.KeyCode == Keys.Space)
            {
                controlledBiter.Location = spawnPoint;
            }
            else if (keyEvent.KeyCode == Keys.Enter)
            {
                rotating = false;
            }
            else if (keyEvent.KeyCode == Keys.Up)
            {
                controlledBiter.Location -= new Vector(0, 1 * manualMoveMultiplier);
            }
            else if (keyEvent.KeyCode == Keys.Down)
            {
                controlledBiter.Location += new Vector(0, 1 * manualMoveMultiplier);
            }
            else if (keyEvent.KeyCode == Keys.Left)
            {
                controlledBiter.Location -= new Vector(1 * manualMoveMultiplier, 0);
            }
            else if (keyEvent.KeyCode == Keys.Right)
            {
                controlledBiter.Location += new Vector(1 * manualMoveMultiplier, 0);
            }
        }

        public void OnLoad()
        {
            //controlledBiter = new BiterEntity(false) { Location = spawnPoint };
            //Engine.Instantiate(controlledBiter);
            
            Engine.Instantiate(new BiterEntity() { Location = spawnPoint + new Vector(10, 5) });
            Engine.Instantiate(new BiterEntity() { Location = spawnPoint + new Vector(1500, 15) });
        }

        public void OnUpdate(float deltaSeconds)
        {
            timeSinceLastRefreshedActiveWindow += deltaSeconds;

            if (timeSinceLastRefreshedActiveWindow >= 1)
            {
                IntPtr windowHandle = NativeFunctions.GetForegroundWindow();

                if (!windows.ContainsKey(windowHandle) && windowHandle != Engine.WindowHandle)
                {
                    WindowEntity windowEntity = new WindowEntity(windowHandle);
                    Engine.Instantiate(windowEntity);

                    windows[windowHandle] = windowEntity;
                }

                timeSinceLastRefreshedActiveWindow = 0;
            }

            if (rotating)
            {
                controlledBiter.Rotation += (float)(2 * Math.PI / 8) * deltaSeconds;
            }
        }
    }
}
