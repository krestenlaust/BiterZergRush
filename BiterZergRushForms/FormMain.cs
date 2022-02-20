using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BiterZergRushForms
{
    public partial class FormMain : Form
    {
        BiterRunSpritesheet biterRun;
        int biterAnimationIndex = 0;
        int biterDirectionIndex = 0;
        GamePoint biterLocation = new GamePoint(10, 10);
        int moveMultiplier = 10;

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
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                biterAnimationIndex = (biterAnimationIndex + 1) % 16;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                biterDirectionIndex = (biterDirectionIndex + 1) % 16;
            }
            else if (e.KeyCode == Keys.Up)
            {
                biterLocation -= new GamePoint(0, 1 * moveMultiplier);
            }
            else if (e.KeyCode == Keys.Down)
            {
                biterLocation += new GamePoint(0, 1 * moveMultiplier);
            }
            else if (e.KeyCode == Keys.Left)
            {
                biterLocation -= new GamePoint(1 * moveMultiplier, 0);
            }
            else if (e.KeyCode == Keys.Right)
            {
                biterLocation += new GamePoint(1 * moveMultiplier, 0);
            }
            else
            {
                return;
            }

            pictureBoxGameDraw.Invalidate();
        }

        readonly struct GamePoint
        {
            public readonly float X;
            public readonly float Y;

            public GamePoint(float x, float y)
            {
                X = x;
                Y = y;
            }

            public static implicit operator Point(GamePoint p) => new Point((int)Math.Round(p.X), (int)Math.Round(p.Y));

            public static GamePoint operator +(GamePoint a, GamePoint b)
            {
                return new GamePoint(a.X + b.X, a.Y + b.Y);
            }

            public static GamePoint operator -(GamePoint a, GamePoint b)
            {
                return new GamePoint(a.X - b.X, a.Y - b.Y);
            }
        }

        private void pictureBoxGameDraw_Paint(object sender, PaintEventArgs e)
        {
            Bitmap biterImage = biterRun.GetBiter(biterAnimationIndex, biterDirectionIndex);

            e.Graphics.DrawImage(biterImage, biterLocation);
        }
    }
}
