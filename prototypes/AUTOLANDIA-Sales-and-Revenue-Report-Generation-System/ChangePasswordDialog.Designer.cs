namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class ChangePasswordDialog
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
            this.DoneButton = new MaterialSkin.Controls.MaterialButton();
            this.TB_OldPassword = new MaterialSkin.Controls.MaterialTextBox();
            this.TB_NewPassword = new MaterialSkin.Controls.MaterialTextBox();
            this.SuspendLayout();
            // 
            // DoneButton
            // 
            this.DoneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DoneButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DoneButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.DoneButton.Depth = 0;
            this.DoneButton.HighEmphasis = true;
            this.DoneButton.Icon = null;
            this.DoneButton.Location = new System.Drawing.Point(301, 149);
            this.DoneButton.Margin = new System.Windows.Forms.Padding(8);
            this.DoneButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.DoneButton.Size = new System.Drawing.Size(64, 36);
            this.DoneButton.TabIndex = 87;
            this.DoneButton.Text = "Done";
            this.DoneButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.DoneButton.UseAccentColor = false;
            this.DoneButton.UseVisualStyleBackColor = true;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // TB_OldPassword
            // 
            this.TB_OldPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_OldPassword.AnimateReadOnly = false;
            this.TB_OldPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_OldPassword.Depth = 0;
            this.TB_OldPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.TB_OldPassword.Hint = "Current password";
            this.TB_OldPassword.LeadingIcon = null;
            this.TB_OldPassword.Location = new System.Drawing.Point(11, 32);
            this.TB_OldPassword.Margin = new System.Windows.Forms.Padding(8);
            this.TB_OldPassword.MaxLength = 50;
            this.TB_OldPassword.MouseState = MaterialSkin.MouseState.OUT;
            this.TB_OldPassword.Multiline = false;
            this.TB_OldPassword.Name = "TB_OldPassword";
            this.TB_OldPassword.Size = new System.Drawing.Size(354, 50);
            this.TB_OldPassword.TabIndex = 86;
            this.TB_OldPassword.Text = "";
            this.TB_OldPassword.TrailingIcon = null;
            // 
            // TB_NewPassword
            // 
            this.TB_NewPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_NewPassword.AnimateReadOnly = false;
            this.TB_NewPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_NewPassword.Depth = 0;
            this.TB_NewPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.TB_NewPassword.Hint = "New password";
            this.TB_NewPassword.LeadingIcon = null;
            this.TB_NewPassword.Location = new System.Drawing.Point(11, 90);
            this.TB_NewPassword.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.TB_NewPassword.MaxLength = 50;
            this.TB_NewPassword.MouseState = MaterialSkin.MouseState.OUT;
            this.TB_NewPassword.Multiline = false;
            this.TB_NewPassword.Name = "TB_NewPassword";
            this.TB_NewPassword.Size = new System.Drawing.Size(354, 50);
            this.TB_NewPassword.TabIndex = 88;
            this.TB_NewPassword.Text = "";
            this.TB_NewPassword.TrailingIcon = null;
            // 
            // ChangePasswordDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 195);
            this.Controls.Add(this.TB_NewPassword);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.TB_OldPassword);
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None;
            this.Name = "ChangePasswordDialog";
            this.Padding = new System.Windows.Forms.Padding(3, 24, 2, 2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChangePasswordDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialButton DoneButton;
        private MaterialSkin.Controls.MaterialTextBox TB_OldPassword;
        private MaterialSkin.Controls.MaterialTextBox TB_NewPassword;
    }
}