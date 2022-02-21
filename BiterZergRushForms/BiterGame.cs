using System;
using System.Drawing;
using System.Windows.Forms;
using BiterZergRushForms.Entities;

namespace BiterZergRushForms
{
    public class BiterGame : Game
    {
        const int manualMoveMultiplier = 50;
        readonly GameVector spawnPoint = new GameVector(10, 10);
        bool rotating = false;
        BiterEntity controlledBiter;
        float timeSinceLastRefreshedActiveWindow;
        IntPtr targetWindowHandle;
        Rectangle targetWindowRectangle;

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
            BiterEntity.BiterRunSpritesheet.Setup(
                Properties.Resources.biter_run_01,
                Properties.Resources.biter_run_02,
                Properties.Resources.biter_run_03,
                Properties.Resources.biter_run_04);

            controlledBiter = new BiterEntity() { Location = spawnPoint };
            Engine.Instantiate(controlledBiter);
        }

        public override void OnUpdate(float deltaSeconds)
        {
            timeSinceLastRefreshedActiveWindow += deltaSeconds;

            if (timeSinceLastRefreshedActiveWindow >= 3)
            {
                targetWindowHandle = NativeFunctions.GetForegroundWindow();

                timeSinceLastRefreshedActiveWindow = 0;
            }

            NativeFunctions.GetWindowRect(targetWindowHandle, out NativeFunctions.RECT lpRect);
            targetWindowRectangle = lpRect;
            controlledBiter.MoveTo(new GameVector(targetWindowRectangle.Location) + new GameVector(targetWindowRectangle.Width / 2, targetWindowRectangle.Height / 2));

            if (rotating)
            {
                controlledBiter.Rotation += (float)(2 * Math.PI / 8) * deltaSeconds;
            }
        }
    }
}
