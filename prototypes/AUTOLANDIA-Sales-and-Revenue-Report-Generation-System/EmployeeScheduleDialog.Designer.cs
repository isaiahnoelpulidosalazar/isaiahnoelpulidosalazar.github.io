namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class EmployeeScheduleDialog
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
            this.EmployeeCalendar = new System.Windows.Forms.MonthCalendar();
            this.SearchBarEmployee = new MaterialSkin.Controls.MaterialTextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.EmployeeListContainer = new System.Windows.Forms.Panel();
            this.EmployeeList = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2.SuspendLayout();
            this.EmployeeListContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // EmployeeCalendar
            // 
            this.EmployeeCalendar.CalendarDimensions = new System.Drawing.Size(1, 2);
            this.EmployeeCalendar.Location = new System.Drawing.Point(10, 36);
            this.EmployeeCalendar.Margin = new System.Windows.Forms.Padding(8, 16, 8, 8);
            this.EmployeeCalendar.Name = "EmployeeCalendar";
            this.EmployeeCalendar.TabIndex = 0;
            this.EmployeeCalendar.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.EmployeeCalendar_DateSelected);
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
            this.SearchBarEmployee.Location = new System.Drawing.Point(245, 36);
            this.SearchBarEmployee.Margin = new System.Windows.Forms.Padding(0, 8, 8, 8);
            this.SearchBarEmployee.MaxLength = 50;
            this.SearchBarEmployee.MouseState = MaterialSkin.MouseState.OUT;
            this.SearchBarEmployee.Multiline = false;
            this.SearchBarEmployee.Name = "SearchBarEmployee";
            this.SearchBarEmployee.Size = new System.Drawing.Size(385, 36);
            this.SearchBarEmployee.TabIndex = 15;
            this.SearchBarEmployee.Text = "";
            this.SearchBarEmployee.TrailingIcon = null;
            this.SearchBarEmployee.UseTallSize = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(245, 80);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(385, 48);
            this.tableLayoutPanel2.TabIndex = 96;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(277, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "Time Out";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(162, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 30);
            this.label5.TabIndex = 1;
            this.label5.Text = "Time In";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(9, 9);
            this.label6.Margin = new System.Windows.Forms.Padding(8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(136, 30);
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
            this.EmployeeListContainer.Location = new System.Drawing.Point(245, 128);
            this.EmployeeListContainer.Margin = new System.Windows.Forms.Padding(0);
            this.EmployeeListContainer.Name = "EmployeeListContainer";
            this.EmployeeListContainer.Size = new System.Drawing.Size(385, 219);
            this.EmployeeListContainer.TabIndex = 95;
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
            this.EmployeeList.Size = new System.Drawing.Size(385, 0);
            this.EmployeeList.TabIndex = 0;
            // 
            // EmployeeScheduleDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 357);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.EmployeeListContainer);
            this.Controls.Add(this.SearchBarEmployee);
            this.Controls.Add(this.EmployeeCalendar);
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None;
            this.Name = "EmployeeScheduleDialog";
            this.Padding = new System.Windows.Forms.Padding(2, 20, 2, 2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EmployeeScheduleDialog";
            this.Load += new System.EventHandler(this.EmployeeScheduleDialog_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.EmployeeListContainer.ResumeLayout(false);
            this.EmployeeListContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar EmployeeCalendar;
        private MaterialSkin.Controls.MaterialTextBox SearchBarEmployee;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel EmployeeListContainer;
        private System.Windows.Forms.TableLayoutPanel EmployeeList;
        private System.Windows.Forms.Label label1;
    }
}