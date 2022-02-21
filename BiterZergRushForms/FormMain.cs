using System;
using System.Windows.Forms;

namespace BiterZergRushForms
{
    public partial class FormMain : Form
    {
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

            Engine.SetupGame(new BiterGame());

            timerGameLoop.Interval = Engine.FrameInterval;
            timerGameLoop.Start();
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            Engine.KeyUp(e);
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            Engine.KeyDown(e);
        }

        private void pictureBoxGameDraw_Paint(object sender, PaintEventArgs e)
        {
            Engine.Render(e.Graphics);
        }

        private void timerGameLoop_Tick(object sender, EventArgs e)
        {
            // time calculation
            Engine.UpdateGame();

            // rendering
            pictureBoxGameDraw.Invalidate();
        }
    }
}
