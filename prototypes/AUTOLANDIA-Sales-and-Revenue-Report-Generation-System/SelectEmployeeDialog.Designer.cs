namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class SelectEmployeeDialog
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
            this.EmployeeListCheckBox = new MaterialSkin.Controls.MaterialCheckedListBox();
            this.CancelButton = new MaterialSkin.Controls.MaterialButton();
            this.DoneButton = new MaterialSkin.Controls.MaterialButton();
            this.SearchBarEmployee = new MaterialSkin.Controls.MaterialTextBox();
            this.SuspendLayout();
            // 
            // EmployeeListCheckBox
            // 
            this.EmployeeListCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EmployeeListCheckBox.AutoScroll = true;
            this.EmployeeListCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.EmployeeListCheckBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EmployeeListCheckBox.Depth = 0;
            this.EmployeeListCheckBox.Location = new System.Drawing.Point(10, 80);
            this.EmployeeListCheckBox.Margin = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this.EmployeeListCheckBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.EmployeeListCheckBox.Name = "EmployeeListCheckBox";
            this.EmployeeListCheckBox.Size = new System.Drawing.Size(460, 266);
            this.EmployeeListCheckBox.Striped = false;
            this.EmployeeListCheckBox.StripeDarkColor = System.Drawing.Color.Empty;
            this.EmployeeListCheckBox.TabIndex = 21;
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
            this.CancelButton.TabIndex = 20;
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
            this.DoneButton.TabIndex = 19;
            this.DoneButton.Text = "Done";
            this.DoneButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.DoneButton.UseAccentColor = false;
            this.DoneButton.UseVisualStyleBackColor = true;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // SearchBarEmployee
            // 
            this.SearchBarEmployee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBarEmployee.AnimateReadOnly = false;
            this.SearchBarEmployee.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SearchBarEmployee.Depth = 0;
            this.SearchBarEmployee.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.SearchBarEmployee.Hint = "Search";
            this.SearchBarEmployee.LeadingIcon = null;
            this.SearchBarEmployee.Location = new System.Drawing.Point(10, 36);
            this.SearchBarEmployee.Margin = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.SearchBarEmployee.MaxLength = 50;
            this.SearchBarEmployee.MouseState = MaterialSkin.MouseState.OUT;
            this.SearchBarEmployee.Multiline = false;
            this.SearchBarEmployee.Name = "SearchBarEmployee";
            this.SearchBarEmployee.Size = new System.Drawing.Size(460, 36);
            this.SearchBarEmployee.TabIndex = 26;
            this.SearchBarEmployee.Text = "";
            this.SearchBarEmployee.TrailingIcon = null;
            this.SearchBarEmployee.UseTallSize = false;
            this.SearchBarEmployee.TextChanged += new System.EventHandler(this.SearchBarEmployee_TextChanged);
            // 
            // SelectEmployeeDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 400);
            this.Controls.Add(this.SearchBarEmployee);
            this.Controls.Add(this.EmployeeListCheckBox);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.DoneButton);
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None;
            this.Name = "SelectEmployeeDialog";
            this.Padding = new System.Windows.Forms.Padding(2, 20, 2, 2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectEmployeeDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SelectEmployeeDialog_FormClosing);
            this.Load += new System.EventHandler(this.SelectEmployeeDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialCheckedListBox EmployeeListCheckBox;
        private MaterialSkin.Controls.MaterialButton CancelButton;
        private MaterialSkin.Controls.MaterialButton DoneButton;
        private MaterialSkin.Controls.MaterialTextBox SearchBarEmployee;
    }
}