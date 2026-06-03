namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class SummaryDialog
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.SalaryTab = new System.Windows.Forms.TabPage();
            this.CustomersTab = new System.Windows.Forms.TabPage();
            this.SummaryCalendar = new System.Windows.Forms.MonthCalendar();
            this.SearchBarEmployee = new MaterialSkin.Controls.MaterialTextBox();
            this.DateRange = new MaterialSkin.Controls.MaterialLabel();
            this.SummaryText = new MaterialSkin.Controls.MaterialLabel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.EmployeeListContainer = new System.Windows.Forms.Panel();
            this.EmployeeList = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.TransactionListContainer = new System.Windows.Forms.Panel();
            this.TransactionList = new System.Windows.Forms.TableLayoutPanel();
            this.EmployeeListCheckBox = new MaterialSkin.Controls.MaterialCheckedListBox();
            this.tabControl1.SuspendLayout();
            this.SalaryTab.SuspendLayout();
            this.CustomersTab.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.EmployeeListContainer.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.TransactionListContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.SalaryTab);
            this.tabControl1.Controls.Add(this.CustomersTab);
            this.tabControl1.ItemSize = new System.Drawing.Size(150, 48);
            this.tabControl1.Location = new System.Drawing.Point(10, 206);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(830, 424);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 1;
            // 
            // SalaryTab
            // 
            this.SalaryTab.BackColor = System.Drawing.Color.White;
            this.SalaryTab.Controls.Add(this.tableLayoutPanel2);
            this.SalaryTab.Controls.Add(this.EmployeeListContainer);
            this.SalaryTab.Location = new System.Drawing.Point(4, 52);
            this.SalaryTab.Margin = new System.Windows.Forms.Padding(0);
            this.SalaryTab.Name = "SalaryTab";
            this.SalaryTab.Size = new System.Drawing.Size(822, 368);
            this.SalaryTab.TabIndex = 0;
            this.SalaryTab.Text = "Salary";
            // 
            // CustomersTab
            // 
            this.CustomersTab.BackColor = System.Drawing.Color.White;
            this.CustomersTab.Controls.Add(this.EmployeeListCheckBox);
            this.CustomersTab.Controls.Add(this.tableLayoutPanel1);
            this.CustomersTab.Controls.Add(this.TransactionListContainer);
            this.CustomersTab.Location = new System.Drawing.Point(4, 52);
            this.CustomersTab.Margin = new System.Windows.Forms.Padding(0);
            this.CustomersTab.Name = "CustomersTab";
            this.CustomersTab.Size = new System.Drawing.Size(822, 368);
            this.CustomersTab.TabIndex = 1;
            this.CustomersTab.Text = "Employee Work Breakdown";
            // 
            // SummaryCalendar
            // 
            this.SummaryCalendar.CalendarDimensions = new System.Drawing.Size(2, 1);
            this.SummaryCalendar.Location = new System.Drawing.Point(10, 36);
            this.SummaryCalendar.Margin = new System.Windows.Forms.Padding(8, 16, 8, 8);
            this.SummaryCalendar.MaxSelectionCount = 1;
            this.SummaryCalendar.Name = "SummaryCalendar";
            this.SummaryCalendar.TabIndex = 98;
            this.SummaryCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.SummaryCalendar_DateSelected);
            // 
            // SearchBarEmployee
            // 
            this.SearchBarEmployee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBarEmployee.AnimateReadOnly = false;
            this.SearchBarEmployee.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SearchBarEmployee.Depth = 0;
            this.SearchBarEmployee.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.SearchBarEmployee.Hint = "Search";
            this.SearchBarEmployee.LeadingIcon = null;
            this.SearchBarEmployee.Location = new System.Drawing.Point(476, 36);
            this.SearchBarEmployee.Margin = new System.Windows.Forms.Padding(0, 8, 8, 8);
            this.SearchBarEmployee.MaxLength = 50;
            this.SearchBarEmployee.MouseState = MaterialSkin.MouseState.OUT;
            this.SearchBarEmployee.Multiline = false;
            this.SearchBarEmployee.Name = "SearchBarEmployee";
            this.SearchBarEmployee.Size = new System.Drawing.Size(360, 36);
            this.SearchBarEmployee.TabIndex = 99;
            this.SearchBarEmployee.Text = "";
            this.SearchBarEmployee.TrailingIcon = null;
            this.SearchBarEmployee.UseTallSize = false;
            // 
            // DateRange
            // 
            this.DateRange.Depth = 0;
            this.DateRange.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.DateRange.Location = new System.Drawing.Point(476, 80);
            this.DateRange.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.DateRange.MouseState = MaterialSkin.MouseState.HOVER;
            this.DateRange.Name = "DateRange";
            this.DateRange.Size = new System.Drawing.Size(360, 29);
            this.DateRange.TabIndex = 102;
            this.DateRange.Text = "Date - Date";
            this.DateRange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SummaryText
            // 
            this.SummaryText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SummaryText.Depth = 0;
            this.SummaryText.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.SummaryText.Location = new System.Drawing.Point(476, 117);
            this.SummaryText.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.SummaryText.MouseState = MaterialSkin.MouseState.HOVER;
            this.SummaryText.Name = "SummaryText";
            this.SummaryText.Size = new System.Drawing.Size(364, 81);
            this.SummaryText.TabIndex = 114;
            this.SummaryText.Text = "SummaryText";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(822, 48);
            this.tableLayoutPanel2.TabIndex = 102;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(501, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(312, 30);
            this.label5.TabIndex = 1;
            this.label5.Text = "Salary";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(9, 9);
            this.label6.Margin = new System.Windows.Forms.Padding(8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(475, 30);
            this.label6.TabIndex = 0;
            this.label6.Text = "Employee Name";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EmployeeListContainer
            // 
            this.EmployeeListContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EmployeeListContainer.AutoScroll = true;
            this.EmployeeListContainer.Controls.Add(this.EmployeeList);
            this.EmployeeListContainer.Location = new System.Drawing.Point(0, 48);
            this.EmployeeListContainer.Margin = new System.Windows.Forms.Padding(0);
            this.EmployeeListContainer.Name = "EmployeeListContainer";
            this.EmployeeListContainer.Size = new System.Drawing.Size(822, 320);
            this.EmployeeListContainer.TabIndex = 101;
            // 
            // EmployeeList
            // 
            this.EmployeeList.AutoSize = true;
            this.EmployeeList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.EmployeeList.ColumnCount = 1;
            this.EmployeeList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.EmployeeList.Dock = System.Windows.Forms.DockStyle.Top;
            this.EmployeeList.Location = new System.Drawing.Point(0, 0);
            this.EmployeeList.Margin = new System.Windows.Forms.Padding(0);
            this.EmployeeList.Name = "EmployeeList";
            this.EmployeeList.RowCount = 1;
            this.EmployeeList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.EmployeeList.Size = new System.Drawing.Size(822, 0);
            this.EmployeeList.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(250, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(572, 48);
            this.tableLayoutPanel1.TabIndex = 113;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(465, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 30);
            this.label1.TabIndex = 11;
            this.label1.Text = "Amount";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(351, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 30);
            this.label4.TabIndex = 10;
            this.label4.Text = "Service/Package";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(237, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 30);
            this.label3.TabIndex = 9;
            this.label3.Text = "Vehicle";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(123, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 30);
            this.label2.TabIndex = 8;
            this.label2.Text = "Plate Number";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(9, 9);
            this.label7.Margin = new System.Windows.Forms.Padding(8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(97, 30);
            this.label7.TabIndex = 7;
            this.label7.Text = "Employee Name";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TransactionListContainer
            // 
            this.TransactionListContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TransactionListContainer.AutoScroll = true;
            this.TransactionListContainer.Controls.Add(this.TransactionList);
            this.TransactionListContainer.Location = new System.Drawing.Point(250, 48);
            this.TransactionListContainer.Margin = new System.Windows.Forms.Padding(0, 0, 8, 8);
            this.TransactionListContainer.Name = "TransactionListContainer";
            this.TransactionListContainer.Size = new System.Drawing.Size(572, 320);
            this.TransactionListContainer.TabIndex = 112;
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
            this.TransactionList.Size = new System.Drawing.Size(572, 0);
            this.TransactionList.TabIndex = 0;
            // 
            // EmployeeListCheckBox
            // 
            this.EmployeeListCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.EmployeeListCheckBox.AutoScroll = true;
            this.EmployeeListCheckBox.BackColor = System.Drawing.Color.White;
            this.EmployeeListCheckBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EmployeeListCheckBox.Depth = 0;
            this.EmployeeListCheckBox.Location = new System.Drawing.Point(0, 0);
            this.EmployeeListCheckBox.Margin = new System.Windows.Forms.Padding(0);
            this.EmployeeListCheckBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.EmployeeListCheckBox.Name = "EmployeeListCheckBox";
            this.EmployeeListCheckBox.Size = new System.Drawing.Size(250, 368);
            this.EmployeeListCheckBox.Striped = false;
            this.EmployeeListCheckBox.StripeDarkColor = System.Drawing.Color.Empty;
            this.EmployeeListCheckBox.TabIndex = 114;
            // 
            // SummaryDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 640);
            this.Controls.Add(this.SummaryText);
            this.Controls.Add(this.DateRange);
            this.Controls.Add(this.SearchBarEmployee);
            this.Controls.Add(this.SummaryCalendar);
            this.Controls.Add(this.tabControl1);
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None;
            this.Name = "SummaryDialog";
            this.Padding = new System.Windows.Forms.Padding(2, 20, 2, 2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SummaryDialog";
            this.Load += new System.EventHandler(this.SummaryDialog_Load);
            this.tabControl1.ResumeLayout(false);
            this.SalaryTab.ResumeLayout(false);
            this.CustomersTab.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.EmployeeListContainer.ResumeLayout(false);
            this.EmployeeListContainer.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.TransactionListContainer.ResumeLayout(false);
            this.TransactionListContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage SalaryTab;
        private System.Windows.Forms.TabPage CustomersTab;
        private System.Windows.Forms.MonthCalendar SummaryCalendar;
        private MaterialSkin.Controls.MaterialTextBox SearchBarEmployee;
        private MaterialSkin.Controls.MaterialLabel DateRange;
        private MaterialSkin.Controls.MaterialLabel SummaryText;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel EmployeeListContainer;
        private System.Windows.Forms.TableLayoutPanel EmployeeList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel TransactionListContainer;
        private System.Windows.Forms.TableLayoutPanel TransactionList;
        private MaterialSkin.Controls.MaterialCheckedListBox EmployeeListCheckBox;
    }
}