namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class BillDetailDialog
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
            this.TB_ID = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.CancelButton = new MaterialSkin.Controls.MaterialButton();
            this.ReceiptButton = new MaterialSkin.Controls.MaterialButton();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.TB_Price = new MaterialSkin.Controls.MaterialTextBox();
            this.TB_DateCreated = new MaterialSkin.Controls.MaterialLabel();
            this.TB_LastUpdated = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel6 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel8 = new MaterialSkin.Controls.MaterialLabel();
            this.PayButton = new MaterialSkin.Controls.MaterialButton();
            this.L_Discount = new MaterialSkin.Controls.MaterialLabel();
            this.SuspendLayout();
            // 
            // TB_ID
            // 
            this.TB_ID.AutoSize = true;
            this.TB_ID.Depth = 0;
            this.TB_ID.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TB_ID.Location = new System.Drawing.Point(200, 105);
            this.TB_ID.MouseState = MaterialSkin.MouseState.HOVER;
            this.TB_ID.Name = "TB_ID";
            this.TB_ID.Size = new System.Drawing.Size(64, 19);
            this.TB_ID.TabIndex = 45;
            this.TB_ID.Text = "Billing ID";
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel3.Location = new System.Drawing.Point(12, 164);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(58, 19);
            this.materialLabel3.TabIndex = 41;
            this.materialLabel3.Text = "Balance";
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel2.Location = new System.Drawing.Point(12, 105);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(64, 19);
            this.materialLabel2.TabIndex = 40;
            this.materialLabel2.Text = "Billing ID";
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.CancelButton.Depth = 0;
            this.CancelButton.HighEmphasis = true;
            this.CancelButton.Icon = null;
            this.CancelButton.Location = new System.Drawing.Point(10, 394);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(8);
            this.CancelButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.CancelButton.Size = new System.Drawing.Size(66, 36);
            this.CancelButton.TabIndex = 39;
            this.CancelButton.Text = "Close";
            this.CancelButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.CancelButton.UseAccentColor = false;
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ReceiptButton
            // 
            this.ReceiptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ReceiptButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ReceiptButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.ReceiptButton.Depth = 0;
            this.ReceiptButton.HighEmphasis = true;
            this.ReceiptButton.Icon = null;
            this.ReceiptButton.Location = new System.Drawing.Point(367, 394);
            this.ReceiptButton.Margin = new System.Windows.Forms.Padding(8);
            this.ReceiptButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.ReceiptButton.Name = "ReceiptButton";
            this.ReceiptButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.ReceiptButton.Size = new System.Drawing.Size(109, 36);
            this.ReceiptButton.TabIndex = 38;
            this.ReceiptButton.Text = "See receipt";
            this.ReceiptButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.ReceiptButton.UseAccentColor = false;
            this.ReceiptButton.UseVisualStyleBackColor = true;
            this.ReceiptButton.Visible = false;
            this.ReceiptButton.Click += new System.EventHandler(this.ReceiptButton_Click);
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
            this.materialLabel1.Size = new System.Drawing.Size(100, 29);
            this.materialLabel1.TabIndex = 36;
            this.materialLabel1.Text = "Bill detail";
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
            this.materialDivider1.Size = new System.Drawing.Size(466, 2);
            this.materialDivider1.TabIndex = 37;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // TB_Price
            // 
            this.TB_Price.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Price.AnimateReadOnly = false;
            this.TB_Price.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_Price.Depth = 0;
            this.TB_Price.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TB_Price.LeadingIcon = null;
            this.TB_Price.Location = new System.Drawing.Point(203, 149);
            this.TB_Price.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.TB_Price.MaxLength = 50;
            this.TB_Price.MouseState = MaterialSkin.MouseState.OUT;
            this.TB_Price.Multiline = false;
            this.TB_Price.Name = "TB_Price";
            this.TB_Price.ReadOnly = true;
            this.TB_Price.Size = new System.Drawing.Size(273, 50);
            this.TB_Price.TabIndex = 49;
            this.TB_Price.Text = "";
            this.TB_Price.TrailingIcon = null;
            // 
            // TB_DateCreated
            // 
            this.TB_DateCreated.AutoSize = true;
            this.TB_DateCreated.Depth = 0;
            this.TB_DateCreated.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TB_DateCreated.Location = new System.Drawing.Point(200, 331);
            this.TB_DateCreated.MouseState = MaterialSkin.MouseState.HOVER;
            this.TB_DateCreated.Name = "TB_DateCreated";
            this.TB_DateCreated.Size = new System.Drawing.Size(92, 19);
            this.TB_DateCreated.TabIndex = 95;
            this.TB_DateCreated.Text = "Date Created";
            // 
            // TB_LastUpdated
            // 
            this.TB_LastUpdated.AutoSize = true;
            this.TB_LastUpdated.Depth = 0;
            this.TB_LastUpdated.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TB_LastUpdated.Location = new System.Drawing.Point(200, 331);
            this.TB_LastUpdated.MouseState = MaterialSkin.MouseState.HOVER;
            this.TB_LastUpdated.Name = "TB_LastUpdated";
            this.TB_LastUpdated.Size = new System.Drawing.Size(95, 19);
            this.TB_LastUpdated.TabIndex = 94;
            this.TB_LastUpdated.Text = "Last Updated";
            this.TB_LastUpdated.Visible = false;
            // 
            // materialLabel6
            // 
            this.materialLabel6.AutoSize = true;
            this.materialLabel6.Depth = 0;
            this.materialLabel6.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel6.Location = new System.Drawing.Point(12, 331);
            this.materialLabel6.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel6.Name = "materialLabel6";
            this.materialLabel6.Size = new System.Drawing.Size(92, 19);
            this.materialLabel6.TabIndex = 93;
            this.materialLabel6.Text = "Date Created";
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel4.Location = new System.Drawing.Point(12, 331);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(95, 19);
            this.materialLabel4.TabIndex = 91;
            this.materialLabel4.Text = "Last Updated";
            this.materialLabel4.Visible = false;
            // 
            // materialLabel8
            // 
            this.materialLabel8.AutoSize = true;
            this.materialLabel8.Depth = 0;
            this.materialLabel8.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel8.Location = new System.Drawing.Point(12, 277);
            this.materialLabel8.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel8.Name = "materialLabel8";
            this.materialLabel8.Size = new System.Drawing.Size(64, 19);
            this.materialLabel8.TabIndex = 97;
            this.materialLabel8.Text = "Discount";
            // 
            // PayButton
            // 
            this.PayButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PayButton.AutoSize = false;
            this.PayButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PayButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.PayButton.Depth = 0;
            this.PayButton.HighEmphasis = true;
            this.PayButton.Icon = null;
            this.PayButton.Location = new System.Drawing.Point(203, 207);
            this.PayButton.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.PayButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.PayButton.Name = "PayButton";
            this.PayButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.PayButton.Size = new System.Drawing.Size(273, 50);
            this.PayButton.TabIndex = 100;
            this.PayButton.Text = "Pay";
            this.PayButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.PayButton.UseAccentColor = false;
            this.PayButton.UseVisualStyleBackColor = true;
            this.PayButton.Click += new System.EventHandler(this.PayButton_Click);
            // 
            // L_Discount
            // 
            this.L_Discount.AutoSize = true;
            this.L_Discount.Depth = 0;
            this.L_Discount.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.L_Discount.Location = new System.Drawing.Point(200, 277);
            this.L_Discount.MouseState = MaterialSkin.MouseState.HOVER;
            this.L_Discount.Name = "L_Discount";
            this.L_Discount.Size = new System.Drawing.Size(64, 19);
            this.L_Discount.TabIndex = 101;
            this.L_Discount.Text = "Discount";
            // 
            // BillDetailDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 440);
            this.Controls.Add(this.L_Discount);
            this.Controls.Add(this.PayButton);
            this.Controls.Add(this.materialLabel8);
            this.Controls.Add(this.TB_DateCreated);
            this.Controls.Add(this.TB_LastUpdated);
            this.Controls.Add(this.materialLabel6);
            this.Controls.Add(this.materialLabel4);
            this.Controls.Add(this.TB_Price);
            this.Controls.Add(this.TB_ID);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ReceiptButton);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.materialDivider1);
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None;
            this.Name = "BillDetailDialog";
            this.Padding = new System.Windows.Forms.Padding(2, 20, 2, 2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditBillingDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MaterialSkin.Controls.MaterialLabel TB_ID;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialButton CancelButton;
        private MaterialSkin.Controls.MaterialButton ReceiptButton;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private MaterialSkin.Controls.MaterialTextBox TB_Price;
        private MaterialSkin.Controls.MaterialLabel TB_DateCreated;
        private MaterialSkin.Controls.MaterialLabel TB_LastUpdated;
        private MaterialSkin.Controls.MaterialLabel materialLabel6;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
        private MaterialSkin.Controls.MaterialLabel materialLabel8;
        private MaterialSkin.Controls.MaterialButton PayButton;
        private MaterialSkin.Controls.MaterialLabel L_Discount;
    }
}