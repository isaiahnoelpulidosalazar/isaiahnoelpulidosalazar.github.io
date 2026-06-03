namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class PaymentDialog
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
            this.TB_Amount = new MaterialSkin.Controls.MaterialTextBox();
            this.BillID = new MaterialSkin.Controls.MaterialLabel();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.DoneButton = new MaterialSkin.Controls.MaterialButton();
            this.CancelButton = new MaterialSkin.Controls.MaterialButton();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.TB_Balance = new MaterialSkin.Controls.MaterialTextBox();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.TB_Change = new MaterialSkin.Controls.MaterialTextBox();
            this.SuspendLayout();
            // 
            // TB_Amount
            // 
            this.TB_Amount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Amount.AnimateReadOnly = false;
            this.TB_Amount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_Amount.Depth = 0;
            this.TB_Amount.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TB_Amount.LeadingIcon = null;
            this.TB_Amount.Location = new System.Drawing.Point(190, 165);
            this.TB_Amount.Margin = new System.Windows.Forms.Padding(8);
            this.TB_Amount.MaxLength = 50;
            this.TB_Amount.MouseState = MaterialSkin.MouseState.OUT;
            this.TB_Amount.Multiline = false;
            this.TB_Amount.Name = "TB_Amount";
            this.TB_Amount.Size = new System.Drawing.Size(328, 50);
            this.TB_Amount.TabIndex = 84;
            this.TB_Amount.Text = "";
            this.TB_Amount.TrailingIcon = null;
            this.TB_Amount.TextChanged += new System.EventHandler(this.TB_Amount_TextChanged);
            // 
            // BillID
            // 
            this.BillID.AutoSize = true;
            this.BillID.Depth = 0;
            this.BillID.Font = new System.Drawing.Font("Roboto", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.BillID.FontType = MaterialSkin.MaterialSkinManager.fontType.H5;
            this.BillID.Location = new System.Drawing.Point(10, 36);
            this.BillID.Margin = new System.Windows.Forms.Padding(8, 16, 8, 8);
            this.BillID.MouseState = MaterialSkin.MouseState.HOVER;
            this.BillID.Name = "BillID";
            this.BillID.Size = new System.Drawing.Size(95, 29);
            this.BillID.TabIndex = 82;
            this.BillID.Text = "Payment";
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
            this.materialDivider1.Size = new System.Drawing.Size(508, 2);
            this.materialDivider1.TabIndex = 83;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // DoneButton
            // 
            this.DoneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DoneButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DoneButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.DoneButton.Depth = 0;
            this.DoneButton.HighEmphasis = true;
            this.DoneButton.Icon = null;
            this.DoneButton.Location = new System.Drawing.Point(454, 312);
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
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.CancelButton.Depth = 0;
            this.CancelButton.HighEmphasis = true;
            this.CancelButton.Icon = null;
            this.CancelButton.Location = new System.Drawing.Point(10, 312);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(8);
            this.CancelButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.CancelButton.Size = new System.Drawing.Size(77, 36);
            this.CancelButton.TabIndex = 88;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.CancelButton.UseAccentColor = false;
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel2.Location = new System.Drawing.Point(12, 114);
            this.materialLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(105, 19);
            this.materialLabel2.TabIndex = 90;
            this.materialLabel2.Text = "Amount to pay";
            // 
            // TB_Balance
            // 
            this.TB_Balance.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Balance.AnimateReadOnly = false;
            this.TB_Balance.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_Balance.Depth = 0;
            this.TB_Balance.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TB_Balance.LeadingIcon = null;
            this.TB_Balance.Location = new System.Drawing.Point(190, 99);
            this.TB_Balance.Margin = new System.Windows.Forms.Padding(8);
            this.TB_Balance.MaxLength = 50;
            this.TB_Balance.MouseState = MaterialSkin.MouseState.OUT;
            this.TB_Balance.Multiline = false;
            this.TB_Balance.Name = "TB_Balance";
            this.TB_Balance.ReadOnly = true;
            this.TB_Balance.Size = new System.Drawing.Size(328, 50);
            this.TB_Balance.TabIndex = 89;
            this.TB_Balance.Text = "";
            this.TB_Balance.TrailingIcon = null;
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.Location = new System.Drawing.Point(12, 179);
            this.materialLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(102, 19);
            this.materialLabel1.TabIndex = 91;
            this.materialLabel1.Text = "Cash tendered";
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel3.Location = new System.Drawing.Point(12, 245);
            this.materialLabel3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(55, 19);
            this.materialLabel3.TabIndex = 93;
            this.materialLabel3.Text = "Change";
            // 
            // TB_Change
            // 
            this.TB_Change.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Change.AnimateReadOnly = false;
            this.TB_Change.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_Change.Depth = 0;
            this.TB_Change.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TB_Change.LeadingIcon = null;
            this.TB_Change.Location = new System.Drawing.Point(190, 231);
            this.TB_Change.Margin = new System.Windows.Forms.Padding(8);
            this.TB_Change.MaxLength = 50;
            this.TB_Change.MouseState = MaterialSkin.MouseState.OUT;
            this.TB_Change.Multiline = false;
            this.TB_Change.Name = "TB_Change";
            this.TB_Change.ReadOnly = true;
            this.TB_Change.Size = new System.Drawing.Size(328, 50);
            this.TB_Change.TabIndex = 92;
            this.TB_Change.Text = "";
            this.TB_Change.TrailingIcon = null;
            // 
            // PaymentDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 358);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.TB_Change);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.TB_Balance);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.TB_Amount);
            this.Controls.Add(this.BillID);
            this.Controls.Add(this.materialDivider1);
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None;
            this.Name = "PaymentDialog";
            this.Padding = new System.Windows.Forms.Padding(2, 20, 2, 2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PaymentDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PaymentDialog_FormClosing);
            this.Load += new System.EventHandler(this.PaymentDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MaterialSkin.Controls.MaterialTextBox TB_Amount;
        private MaterialSkin.Controls.MaterialLabel BillID;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private MaterialSkin.Controls.MaterialButton DoneButton;
        private MaterialSkin.Controls.MaterialButton CancelButton;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialTextBox TB_Balance;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialTextBox TB_Change;
    }
}