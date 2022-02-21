using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BiterZergRushForms.Entities;

namespace BiterZergRushForms
{
    public class BiterGame : Game
    {
        const int manualMoveMultiplier = 50;
        readonly Dictionary<IntPtr, WindowEntity> windows = new Dictionary<IntPtr, WindowEntity>();
        readonly GameVector spawnPoint = new GameVector(10, 10);
        bool rotating = false;
        BiterEntity controlledBiter;
        float timeSinceLastRefreshedActiveWindow;

        public override void OnKeyDown(KeyEventArgs keyEvent)
        {
            if (keyEvent.KeyCode == Keys.Enter)
            {
                rotating = true;
            }
        }

        public override void OnKeyUp(KeyEventArgs keyEvent)
        {
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
                controlledBiter.Location -= new GameVector(0, 1 * manualMoveMultiplier);
            }
            else if (keyEvent.KeyCode == Keys.Down)
            {
                controlledBiter.Location += new GameVector(0, 1 * manualMoveMultiplier);
            }
            else if (keyEvent.KeyCode == Keys.Left)
            {
                controlledBiter.Location -= new GameVector(1 * manualMoveMultiplier, 0);
            }
            else if (keyEvent.KeyCode == Keys.Right)
            {
                controlledBiter.Location += new GameVector(1 * manualMoveMultiplier, 0);
            }
            else
            {
                return;
            }
        }

        public override void OnLoad()
        {
            controlledBiter = new BiterEntity() { Location = spawnPoint };
            Engine.Instantiate(controlledBiter);
            Engine.Instantiate(new BiterEntity() { Location = spawnPoint + new GameVector(10, 5) });
            Engine.Instantiate(new BiterEntity() { Location = spawnPoint + new GameVector(15, 15) });
        }

        public override void OnUpdate(float deltaSeconds)
        {
            timeSinceLastRefreshedActiveWindow += deltaSeconds;

            if (timeSinceLastRefreshedActiveWindow >= 3)
            {
                IntPtr windowHandle = NativeFunctions.GetForegroundWindow();
                if (!windows.ContainsKey(windowHandle))
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
