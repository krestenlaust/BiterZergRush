using System;
using System.Drawing;
using System.Windows.Forms;

namespace BiterZergRushForms
{
    public partial class FormMain : Form
    {
        // Program
        BiterRunSpritesheet biterRun;
        const int manualMoveMultiplier = 50;
        const int frameInterval = 1000 / 60;
        DateTime previousTime;

        // Biter general
        const float animationInterval = 250f / 1000f;
        const float movementPerFrame = 10;
        readonly GamePoint spawnPoint = new GamePoint(10, 10);

        // Biter specifics
        int biterAnimationIndex = 0;
        int biterRotation = 4;
        GamePoint biterLocation;
        GamePoint animationStartLocation;
        GamePoint animationEndLocation;
        float timeSinceLastAnimation;

        private void pictureBoxBiter1_Click(object sender, EventArgs e)
        {
        }

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.Bounds = Screen.PrimaryScreen.Bounds;

            biterRun = new BiterRunSpritesheet(Properties.Resources.biter_run_01,
                Properties.Resources.biter_run_02,
                Properties.Resources.biter_run_03,
                Properties.Resources.biter_run_04);

            biterLocation = spawnPoint;

            timerGameLoop.Interval = frameInterval;
            timerGameLoop.Start();
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                animationEndLocation = spawnPoint;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                biterRotation = (biterRotation + 1) % 16;
            }
            else if (e.KeyCode == Keys.Up)
            {
                animationEndLocation -= new GamePoint(0, 1 * manualMoveMultiplier);
            }
            else if (e.KeyCode == Keys.Down)
            {
                animationEndLocation += new GamePoint(0, 1 * manualMoveMultiplier);
            }
            else if (e.KeyCode == Keys.Left)
            {
                animationEndLocation -= new GamePoint(1 * manualMoveMultiplier, 0);
            }
            else if (e.KeyCode == Keys.Right)
            {
                animationEndLocation += new GamePoint(1 * manualMoveMultiplier, 0);
            }
            else
            {
                return;
            }

            pictureBoxGameDraw.Invalidate();
        }

        private void pictureBoxGameDraw_Paint(object sender, PaintEventArgs e)
        {
            Bitmap biterImage = biterRun.GetBiter(biterAnimationIndex, biterRotation);

            e.Graphics.DrawImage(biterImage, biterLocation);
        }

        private void timerGameLoop_Tick(object sender, EventArgs e)
        {
            // time calculation
            DateTime currentTime = DateTime.Now;
            float deltaSeconds = (float)(currentTime - previousTime).TotalSeconds;
            previousTime = currentTime;

            Console.WriteLine(deltaSeconds);

            // code ...
            timeSinceLastAnimation += deltaSeconds;

            float lerpValue = timeSinceLastAnimation / animationInterval;
            lerpValue = Math.Min(1, lerpValue);

            // interpolate biter position.
            biterLocation = GamePoint.Lerp(animationStartLocation, animationEndLocation, lerpValue);

            // if time passed for whole frame, then swap frame.
            if (timeSinceLastAnimation > animationInterval)
            {
                // reset lerp values
                timeSinceLastAnimation = 0;
                biterLocation = animationEndLocation;
                animationStartLocation = biterLocation;

                // swap to next animation frame
                biterAnimationIndex = (biterAnimationIndex + 1) % 16;
                // set new end location
                animationEndLocation = biterLocation + new GamePoint(movementPerFrame, 0);
            }

            // rendering
            pictureBoxGameDraw.Invalidate();
        }
    }
}
