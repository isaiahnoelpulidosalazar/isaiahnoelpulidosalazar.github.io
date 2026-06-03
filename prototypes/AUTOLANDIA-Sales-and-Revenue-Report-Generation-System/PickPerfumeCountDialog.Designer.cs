namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class PickPerfumeCountDialog
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
            this.TB_PC = new MaterialSkin.Controls.MaterialTextBox();
            this.DoneButton = new MaterialSkin.Controls.MaterialButton();
            this.SuspendLayout();
            // 
            // TB_PC
            // 
            this.TB_PC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_PC.AnimateReadOnly = false;
            this.TB_PC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_PC.Depth = 0;
            this.TB_PC.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TB_PC.Hint = "Perfume amount";
            this.TB_PC.LeadingIcon = null;
            this.TB_PC.Location = new System.Drawing.Point(11, 32);
            this.TB_PC.Margin = new System.Windows.Forms.Padding(8);
            this.TB_PC.MaxLength = 50;
            this.TB_PC.MouseState = MaterialSkin.MouseState.OUT;
            this.TB_PC.Multiline = false;
            this.TB_PC.Name = "TB_PC";
            this.TB_PC.Size = new System.Drawing.Size(354, 50);
            this.TB_PC.TabIndex = 84;
            this.TB_PC.Text = "1";
            this.TB_PC.TrailingIcon = null;
            // 
            // DoneButton
            // 
            this.DoneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DoneButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DoneButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.DoneButton.Depth = 0;
            this.DoneButton.HighEmphasis = true;
            this.DoneButton.Icon = null;
            this.DoneButton.Location = new System.Drawing.Point(301, 94);
            this.DoneButton.Margin = new System.Windows.Forms.Padding(8);
            this.DoneButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.DoneButton.Size = new System.Drawing.Size(64, 36);
            this.DoneButton.TabIndex = 85;
            this.DoneButton.Text = "Done";
            this.DoneButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.DoneButton.UseAccentColor = false;
            this.DoneButton.UseVisualStyleBackColor = true;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // PickPerfumeCountDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 140);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.TB_PC);
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None;
            this.Name = "PickPerfumeCountDialog";
            this.Padding = new System.Windows.Forms.Padding(3, 24, 2, 2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PickPerfumeCountDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PickPerfumeCountDialog_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialTextBox TB_PC;
        private MaterialSkin.Controls.MaterialButton DoneButton;
    }
}