using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace BiterZergRushForms
{
    public partial class FormMain : Form
    {
        // Program
        const int manualMoveMultiplier = 50;
        const int frameInterval = 1000 / 60;
        DateTime previousTime;

        // Engine general
        readonly List<GameEntity> entitites = new List<GameEntity>();
        float timeSinceLastRefreshedActiveWindow;
        IntPtr targetWindowHandle;
        Rectangle targetWindowRectangle;

        // Biter general
        BiterEntity controlledBiter;
        readonly GameVector spawnPoint = new GameVector(10, 10);

        private void pictureBoxBiter1_Click(object sender, EventArgs e)
        {

        }

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Bounds = Screen.PrimaryScreen.Bounds;
            WindowState = FormWindowState.Maximized;

            BiterEntity.BiterRunSpritesheet.Setup(
                Properties.Resources.biter_run_01,
                Properties.Resources.biter_run_02,
                Properties.Resources.biter_run_03,
                Properties.Resources.biter_run_04);

            controlledBiter = new BiterEntity() { Location = spawnPoint };
            entitites.Add(controlledBiter);

            timerGameLoop.Interval = frameInterval;
            timerGameLoop.Start();
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                controlledBiter.Location = spawnPoint;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                rotating = false;
            }
            else if (e.KeyCode == Keys.Up)
            {
                controlledBiter.Location -= new GameVector(0, 1 * manualMoveMultiplier);
            }
            else if (e.KeyCode == Keys.Down)
            {
                controlledBiter.Location += new GameVector(0, 1 * manualMoveMultiplier);
            }
            else if (e.KeyCode == Keys.Left)
            {
                controlledBiter.Location -= new GameVector(1 * manualMoveMultiplier, 0);
            }
            else if (e.KeyCode == Keys.Right)
            {
                controlledBiter.Location += new GameVector(1 * manualMoveMultiplier, 0);
            }
            else
            {
                return;
            }

            pictureBoxGameDraw.Invalidate();
        }

        private void pictureBoxGameDraw_Paint(object sender, PaintEventArgs e)
        {
            foreach (var item in entitites)
            {
                Image sprite = item.Sprite;

                int spriteWidth = (int)(sprite.Width * item.Scale);
                int spriteHeight = (int)(sprite.Height * item.Scale);

                float locX = item.Location.X - (spriteWidth * 0.5f);
                float locY = item.Location.Y - (spriteHeight * 0.5f);
                
                e.Graphics.DrawImage(sprite, locX, locY, spriteWidth, spriteHeight);
            }
        }

        private void timerGameLoop_Tick(object sender, EventArgs e)
        {
            // time calculation
            DateTime currentTime = DateTime.Now;
            float deltaSeconds = (float)(currentTime - previousTime).TotalSeconds;
            previousTime = currentTime;

            Console.WriteLine(deltaSeconds);
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

            foreach (var item in entitites)
            {
                item.OnUpdate(deltaSeconds);
            }

            // rendering
            pictureBoxGameDraw.Invalidate();
        }

        bool rotating = false;

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                rotating = true;
            }
        }
    }
}
