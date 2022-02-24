
namespace BiterZergRushForms
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBoxGameDraw = new System.Windows.Forms.PictureBox();
            this.timerGameLoop = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameDraw)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxGameDraw
            // 
            this.pictureBoxGameDraw.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.pictureBoxGameDraw.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBoxGameDraw.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxGameDraw.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxGameDraw.Name = "pictureBoxGameDraw";
            this.pictureBoxGameDraw.Size = new System.Drawing.Size(800, 450);
            this.pictureBoxGameDraw.TabIndex = 0;
            this.pictureBoxGameDraw.TabStop = false;
            this.pictureBoxGameDraw.Click += new System.EventHandler(this.pictureBoxBiter1_Click);
            this.pictureBoxGameDraw.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxGameDraw_Paint);
            // 
            // timerGameLoop
            // 
            this.timerGameLoop.Tick += new System.EventHandler(this.timerGameLoop_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBoxGameDraw);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FormMain";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.SystemColors.ButtonShadow;
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGameDraw)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxGameDraw;
        private System.Windows.Forms.Timer timerGameLoop;
    }
}

