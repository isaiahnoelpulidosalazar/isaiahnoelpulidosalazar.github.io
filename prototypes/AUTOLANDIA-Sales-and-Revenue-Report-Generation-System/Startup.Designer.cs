namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class Startup
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
            this.ProgressBar = new MaterialSkin.Controls.MaterialProgressBar();
            this.Banner = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Banner)).BeginInit();
            this.SuspendLayout();
            // 
            // ProgressBar
            // 
            this.ProgressBar.Depth = 0;
            this.ProgressBar.Location = new System.Drawing.Point(0, 195);
            this.ProgressBar.Margin = new System.Windows.Forms.Padding(0);
            this.ProgressBar.MouseState = MaterialSkin.MouseState.HOVER;
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(500, 5);
            this.ProgressBar.TabIndex = 0;
            // 
            // Banner
            // 
            this.Banner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Banner.Location = new System.Drawing.Point(0, 0);
            this.Banner.Margin = new System.Windows.Forms.Padding(0);
            this.Banner.Name = "Banner";
            this.Banner.Size = new System.Drawing.Size(500, 200);
            this.Banner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Banner.TabIndex = 1;
            this.Banner.TabStop = false;
            this.Banner.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Banner_MouseDown);
            this.Banner.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Banner_MouseMove);
            this.Banner.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Banner_MouseUp);
            // 
            // Startup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 200);
            this.Controls.Add(this.ProgressBar);
            this.Controls.Add(this.Banner);
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.StatusAndActionBar_None;
            this.MaximizeBox = false;
            this.Name = "Startup";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.Sizable = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Startup";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Startup_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.Banner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialProgressBar ProgressBar;
        private System.Windows.Forms.PictureBox Banner;
    }
}