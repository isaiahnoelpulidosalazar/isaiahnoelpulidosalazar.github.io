namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class SettingsDialog
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
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.BackupButton = new MaterialSkin.Controls.MaterialButton();
            this.RestoreButton = new MaterialSkin.Controls.MaterialButton();
            this.ChangePassword = new MaterialSkin.Controls.MaterialButton();
            this.SuspendLayout();
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.FontType = MaterialSkin.MaterialSkinManager.fontType.H5;
            this.materialLabel1.Location = new System.Drawing.Point(11, 40);
            this.materialLabel1.Margin = new System.Windows.Forms.Padding(8, 16, 8, 8);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(88, 29);
            this.materialLabel1.TabIndex = 79;
            this.materialLabel1.Text = "Settings";
            // 
            // materialDivider1
            // 
            this.materialDivider1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(11, 85);
            this.materialDivider1.Margin = new System.Windows.Forms.Padding(8);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(353, 2);
            this.materialDivider1.TabIndex = 80;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // BackupButton
            // 
            this.BackupButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BackupButton.AutoSize = false;
            this.BackupButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackupButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.BackupButton.Depth = 0;
            this.BackupButton.HighEmphasis = true;
            this.BackupButton.Icon = null;
            this.BackupButton.Location = new System.Drawing.Point(11, 95);
            this.BackupButton.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.BackupButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.BackupButton.Name = "BackupButton";
            this.BackupButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.BackupButton.Size = new System.Drawing.Size(353, 49);
            this.BackupButton.TabIndex = 81;
            this.BackupButton.Text = "Backup Data";
            this.BackupButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.BackupButton.UseAccentColor = false;
            this.BackupButton.UseVisualStyleBackColor = true;
            this.BackupButton.Click += new System.EventHandler(this.BackupButton_Click);
            // 
            // RestoreButton
            // 
            this.RestoreButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RestoreButton.AutoSize = false;
            this.RestoreButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.RestoreButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.RestoreButton.Depth = 0;
            this.RestoreButton.HighEmphasis = true;
            this.RestoreButton.Icon = null;
            this.RestoreButton.Location = new System.Drawing.Point(11, 152);
            this.RestoreButton.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.RestoreButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.RestoreButton.Name = "RestoreButton";
            this.RestoreButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.RestoreButton.Size = new System.Drawing.Size(353, 49);
            this.RestoreButton.TabIndex = 82;
            this.RestoreButton.Text = "Restore Data";
            this.RestoreButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.RestoreButton.UseAccentColor = false;
            this.RestoreButton.UseVisualStyleBackColor = true;
            this.RestoreButton.Click += new System.EventHandler(this.RestoreButton_Click);
            // 
            // ChangePassword
            // 
            this.ChangePassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ChangePassword.AutoSize = false;
            this.ChangePassword.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ChangePassword.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.ChangePassword.Depth = 0;
            this.ChangePassword.HighEmphasis = true;
            this.ChangePassword.Icon = null;
            this.ChangePassword.Location = new System.Drawing.Point(11, 209);
            this.ChangePassword.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.ChangePassword.MouseState = MaterialSkin.MouseState.HOVER;
            this.ChangePassword.Name = "ChangePassword";
            this.ChangePassword.NoAccentTextColor = System.Drawing.Color.Empty;
            this.ChangePassword.Size = new System.Drawing.Size(353, 49);
            this.ChangePassword.TabIndex = 83;
            this.ChangePassword.Text = "Change password";
            this.ChangePassword.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.ChangePassword.UseAccentColor = false;
            this.ChangePassword.UseVisualStyleBackColor = true;
            this.ChangePassword.Click += new System.EventHandler(this.ChangePassword_Click);
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(374, 268);
            this.Controls.Add(this.ChangePassword);
            this.Controls.Add(this.RestoreButton);
            this.Controls.Add(this.BackupButton);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.materialDivider1);
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None;
            this.Name = "SettingsDialog";
            this.Padding = new System.Windows.Forms.Padding(3, 24, 2, 2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SettingsDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private MaterialSkin.Controls.MaterialButton BackupButton;
        private MaterialSkin.Controls.MaterialButton RestoreButton;
        private MaterialSkin.Controls.MaterialButton ChangePassword;
    }
}