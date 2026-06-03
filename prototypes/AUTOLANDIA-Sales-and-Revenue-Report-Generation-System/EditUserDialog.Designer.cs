namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class EditUserDialog
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
            this.CB_T = new MaterialSkin.Controls.MaterialComboBox();
            this.CancelButton = new MaterialSkin.Controls.MaterialButton();
            this.DoneButton = new MaterialSkin.Controls.MaterialButton();
            this.TB_U = new MaterialSkin.Controls.MaterialTextBox();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.TB_P = new MaterialSkin.Controls.MaterialTextBox();
            this.DeleteButton = new MaterialSkin.Controls.MaterialButton();
            this.SuspendLayout();
            // 
            // CB_T
            // 
            this.CB_T.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CB_T.AutoResize = false;
            this.CB_T.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CB_T.Depth = 0;
            this.CB_T.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.CB_T.DropDownHeight = 174;
            this.CB_T.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_T.DropDownWidth = 121;
            this.CB_T.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.CB_T.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CB_T.FormattingEnabled = true;
            this.CB_T.Hint = "Type";
            this.CB_T.IntegralHeight = false;
            this.CB_T.ItemHeight = 43;
            this.CB_T.Location = new System.Drawing.Point(10, 210);
            this.CB_T.MaxDropDownItems = 4;
            this.CB_T.MouseState = MaterialSkin.MouseState.OUT;
            this.CB_T.Name = "CB_T";
            this.CB_T.Size = new System.Drawing.Size(620, 49);
            this.CB_T.StartIndex = 0;
            this.CB_T.TabIndex = 28;
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.CancelButton.Depth = 0;
            this.CancelButton.HighEmphasis = true;
            this.CancelButton.Icon = null;
            this.CancelButton.Location = new System.Drawing.Point(10, 314);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(8);
            this.CancelButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.CancelButton.Size = new System.Drawing.Size(77, 36);
            this.CancelButton.TabIndex = 25;
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
            this.DoneButton.Location = new System.Drawing.Point(566, 314);
            this.DoneButton.Margin = new System.Windows.Forms.Padding(8);
            this.DoneButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.DoneButton.Size = new System.Drawing.Size(64, 36);
            this.DoneButton.TabIndex = 24;
            this.DoneButton.Text = "Done";
            this.DoneButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.DoneButton.UseAccentColor = false;
            this.DoneButton.UseVisualStyleBackColor = true;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // TB_U
            // 
            this.TB_U.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_U.AnimateReadOnly = false;
            this.TB_U.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_U.Depth = 0;
            this.TB_U.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TB_U.Hint = "Username";
            this.TB_U.LeadingIcon = null;
            this.TB_U.Location = new System.Drawing.Point(10, 91);
            this.TB_U.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.TB_U.MaxLength = 50;
            this.TB_U.MouseState = MaterialSkin.MouseState.OUT;
            this.TB_U.Multiline = false;
            this.TB_U.Name = "TB_U";
            this.TB_U.Size = new System.Drawing.Size(620, 50);
            this.TB_U.TabIndex = 26;
            this.TB_U.Text = "";
            this.TB_U.TrailingIcon = null;
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.FontType = MaterialSkin.MaterialSkinManager.fontType.H5;
            this.materialLabel1.Location = new System.Drawing.Point(10, 36);
            this.materialLabel1.Margin = new System.Windows.Forms.Padding(8, 16, 8, 8);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(95, 29);
            this.materialLabel1.TabIndex = 22;
            this.materialLabel1.Text = "Edit user";
            // 
            // materialDivider1
            // 
            this.materialDivider1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(10, 81);
            this.materialDivider1.Margin = new System.Windows.Forms.Padding(8);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(620, 2);
            this.materialDivider1.TabIndex = 23;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // TB_P
            // 
            this.TB_P.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_P.AnimateReadOnly = false;
            this.TB_P.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_P.Depth = 0;
            this.TB_P.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TB_P.Hint = "Password";
            this.TB_P.LeadingIcon = null;
            this.TB_P.Location = new System.Drawing.Point(10, 149);
            this.TB_P.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.TB_P.MaxLength = 50;
            this.TB_P.MouseState = MaterialSkin.MouseState.OUT;
            this.TB_P.Multiline = false;
            this.TB_P.Name = "TB_P";
            this.TB_P.Size = new System.Drawing.Size(620, 50);
            this.TB_P.TabIndex = 27;
            this.TB_P.Text = "";
            this.TB_P.TrailingIcon = null;
            // 
            // DeleteButton
            // 
            this.DeleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DeleteButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.DeleteButton.Depth = 0;
            this.DeleteButton.HighEmphasis = true;
            this.DeleteButton.Icon = null;
            this.DeleteButton.Location = new System.Drawing.Point(103, 314);
            this.DeleteButton.Margin = new System.Windows.Forms.Padding(8);
            this.DeleteButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.DeleteButton.Size = new System.Drawing.Size(73, 36);
            this.DeleteButton.TabIndex = 29;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.DeleteButton.UseAccentColor = false;
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // EditUserDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 360);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.CB_T);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.TB_U);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.materialDivider1);
            this.Controls.Add(this.TB_P);
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None;
            this.Name = "EditUserDialog";
            this.Padding = new System.Windows.Forms.Padding(2, 20, 2, 2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditUserDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialComboBox CB_T;
        private MaterialSkin.Controls.MaterialButton CancelButton;
        private MaterialSkin.Controls.MaterialButton DoneButton;
        private MaterialSkin.Controls.MaterialTextBox TB_U;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private MaterialSkin.Controls.MaterialTextBox TB_P;
        private MaterialSkin.Controls.MaterialButton DeleteButton;
    }
}