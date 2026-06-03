namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class LoginForm
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
            this.TB_Username = new MaterialSkin.Controls.MaterialTextBox();
            this.TB_Password = new MaterialSkin.Controls.MaterialTextBox();
            this.LoginButton = new MaterialSkin.Controls.MaterialButton();
            this.Banner = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Banner)).BeginInit();
            this.SuspendLayout();
            // 
            // TB_Username
            // 
            this.TB_Username.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Username.AnimateReadOnly = false;
            this.TB_Username.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_Username.Depth = 0;
            this.TB_Username.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TB_Username.Hint = "Username";
            this.TB_Username.LeadingIcon = null;
            this.TB_Username.Location = new System.Drawing.Point(8, 199);
            this.TB_Username.Margin = new System.Windows.Forms.Padding(8);
            this.TB_Username.MaxLength = 50;
            this.TB_Username.MouseState = MaterialSkin.MouseState.OUT;
            this.TB_Username.Multiline = false;
            this.TB_Username.Name = "TB_Username";
            this.TB_Username.Size = new System.Drawing.Size(359, 50);
            this.TB_Username.TabIndex = 84;
            this.TB_Username.Text = "";
            this.TB_Username.TrailingIcon = null;
            // 
            // TB_Password
            // 
            this.TB_Password.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Password.AnimateReadOnly = false;
            this.TB_Password.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_Password.Depth = 0;
            this.TB_Password.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TB_Password.Hint = "Password";
            this.TB_Password.LeadingIcon = null;
            this.TB_Password.Location = new System.Drawing.Point(8, 265);
            this.TB_Password.Margin = new System.Windows.Forms.Padding(8);
            this.TB_Password.MaxLength = 50;
            this.TB_Password.MouseState = MaterialSkin.MouseState.OUT;
            this.TB_Password.Multiline = false;
            this.TB_Password.Name = "TB_Password";
            this.TB_Password.Password = true;
            this.TB_Password.Size = new System.Drawing.Size(359, 50);
            this.TB_Password.TabIndex = 85;
            this.TB_Password.Text = "";
            this.TB_Password.TrailingIcon = null;
            // 
            // LoginButton
            // 
            this.LoginButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoginButton.AutoSize = false;
            this.LoginButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.LoginButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.LoginButton.Depth = 0;
            this.LoginButton.HighEmphasis = true;
            this.LoginButton.Icon = null;
            this.LoginButton.Location = new System.Drawing.Point(8, 374);
            this.LoginButton.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.LoginButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.LoginButton.Size = new System.Drawing.Size(359, 49);
            this.LoginButton.TabIndex = 86;
            this.LoginButton.Text = "Log In";
            this.LoginButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.LoginButton.UseAccentColor = false;
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // Banner
            // 
            this.Banner.Location = new System.Drawing.Point(0, 24);
            this.Banner.Margin = new System.Windows.Forms.Padding(0);
            this.Banner.Name = "Banner";
            this.Banner.Size = new System.Drawing.Size(375, 150);
            this.Banner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Banner.TabIndex = 87;
            this.Banner.TabStop = false;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 431);
            this.Controls.Add(this.Banner);
            this.Controls.Add(this.LoginButton);
            this.Controls.Add(this.TB_Password);
            this.Controls.Add(this.TB_Username);
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None;
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.Padding = new System.Windows.Forms.Padding(0, 24, 0, 0);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LoginForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.Banner)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private MaterialSkin.Controls.MaterialTextBox TB_Username;
        private MaterialSkin.Controls.MaterialTextBox TB_Password;
        private MaterialSkin.Controls.MaterialButton LoginButton;
        private System.Windows.Forms.PictureBox Banner;
    }
}