namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class SelectServicesDialog
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
            this.CancelButton = new MaterialSkin.Controls.MaterialButton();
            this.DoneButton = new MaterialSkin.Controls.MaterialButton();
            this.ServiceListCheckBox = new MaterialSkin.Controls.MaterialCheckedListBox();
            this.SearchBarService = new MaterialSkin.Controls.MaterialTextBox();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.CancelButton.Depth = 0;
            this.CancelButton.HighEmphasis = true;
            this.CancelButton.Icon = null;
            this.CancelButton.Location = new System.Drawing.Point(10, 354);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(8);
            this.CancelButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.CancelButton.Size = new System.Drawing.Size(77, 36);
            this.CancelButton.TabIndex = 17;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.CancelButton.UseAccentColor = false;
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // DoneButton
            // 
            this.DoneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DoneButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DoneButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.DoneButton.Depth = 0;
            this.DoneButton.HighEmphasis = true;
            this.DoneButton.Icon = null;
            this.DoneButton.Location = new System.Drawing.Point(406, 354);
            this.DoneButton.Margin = new System.Windows.Forms.Padding(8);
            this.DoneButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.DoneButton.Size = new System.Drawing.Size(64, 36);
            this.DoneButton.TabIndex = 16;
            this.DoneButton.Text = "Done";
            this.DoneButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.DoneButton.UseAccentColor = false;
            this.DoneButton.UseVisualStyleBackColor = true;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // ServiceListCheckBox
            // 
            this.ServiceListCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ServiceListCheckBox.AutoScroll = true;
            this.ServiceListCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.ServiceListCheckBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ServiceListCheckBox.Depth = 0;
            this.ServiceListCheckBox.Location = new System.Drawing.Point(10, 80);
            this.ServiceListCheckBox.Margin = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this.ServiceListCheckBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.ServiceListCheckBox.Name = "ServiceListCheckBox";
            this.ServiceListCheckBox.Size = new System.Drawing.Size(460, 266);
            this.ServiceListCheckBox.Striped = false;
            this.ServiceListCheckBox.StripeDarkColor = System.Drawing.Color.Empty;
            this.ServiceListCheckBox.TabIndex = 18;
            // 
            // SearchBarService
            // 
            this.SearchBarService.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBarService.AnimateReadOnly = false;
            this.SearchBarService.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SearchBarService.Depth = 0;
            this.SearchBarService.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.SearchBarService.Hint = "Search";
            this.SearchBarService.LeadingIcon = null;
            this.SearchBarService.Location = new System.Drawing.Point(10, 36);
            this.SearchBarService.Margin = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.SearchBarService.MaxLength = 50;
            this.SearchBarService.MouseState = MaterialSkin.MouseState.OUT;
            this.SearchBarService.Multiline = false;
            this.SearchBarService.Name = "SearchBarService";
            this.SearchBarService.Size = new System.Drawing.Size(460, 36);
            this.SearchBarService.TabIndex = 24;
            this.SearchBarService.Text = "";
            this.SearchBarService.TrailingIcon = null;
            this.SearchBarService.UseTallSize = false;
            this.SearchBarService.TextChanged += new System.EventHandler(this.SearchBarService_TextChanged);
            // 
            // SelectServicesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 400);
            this.Controls.Add(this.SearchBarService);
            this.Controls.Add(this.ServiceListCheckBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.DoneButton);
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None;
            this.Name = "SelectServicesDialog";
            this.Padding = new System.Windows.Forms.Padding(2, 20, 2, 2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectServicesDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectServicesDialog_FormClosing);
            this.Load += new System.EventHandler(this.SelectServicesDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialButton CancelButton;
        private MaterialSkin.Controls.MaterialButton DoneButton;
        private MaterialSkin.Controls.MaterialCheckedListBox ServiceListCheckBox;
        private MaterialSkin.Controls.MaterialTextBox SearchBarService;
    }
}