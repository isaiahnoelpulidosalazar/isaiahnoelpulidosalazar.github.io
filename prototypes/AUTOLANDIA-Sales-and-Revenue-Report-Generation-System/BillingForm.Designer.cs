namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class BillingForm
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
            this.BillingListContainer = new System.Windows.Forms.Panel();
            this.BillingList = new System.Windows.Forms.TableLayoutPanel();
            this.SearchBarBilling = new MaterialSkin.Controls.MaterialTextBox();
            this.materialDivider2 = new MaterialSkin.Controls.MaterialDivider();
            this.FilterBilling = new MaterialSkin.Controls.MaterialComboBox();
            this.DatePickerButton = new MaterialSkin.Controls.MaterialButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CompletedButton = new MaterialSkin.Controls.MaterialButton();
            this.BillingListContainer.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // BillingListContainer
            // 
            this.BillingListContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BillingListContainer.AutoScroll = true;
            this.BillingListContainer.Controls.Add(this.BillingList);
            this.BillingListContainer.Location = new System.Drawing.Point(9, 111);
            this.BillingListContainer.Margin = new System.Windows.Forms.Padding(0);
            this.BillingListContainer.Name = "BillingListContainer";
            this.BillingListContainer.Size = new System.Drawing.Size(832, 240);
            this.BillingListContainer.TabIndex = 20;
            // 
            // BillingList
            // 
            this.BillingList.AutoSize = true;
            this.BillingList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BillingList.ColumnCount = 1;
            this.BillingList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BillingList.Dock = System.Windows.Forms.DockStyle.Top;
            this.BillingList.Location = new System.Drawing.Point(0, 0);
            this.BillingList.Margin = new System.Windows.Forms.Padding(0);
            this.BillingList.Name = "BillingList";
            this.BillingList.RowCount = 1;
            this.BillingList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BillingList.Size = new System.Drawing.Size(832, 0);
            this.BillingList.TabIndex = 0;
            // 
            // SearchBarBilling
            // 
            this.SearchBarBilling.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBarBilling.AnimateReadOnly = false;
            this.SearchBarBilling.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SearchBarBilling.Depth = 0;
            this.SearchBarBilling.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.SearchBarBilling.Hint = "Search";
            this.SearchBarBilling.LeadingIcon = null;
            this.SearchBarBilling.Location = new System.Drawing.Point(9, 9);
            this.SearchBarBilling.Margin = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.SearchBarBilling.MaxLength = 50;
            this.SearchBarBilling.MouseState = MaterialSkin.MouseState.OUT;
            this.SearchBarBilling.Multiline = false;
            this.SearchBarBilling.Name = "SearchBarBilling";
            this.SearchBarBilling.Size = new System.Drawing.Size(425, 36);
            this.SearchBarBilling.TabIndex = 24;
            this.SearchBarBilling.Text = "";
            this.SearchBarBilling.TrailingIcon = null;
            this.SearchBarBilling.UseTallSize = false;
            this.SearchBarBilling.TextChanged += new System.EventHandler(this.SearchBarBilling_TextChanged);
            // 
            // materialDivider2
            // 
            this.materialDivider2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialDivider2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider2.Depth = 0;
            this.materialDivider2.Location = new System.Drawing.Point(9, 53);
            this.materialDivider2.Margin = new System.Windows.Forms.Padding(0, 8, 0, 8);
            this.materialDivider2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider2.Name = "materialDivider2";
            this.materialDivider2.Size = new System.Drawing.Size(832, 2);
            this.materialDivider2.TabIndex = 23;
            this.materialDivider2.Text = "materialDivider2";
            // 
            // FilterBilling
            // 
            this.FilterBilling.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterBilling.AutoResize = false;
            this.FilterBilling.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.FilterBilling.Depth = 0;
            this.FilterBilling.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.FilterBilling.DropDownHeight = 118;
            this.FilterBilling.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FilterBilling.DropDownWidth = 121;
            this.FilterBilling.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.FilterBilling.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FilterBilling.FormattingEnabled = true;
            this.FilterBilling.IntegralHeight = false;
            this.FilterBilling.ItemHeight = 29;
            this.FilterBilling.Location = new System.Drawing.Point(445, 9);
            this.FilterBilling.MaxDropDownItems = 4;
            this.FilterBilling.MouseState = MaterialSkin.MouseState.OUT;
            this.FilterBilling.Name = "FilterBilling";
            this.FilterBilling.Size = new System.Drawing.Size(150, 35);
            this.FilterBilling.StartIndex = 0;
            this.FilterBilling.TabIndex = 28;
            this.FilterBilling.UseTallSize = false;
            this.FilterBilling.TextChanged += new System.EventHandler(this.FilterBilling_TextChanged);
            // 
            // DatePickerButton
            // 
            this.DatePickerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DatePickerButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DatePickerButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.DatePickerButton.Depth = 0;
            this.DatePickerButton.HighEmphasis = true;
            this.DatePickerButton.Icon = null;
            this.DatePickerButton.Location = new System.Drawing.Point(740, 9);
            this.DatePickerButton.Margin = new System.Windows.Forms.Padding(0);
            this.DatePickerButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.DatePickerButton.Name = "DatePickerButton";
            this.DatePickerButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.DatePickerButton.Size = new System.Drawing.Size(101, 36);
            this.DatePickerButton.TabIndex = 30;
            this.DatePickerButton.Text = "2025/10/17";
            this.DatePickerButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.DatePickerButton.UseAccentColor = false;
            this.DatePickerButton.UseVisualStyleBackColor = true;
            this.DatePickerButton.Click += new System.EventHandler(this.DatePickerButton_Click);
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(9, 9);
            this.label6.Margin = new System.Windows.Forms.Padding(8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 30);
            this.label6.TabIndex = 0;
            this.label6.Text = "ID";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(133, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 30);
            this.label5.TabIndex = 1;
            this.label5.Text = "Balance";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(299, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 30);
            this.label4.TabIndex = 2;
            this.label4.Text = "Remaining Balance";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 6;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(9, 63);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(832, 48);
            this.tableLayoutPanel2.TabIndex = 21;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(465, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 30);
            this.label3.TabIndex = 9;
            this.label3.Text = "Status";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(589, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 30);
            this.label1.TabIndex = 8;
            this.label1.Text = "Last Updated";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(713, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 30);
            this.label2.TabIndex = 7;
            this.label2.Text = "Date Created";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CompletedButton
            // 
            this.CompletedButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CompletedButton.AutoSize = false;
            this.CompletedButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CompletedButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.CompletedButton.Depth = 0;
            this.CompletedButton.HighEmphasis = true;
            this.CompletedButton.Icon = null;
            this.CompletedButton.Location = new System.Drawing.Point(606, 9);
            this.CompletedButton.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.CompletedButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.CompletedButton.Name = "CompletedButton";
            this.CompletedButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.CompletedButton.Size = new System.Drawing.Size(126, 36);
            this.CompletedButton.TabIndex = 31;
            this.CompletedButton.Text = "Switch to paid";
            this.CompletedButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.CompletedButton.UseAccentColor = false;
            this.CompletedButton.UseVisualStyleBackColor = true;
            this.CompletedButton.Click += new System.EventHandler(this.CompletedButton_Click);
            // 
            // BillingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 360);
            this.Controls.Add(this.CompletedButton);
            this.Controls.Add(this.DatePickerButton);
            this.Controls.Add(this.FilterBilling);
            this.Controls.Add(this.SearchBarBilling);
            this.Controls.Add(this.materialDivider2);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.BillingListContainer);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BillingForm";
            this.Text = "BillingForm";
            this.Load += new System.EventHandler(this.BillingForm_Load);
            this.BillingListContainer.ResumeLayout(false);
            this.BillingListContainer.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel BillingListContainer;
        private System.Windows.Forms.TableLayoutPanel BillingList;
        private MaterialSkin.Controls.MaterialTextBox SearchBarBilling;
        private MaterialSkin.Controls.MaterialDivider materialDivider2;
        private MaterialSkin.Controls.MaterialComboBox FilterBilling;
        private MaterialSkin.Controls.MaterialButton DatePickerButton;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private MaterialSkin.Controls.MaterialButton CompletedButton;
    }
}