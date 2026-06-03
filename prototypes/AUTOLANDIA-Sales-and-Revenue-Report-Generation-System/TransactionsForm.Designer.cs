namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class TransactionsForm
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
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.TransactionListContainer = new System.Windows.Forms.Panel();
            this.TransactionList = new System.Windows.Forms.TableLayoutPanel();
            this.materialDivider2 = new MaterialSkin.Controls.MaterialDivider();
            this.AddNewTransactionButton = new MaterialSkin.Controls.MaterialButton();
            this.SearchBarTransaction = new MaterialSkin.Controls.MaterialTextBox();
            this.FilterTransaction = new MaterialSkin.Controls.MaterialComboBox();
            this.DatePickerButton = new MaterialSkin.Controls.MaterialButton();
            this.CompletedButton = new MaterialSkin.Controls.MaterialButton();
            this.tableLayoutPanel2.SuspendLayout();
            this.TransactionListContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 7;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.Controls.Add(this.label3, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.label2, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.label1, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.label7, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(9, 63);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(955, 48);
            this.tableLayoutPanel2.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(819, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 30);
            this.label3.TabIndex = 6;
            this.label3.Text = "Date Created";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(676, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 30);
            this.label2.TabIndex = 5;
            this.label2.Text = "Last Updated";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(533, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 30);
            this.label1.TabIndex = 4;
            this.label1.Text = "Status";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(390, 9);
            this.label7.Margin = new System.Windows.Forms.Padding(8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 30);
            this.label7.TabIndex = 3;
            this.label7.Text = "Plate Number";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(247, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(126, 30);
            this.label4.TabIndex = 2;
            this.label4.Text = "Service/Package";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(104, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 30);
            this.label5.TabIndex = 1;
            this.label5.Text = "Employee(s)";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(9, 9);
            this.label6.Margin = new System.Windows.Forms.Padding(8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 30);
            this.label6.TabIndex = 0;
            this.label6.Text = "ID";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TransactionListContainer
            // 
            this.TransactionListContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TransactionListContainer.AutoScroll = true;
            this.TransactionListContainer.Controls.Add(this.TransactionList);
            this.TransactionListContainer.Location = new System.Drawing.Point(9, 111);
            this.TransactionListContainer.Margin = new System.Windows.Forms.Padding(0);
            this.TransactionListContainer.Name = "TransactionListContainer";
            this.TransactionListContainer.Size = new System.Drawing.Size(955, 240);
            this.TransactionListContainer.TabIndex = 16;
            // 
            // TransactionList
            // 
            this.TransactionList.AutoSize = true;
            this.TransactionList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TransactionList.ColumnCount = 1;
            this.TransactionList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TransactionList.Dock = System.Windows.Forms.DockStyle.Top;
            this.TransactionList.Location = new System.Drawing.Point(0, 0);
            this.TransactionList.Margin = new System.Windows.Forms.Padding(0);
            this.TransactionList.Name = "TransactionList";
            this.TransactionList.RowCount = 1;
            this.TransactionList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.TransactionList.Size = new System.Drawing.Size(955, 0);
            this.TransactionList.TabIndex = 0;
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
            this.materialDivider2.Size = new System.Drawing.Size(955, 2);
            this.materialDivider2.TabIndex = 15;
            this.materialDivider2.Text = "materialDivider2";
            // 
            // AddNewTransactionButton
            // 
            this.AddNewTransactionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddNewTransactionButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AddNewTransactionButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.AddNewTransactionButton.Depth = 0;
            this.AddNewTransactionButton.HighEmphasis = true;
            this.AddNewTransactionButton.Icon = null;
            this.AddNewTransactionButton.Location = new System.Drawing.Point(774, 9);
            this.AddNewTransactionButton.Margin = new System.Windows.Forms.Padding(0);
            this.AddNewTransactionButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.AddNewTransactionButton.Name = "AddNewTransactionButton";
            this.AddNewTransactionButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.AddNewTransactionButton.Size = new System.Drawing.Size(190, 36);
            this.AddNewTransactionButton.TabIndex = 14;
            this.AddNewTransactionButton.Text = "Add new transaction";
            this.AddNewTransactionButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.AddNewTransactionButton.UseAccentColor = false;
            this.AddNewTransactionButton.UseVisualStyleBackColor = true;
            this.AddNewTransactionButton.Click += new System.EventHandler(this.AddNewTransactionButton_Click);
            // 
            // SearchBarTransaction
            // 
            this.SearchBarTransaction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBarTransaction.AnimateReadOnly = false;
            this.SearchBarTransaction.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SearchBarTransaction.Depth = 0;
            this.SearchBarTransaction.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.SearchBarTransaction.Hint = "Search";
            this.SearchBarTransaction.LeadingIcon = null;
            this.SearchBarTransaction.Location = new System.Drawing.Point(9, 9);
            this.SearchBarTransaction.MaxLength = 50;
            this.SearchBarTransaction.MouseState = MaterialSkin.MouseState.OUT;
            this.SearchBarTransaction.Multiline = false;
            this.SearchBarTransaction.Name = "SearchBarTransaction";
            this.SearchBarTransaction.Size = new System.Drawing.Size(282, 36);
            this.SearchBarTransaction.TabIndex = 21;
            this.SearchBarTransaction.Text = "";
            this.SearchBarTransaction.TrailingIcon = null;
            this.SearchBarTransaction.UseTallSize = false;
            this.SearchBarTransaction.TextChanged += new System.EventHandler(this.SearchBarTransaction_TextChanged);
            // 
            // FilterTransaction
            // 
            this.FilterTransaction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterTransaction.AutoResize = false;
            this.FilterTransaction.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.FilterTransaction.Depth = 0;
            this.FilterTransaction.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.FilterTransaction.DropDownHeight = 118;
            this.FilterTransaction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FilterTransaction.DropDownWidth = 121;
            this.FilterTransaction.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.FilterTransaction.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FilterTransaction.FormattingEnabled = true;
            this.FilterTransaction.IntegralHeight = false;
            this.FilterTransaction.ItemHeight = 29;
            this.FilterTransaction.Location = new System.Drawing.Point(302, 9);
            this.FilterTransaction.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.FilterTransaction.MaxDropDownItems = 4;
            this.FilterTransaction.MouseState = MaterialSkin.MouseState.OUT;
            this.FilterTransaction.Name = "FilterTransaction";
            this.FilterTransaction.Size = new System.Drawing.Size(127, 35);
            this.FilterTransaction.StartIndex = 0;
            this.FilterTransaction.TabIndex = 28;
            this.FilterTransaction.UseTallSize = false;
            this.FilterTransaction.TextChanged += new System.EventHandler(this.FilterTransaction_TextChanged);
            // 
            // DatePickerButton
            // 
            this.DatePickerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DatePickerButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DatePickerButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.DatePickerButton.Depth = 0;
            this.DatePickerButton.HighEmphasis = true;
            this.DatePickerButton.Icon = null;
            this.DatePickerButton.Location = new System.Drawing.Point(665, 9);
            this.DatePickerButton.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.DatePickerButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.DatePickerButton.Name = "DatePickerButton";
            this.DatePickerButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.DatePickerButton.Size = new System.Drawing.Size(101, 36);
            this.DatePickerButton.TabIndex = 29;
            this.DatePickerButton.Text = "2025/10/17";
            this.DatePickerButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.DatePickerButton.UseAccentColor = false;
            this.DatePickerButton.UseVisualStyleBackColor = true;
            this.DatePickerButton.Click += new System.EventHandler(this.DatePickerButton_Click);
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
            this.CompletedButton.Location = new System.Drawing.Point(437, 9);
            this.CompletedButton.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.CompletedButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.CompletedButton.Name = "CompletedButton";
            this.CompletedButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.CompletedButton.Size = new System.Drawing.Size(220, 36);
            this.CompletedButton.TabIndex = 30;
            this.CompletedButton.Text = "Switch to completed";
            this.CompletedButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.CompletedButton.UseAccentColor = false;
            this.CompletedButton.UseVisualStyleBackColor = true;
            this.CompletedButton.Click += new System.EventHandler(this.CompletedButton_Click);
            // 
            // TransactionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(973, 360);
            this.Controls.Add(this.CompletedButton);
            this.Controls.Add(this.DatePickerButton);
            this.Controls.Add(this.FilterTransaction);
            this.Controls.Add(this.SearchBarTransaction);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.TransactionListContainer);
            this.Controls.Add(this.materialDivider2);
            this.Controls.Add(this.AddNewTransactionButton);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TransactionsForm";
            this.Text = "TransactionsForm";
            this.Load += new System.EventHandler(this.TransactionsForm_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.TransactionListContainer.ResumeLayout(false);
            this.TransactionListContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel TransactionListContainer;
        private System.Windows.Forms.TableLayoutPanel TransactionList;
        private MaterialSkin.Controls.MaterialDivider materialDivider2;
        private MaterialSkin.Controls.MaterialButton AddNewTransactionButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private MaterialSkin.Controls.MaterialTextBox SearchBarTransaction;
        private MaterialSkin.Controls.MaterialComboBox FilterTransaction;
        private MaterialSkin.Controls.MaterialButton DatePickerButton;
        private MaterialSkin.Controls.MaterialButton CompletedButton;
    }
}