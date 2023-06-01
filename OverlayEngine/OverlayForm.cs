using System;
using System.Windows.Forms;

namespace OverlayEngine
{
    public class OverlayForm : Form
    {
        readonly Timer timerGameLoop;

        public OverlayForm()
        {
            Engine.WindowHandle = Handle;
            timerGameLoop = new Timer();
            timerGameLoop.Tick += TimerGameLoop_Tick;
            timerGameLoop.Interval = Engine.FrameInterval;

            SuspendLayout();
            // TODO: Extract this variable to Engine.
            BackColor = System.Drawing.SystemColors.ButtonShadow;
            TransparencyKey = System.Drawing.SystemColors.ButtonShadow;

            ClientSize = new System.Drawing.Size(1, 1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            //KeyPreview = true;
            Name = "OverlayForm";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.WindowsDefaultLocation;
            TopMost = true;
            Load += OverlayForm_Load;
            //KeyDown += ;
            //KeyUp += ;
            ResumeLayout(false);
        }

        void OverlayForm_Load(object sender, EventArgs e)
        {
            Bounds = Screen.PrimaryScreen.Bounds;
            WindowState = FormWindowState.Maximized;

            timerGameLoop.Start();
        }

        void TimerGameLoop_Tick(object sender, EventArgs e)
        {
            Engine.UpdateGame();

            // Redraw
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Engine.Render(e.Graphics);
        }
    }
}
