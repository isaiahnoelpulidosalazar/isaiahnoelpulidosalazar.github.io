using System.Windows.Forms;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class PeopleForm
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
            this.EmployeesTab = new System.Windows.Forms.TabPage();
            this.EmployeeScheduleButton = new MaterialSkin.Controls.MaterialButton();
            this.FilterEmployee = new MaterialSkin.Controls.MaterialComboBox();
            this.SearchBarEmployee = new MaterialSkin.Controls.MaterialTextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.EmployeeListContainer = new System.Windows.Forms.Panel();
            this.EmployeeList = new System.Windows.Forms.TableLayoutPanel();
            this.materialDivider2 = new MaterialSkin.Controls.MaterialDivider();
            this.AddNewEmployeeButton = new MaterialSkin.Controls.MaterialButton();
            this.CustomersTab = new System.Windows.Forms.TabPage();
            this.FilterCustomer = new MaterialSkin.Controls.MaterialComboBox();
            this.SearchBarCustomer = new MaterialSkin.Controls.MaterialTextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CustomerListContainer = new System.Windows.Forms.Panel();
            this.CustomerList = new System.Windows.Forms.TableLayoutPanel();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.AddNewCustomerButton = new MaterialSkin.Controls.MaterialButton();
            this.VehiclesTab = new System.Windows.Forms.TabPage();
            this.FilterVehicle = new MaterialSkin.Controls.MaterialComboBox();
            this.SearchBarVehicle = new MaterialSkin.Controls.MaterialTextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.VehicleListContainer = new System.Windows.Forms.Panel();
            this.VehicleList = new System.Windows.Forms.TableLayoutPanel();
            this.materialDivider3 = new MaterialSkin.Controls.MaterialDivider();
            this.AddNewVehicleButton = new MaterialSkin.Controls.MaterialButton();
            this.UserAccountsTab = new System.Windows.Forms.TabPage();
            this.FilterUser = new MaterialSkin.Controls.MaterialComboBox();
            this.SearchUser = new MaterialSkin.Controls.MaterialTextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label15 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.UserList = new System.Windows.Forms.TableLayoutPanel();
            this.materialDivider4 = new MaterialSkin.Controls.MaterialDivider();
            this.AddUserButton = new MaterialSkin.Controls.MaterialButton();
            this.tabControl1.SuspendLayout();
            this.EmployeesTab.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.EmployeeListContainer.SuspendLayout();
            this.CustomersTab.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.CustomerListContainer.SuspendLayout();
            this.VehiclesTab.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.VehicleListContainer.SuspendLayout();
            this.UserAccountsTab.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.EmployeesTab);
            this.tabControl1.Controls.Add(this.CustomersTab);
            this.tabControl1.Controls.Add(this.VehiclesTab);
            this.tabControl1.Controls.Add(this.UserAccountsTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(96, 48);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(960, 360);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 0;
            // 
            // EmployeesTab
            // 
            this.EmployeesTab.BackColor = System.Drawing.Color.White;
            this.EmployeesTab.Controls.Add(this.EmployeeScheduleButton);
            this.EmployeesTab.Controls.Add(this.FilterEmployee);
            this.EmployeesTab.Controls.Add(this.SearchBarEmployee);
            this.EmployeesTab.Controls.Add(this.tableLayoutPanel2);
            this.EmployeesTab.Controls.Add(this.EmployeeListContainer);
            this.EmployeesTab.Controls.Add(this.materialDivider2);
            this.EmployeesTab.Controls.Add(this.AddNewEmployeeButton);
            this.EmployeesTab.Location = new System.Drawing.Point(4, 52);
            this.EmployeesTab.Margin = new System.Windows.Forms.Padding(0);
            this.EmployeesTab.Name = "EmployeesTab";
            this.EmployeesTab.Size = new System.Drawing.Size(952, 304);
            this.EmployeesTab.TabIndex = 0;
            this.EmployeesTab.Text = "Employees";
            this.EmployeesTab.Enter += new System.EventHandler(this.EmployeesTab_Enter);
            // 
            // EmployeeScheduleButton
            // 
            this.EmployeeScheduleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EmployeeScheduleButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.EmployeeScheduleButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.EmployeeScheduleButton.Depth = 0;
            this.EmployeeScheduleButton.HighEmphasis = true;
            this.EmployeeScheduleButton.Icon = null;
            this.EmployeeScheduleButton.Location = new System.Drawing.Point(598, 8);
            this.EmployeeScheduleButton.Margin = new System.Windows.Forms.Padding(8, 8, 0, 8);
            this.EmployeeScheduleButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.EmployeeScheduleButton.Name = "EmployeeScheduleButton";
            this.EmployeeScheduleButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.EmployeeScheduleButton.Size = new System.Drawing.Size(173, 36);
            this.EmployeeScheduleButton.TabIndex = 27;
            this.EmployeeScheduleButton.Text = "Employee schedule";
            this.EmployeeScheduleButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.EmployeeScheduleButton.UseAccentColor = false;
            this.EmployeeScheduleButton.UseVisualStyleBackColor = true;
            this.EmployeeScheduleButton.Click += new System.EventHandler(this.EmployeeScheduleButton_Click);
            // 
            // FilterEmployee
            // 
            this.FilterEmployee.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterEmployee.AutoResize = false;
            this.FilterEmployee.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.FilterEmployee.Depth = 0;
            this.FilterEmployee.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.FilterEmployee.DropDownHeight = 118;
            this.FilterEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FilterEmployee.DropDownWidth = 121;
            this.FilterEmployee.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.FilterEmployee.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FilterEmployee.FormattingEnabled = true;
            this.FilterEmployee.IntegralHeight = false;
            this.FilterEmployee.ItemHeight = 29;
            this.FilterEmployee.Location = new System.Drawing.Point(437, 9);
            this.FilterEmployee.MaxDropDownItems = 4;
            this.FilterEmployee.MouseState = MaterialSkin.MouseState.OUT;
            this.FilterEmployee.Name = "FilterEmployee";
            this.FilterEmployee.Size = new System.Drawing.Size(150, 35);
            this.FilterEmployee.StartIndex = 0;
            this.FilterEmployee.TabIndex = 26;
            this.FilterEmployee.UseTallSize = false;
            this.FilterEmployee.TextChanged += new System.EventHandler(this.FilterEmployee_TextChanged);
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
            this.SearchBarEmployee.Location = new System.Drawing.Point(8, 9);
            this.SearchBarEmployee.MaxLength = 50;
            this.SearchBarEmployee.MouseState = MaterialSkin.MouseState.OUT;
            this.SearchBarEmployee.Multiline = false;
            this.SearchBarEmployee.Name = "SearchBarEmployee";
            this.SearchBarEmployee.Size = new System.Drawing.Size(423, 36);
            this.SearchBarEmployee.TabIndex = 14;
            this.SearchBarEmployee.Text = "";
            this.SearchBarEmployee.TrailingIcon = null;
            this.SearchBarEmployee.UseTallSize = false;
            this.SearchBarEmployee.TextChanged += new System.EventHandler(this.SearchBarEmployee_TextChanged);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.label7, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(8, 62);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(936, 48);
            this.tableLayoutPanel2.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(709, 9);
            this.label7.Margin = new System.Windows.Forms.Padding(8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(218, 30);
            this.label7.TabIndex = 3;
            this.label7.Text = "Time Out";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(476, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(216, 30);
            this.label4.TabIndex = 2;
            this.label4.Text = "Time In";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(196, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(263, 30);
            this.label5.TabIndex = 1;
            this.label5.Text = "Name";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(9, 9);
            this.label6.Margin = new System.Windows.Forms.Padding(8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(170, 30);
            this.label6.TabIndex = 0;
            this.label6.Text = "ID";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EmployeeListContainer
            // 
            this.EmployeeListContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EmployeeListContainer.AutoScroll = true;
            this.EmployeeListContainer.Controls.Add(this.EmployeeList);
            this.EmployeeListContainer.Location = new System.Drawing.Point(8, 110);
            this.EmployeeListContainer.Margin = new System.Windows.Forms.Padding(0);
            this.EmployeeListContainer.Name = "EmployeeListContainer";
            this.EmployeeListContainer.Size = new System.Drawing.Size(936, 189);
            this.EmployeeListContainer.TabIndex = 12;
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
            this.EmployeeList.Size = new System.Drawing.Size(936, 0);
            this.EmployeeList.TabIndex = 0;
            // 
            // materialDivider2
            // 
            this.materialDivider2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialDivider2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider2.Depth = 0;
            this.materialDivider2.Location = new System.Drawing.Point(8, 52);
            this.materialDivider2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.materialDivider2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider2.Name = "materialDivider2";
            this.materialDivider2.Size = new System.Drawing.Size(936, 2);
            this.materialDivider2.TabIndex = 11;
            this.materialDivider2.Text = "materialDivider2";
            // 
            // AddNewEmployeeButton
            // 
            this.AddNewEmployeeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddNewEmployeeButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AddNewEmployeeButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.AddNewEmployeeButton.Depth = 0;
            this.AddNewEmployeeButton.HighEmphasis = true;
            this.AddNewEmployeeButton.Icon = null;
            this.AddNewEmployeeButton.Location = new System.Drawing.Point(779, 8);
            this.AddNewEmployeeButton.Margin = new System.Windows.Forms.Padding(8);
            this.AddNewEmployeeButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.AddNewEmployeeButton.Name = "AddNewEmployeeButton";
            this.AddNewEmployeeButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.AddNewEmployeeButton.Size = new System.Drawing.Size(165, 36);
            this.AddNewEmployeeButton.TabIndex = 10;
            this.AddNewEmployeeButton.Text = "Add new employee";
            this.AddNewEmployeeButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.AddNewEmployeeButton.UseAccentColor = false;
            this.AddNewEmployeeButton.UseVisualStyleBackColor = true;
            this.AddNewEmployeeButton.Click += new System.EventHandler(this.AddNewEmployeeButton_Click);
            // 
            // CustomersTab
            // 
            this.CustomersTab.BackColor = System.Drawing.Color.White;
            this.CustomersTab.Controls.Add(this.FilterCustomer);
            this.CustomersTab.Controls.Add(this.SearchBarCustomer);
            this.CustomersTab.Controls.Add(this.tableLayoutPanel1);
            this.CustomersTab.Controls.Add(this.CustomerListContainer);
            this.CustomersTab.Controls.Add(this.materialDivider1);
            this.CustomersTab.Controls.Add(this.AddNewCustomerButton);
            this.CustomersTab.Location = new System.Drawing.Point(4, 52);
            this.CustomersTab.Margin = new System.Windows.Forms.Padding(0);
            this.CustomersTab.Name = "CustomersTab";
            this.CustomersTab.Size = new System.Drawing.Size(952, 304);
            this.CustomersTab.TabIndex = 1;
            this.CustomersTab.Text = "Customers";
            this.CustomersTab.Enter += new System.EventHandler(this.CustomersTab_Enter);
            // 
            // FilterCustomer
            // 
            this.FilterCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterCustomer.AutoResize = false;
            this.FilterCustomer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.FilterCustomer.Depth = 0;
            this.FilterCustomer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.FilterCustomer.DropDownHeight = 118;
            this.FilterCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FilterCustomer.DropDownWidth = 121;
            this.FilterCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.FilterCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FilterCustomer.FormattingEnabled = true;
            this.FilterCustomer.IntegralHeight = false;
            this.FilterCustomer.ItemHeight = 29;
            this.FilterCustomer.Location = new System.Drawing.Point(616, 9);
            this.FilterCustomer.MaxDropDownItems = 4;
            this.FilterCustomer.MouseState = MaterialSkin.MouseState.OUT;
            this.FilterCustomer.Name = "FilterCustomer";
            this.FilterCustomer.Size = new System.Drawing.Size(150, 35);
            this.FilterCustomer.StartIndex = 0;
            this.FilterCustomer.TabIndex = 27;
            this.FilterCustomer.UseTallSize = false;
            this.FilterCustomer.TextChanged += new System.EventHandler(this.FilterCustomer_TextChanged);
            // 
            // SearchBarCustomer
            // 
            this.SearchBarCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBarCustomer.AnimateReadOnly = false;
            this.SearchBarCustomer.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SearchBarCustomer.Depth = 0;
            this.SearchBarCustomer.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.SearchBarCustomer.Hint = "Search";
            this.SearchBarCustomer.LeadingIcon = null;
            this.SearchBarCustomer.Location = new System.Drawing.Point(8, 9);
            this.SearchBarCustomer.MaxLength = 50;
            this.SearchBarCustomer.MouseState = MaterialSkin.MouseState.OUT;
            this.SearchBarCustomer.Multiline = false;
            this.SearchBarCustomer.Name = "SearchBarCustomer";
            this.SearchBarCustomer.Size = new System.Drawing.Size(570, 36);
            this.SearchBarCustomer.TabIndex = 15;
            this.SearchBarCustomer.Text = "";
            this.SearchBarCustomer.TrailingIcon = null;
            this.SearchBarCustomer.UseTallSize = false;
            this.SearchBarCustomer.TextChanged += new System.EventHandler(this.SearchBarCustomer_TextChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 42F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 62);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(936, 48);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(551, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(376, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "Owned Vehicle Plate Numbers";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(243, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(291, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CustomerListContainer
            // 
            this.CustomerListContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CustomerListContainer.AutoScroll = true;
            this.CustomerListContainer.Controls.Add(this.CustomerList);
            this.CustomerListContainer.Location = new System.Drawing.Point(8, 110);
            this.CustomerListContainer.Margin = new System.Windows.Forms.Padding(0);
            this.CustomerListContainer.Name = "CustomerListContainer";
            this.CustomerListContainer.Size = new System.Drawing.Size(936, 189);
            this.CustomerListContainer.TabIndex = 8;
            // 
            // CustomerList
            // 
            this.CustomerList.AutoSize = true;
            this.CustomerList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CustomerList.ColumnCount = 1;
            this.CustomerList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.CustomerList.Dock = System.Windows.Forms.DockStyle.Top;
            this.CustomerList.Location = new System.Drawing.Point(0, 0);
            this.CustomerList.Margin = new System.Windows.Forms.Padding(0);
            this.CustomerList.Name = "CustomerList";
            this.CustomerList.RowCount = 1;
            this.CustomerList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.CustomerList.Size = new System.Drawing.Size(936, 0);
            this.CustomerList.TabIndex = 0;
            // 
            // materialDivider1
            // 
            this.materialDivider1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(8, 52);
            this.materialDivider1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(936, 2);
            this.materialDivider1.TabIndex = 7;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // AddNewCustomerButton
            // 
            this.AddNewCustomerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddNewCustomerButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AddNewCustomerButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.AddNewCustomerButton.Depth = 0;
            this.AddNewCustomerButton.HighEmphasis = true;
            this.AddNewCustomerButton.Icon = null;
            this.AddNewCustomerButton.Location = new System.Drawing.Point(777, 8);
            this.AddNewCustomerButton.Margin = new System.Windows.Forms.Padding(8);
            this.AddNewCustomerButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.AddNewCustomerButton.Name = "AddNewCustomerButton";
            this.AddNewCustomerButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.AddNewCustomerButton.Size = new System.Drawing.Size(167, 36);
            this.AddNewCustomerButton.TabIndex = 1;
            this.AddNewCustomerButton.Text = "Add new customer";
            this.AddNewCustomerButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.AddNewCustomerButton.UseAccentColor = false;
            this.AddNewCustomerButton.UseVisualStyleBackColor = true;
            this.AddNewCustomerButton.Click += new System.EventHandler(this.AddNewCustomerButton_Click);
            // 
            // VehiclesTab
            // 
            this.VehiclesTab.BackColor = System.Drawing.Color.White;
            this.VehiclesTab.Controls.Add(this.FilterVehicle);
            this.VehiclesTab.Controls.Add(this.SearchBarVehicle);
            this.VehiclesTab.Controls.Add(this.tableLayoutPanel3);
            this.VehiclesTab.Controls.Add(this.VehicleListContainer);
            this.VehiclesTab.Controls.Add(this.materialDivider3);
            this.VehiclesTab.Controls.Add(this.AddNewVehicleButton);
            this.VehiclesTab.Location = new System.Drawing.Point(4, 52);
            this.VehiclesTab.Margin = new System.Windows.Forms.Padding(0);
            this.VehiclesTab.Name = "VehiclesTab";
            this.VehiclesTab.Size = new System.Drawing.Size(952, 304);
            this.VehiclesTab.TabIndex = 2;
            this.VehiclesTab.Text = "Vehicles";
            this.VehiclesTab.Enter += new System.EventHandler(this.VehiclesTab_Enter);
            // 
            // FilterVehicle
            // 
            this.FilterVehicle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterVehicle.AutoResize = false;
            this.FilterVehicle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.FilterVehicle.Depth = 0;
            this.FilterVehicle.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.FilterVehicle.DropDownHeight = 118;
            this.FilterVehicle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FilterVehicle.DropDownWidth = 121;
            this.FilterVehicle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.FilterVehicle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FilterVehicle.FormattingEnabled = true;
            this.FilterVehicle.IntegralHeight = false;
            this.FilterVehicle.ItemHeight = 29;
            this.FilterVehicle.Location = new System.Drawing.Point(634, 9);
            this.FilterVehicle.MaxDropDownItems = 4;
            this.FilterVehicle.MouseState = MaterialSkin.MouseState.OUT;
            this.FilterVehicle.Name = "FilterVehicle";
            this.FilterVehicle.Size = new System.Drawing.Size(150, 35);
            this.FilterVehicle.StartIndex = 0;
            this.FilterVehicle.TabIndex = 27;
            this.FilterVehicle.UseTallSize = false;
            this.FilterVehicle.TextChanged += new System.EventHandler(this.FilterVehicle_TextChanged);
            // 
            // SearchBarVehicle
            // 
            this.SearchBarVehicle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBarVehicle.AnimateReadOnly = false;
            this.SearchBarVehicle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SearchBarVehicle.Depth = 0;
            this.SearchBarVehicle.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.SearchBarVehicle.Hint = "Search";
            this.SearchBarVehicle.LeadingIcon = null;
            this.SearchBarVehicle.Location = new System.Drawing.Point(8, 9);
            this.SearchBarVehicle.MaxLength = 50;
            this.SearchBarVehicle.MouseState = MaterialSkin.MouseState.OUT;
            this.SearchBarVehicle.Multiline = false;
            this.SearchBarVehicle.Name = "SearchBarVehicle";
            this.SearchBarVehicle.Size = new System.Drawing.Size(570, 36);
            this.SearchBarVehicle.TabIndex = 20;
            this.SearchBarVehicle.Text = "";
            this.SearchBarVehicle.TrailingIcon = null;
            this.SearchBarVehicle.UseTallSize = false;
            this.SearchBarVehicle.TextChanged += new System.EventHandler(this.SearchBarVehicle_TextChanged);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 6;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Controls.Add(this.label13, 5, 0);
            this.tableLayoutPanel3.Controls.Add(this.label12, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.label8, 4, 0);
            this.tableLayoutPanel3.Controls.Add(this.label9, 3, 0);
            this.tableLayoutPanel3.Controls.Add(this.label10, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.label11, 1, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(8, 62);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(936, 48);
            this.tableLayoutPanel3.TabIndex = 19;
            // 
            // label13
            // 
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(753, 9);
            this.label13.Margin = new System.Windows.Forms.Padding(8);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(174, 30);
            this.label13.TabIndex = 5;
            this.label13.Text = "Owner Name";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(9, 9);
            this.label12.Margin = new System.Windows.Forms.Padding(8);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(76, 30);
            this.label12.TabIndex = 4;
            this.label12.Text = "ID";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(567, 9);
            this.label8.Margin = new System.Windows.Forms.Padding(8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(169, 30);
            this.label8.TabIndex = 3;
            this.label8.Text = "Plate Number";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(474, 9);
            this.label9.Margin = new System.Windows.Forms.Padding(8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 30);
            this.label9.TabIndex = 2;
            this.label9.Text = "Size";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(288, 9);
            this.label10.Margin = new System.Windows.Forms.Padding(8);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(169, 30);
            this.label10.TabIndex = 1;
            this.label10.Text = "Model";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(102, 9);
            this.label11.Margin = new System.Windows.Forms.Padding(8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(169, 30);
            this.label11.TabIndex = 0;
            this.label11.Text = "Brand";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // VehicleListContainer
            // 
            this.VehicleListContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VehicleListContainer.AutoScroll = true;
            this.VehicleListContainer.Controls.Add(this.VehicleList);
            this.VehicleListContainer.Location = new System.Drawing.Point(8, 110);
            this.VehicleListContainer.Margin = new System.Windows.Forms.Padding(0);
            this.VehicleListContainer.Name = "VehicleListContainer";
            this.VehicleListContainer.Size = new System.Drawing.Size(936, 189);
            this.VehicleListContainer.TabIndex = 18;
            // 
            // VehicleList
            // 
            this.VehicleList.AutoSize = true;
            this.VehicleList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.VehicleList.ColumnCount = 1;
            this.VehicleList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.VehicleList.Dock = System.Windows.Forms.DockStyle.Top;
            this.VehicleList.Location = new System.Drawing.Point(0, 0);
            this.VehicleList.Margin = new System.Windows.Forms.Padding(0);
            this.VehicleList.Name = "VehicleList";
            this.VehicleList.RowCount = 1;
            this.VehicleList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.VehicleList.Size = new System.Drawing.Size(936, 0);
            this.VehicleList.TabIndex = 0;
            // 
            // materialDivider3
            // 
            this.materialDivider3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialDivider3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider3.Depth = 0;
            this.materialDivider3.Location = new System.Drawing.Point(8, 52);
            this.materialDivider3.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.materialDivider3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider3.Name = "materialDivider3";
            this.materialDivider3.Size = new System.Drawing.Size(936, 2);
            this.materialDivider3.TabIndex = 9;
            this.materialDivider3.Text = "materialDivider3";
            // 
            // AddNewVehicleButton
            // 
            this.AddNewVehicleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddNewVehicleButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AddNewVehicleButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.AddNewVehicleButton.Depth = 0;
            this.AddNewVehicleButton.HighEmphasis = true;
            this.AddNewVehicleButton.Icon = null;
            this.AddNewVehicleButton.Location = new System.Drawing.Point(795, 8);
            this.AddNewVehicleButton.Margin = new System.Windows.Forms.Padding(8);
            this.AddNewVehicleButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.AddNewVehicleButton.Name = "AddNewVehicleButton";
            this.AddNewVehicleButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.AddNewVehicleButton.Size = new System.Drawing.Size(149, 36);
            this.AddNewVehicleButton.TabIndex = 8;
            this.AddNewVehicleButton.Text = "Add new vehicle";
            this.AddNewVehicleButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.AddNewVehicleButton.UseAccentColor = false;
            this.AddNewVehicleButton.UseVisualStyleBackColor = true;
            this.AddNewVehicleButton.Click += new System.EventHandler(this.AddNewVehicleButton_Click);
            // 
            // UserAccountsTab
            // 
            this.UserAccountsTab.BackColor = System.Drawing.Color.White;
            this.UserAccountsTab.Controls.Add(this.FilterUser);
            this.UserAccountsTab.Controls.Add(this.SearchUser);
            this.UserAccountsTab.Controls.Add(this.tableLayoutPanel4);
            this.UserAccountsTab.Controls.Add(this.panel1);
            this.UserAccountsTab.Controls.Add(this.materialDivider4);
            this.UserAccountsTab.Controls.Add(this.AddUserButton);
            this.UserAccountsTab.Location = new System.Drawing.Point(4, 52);
            this.UserAccountsTab.Name = "UserAccountsTab";
            this.UserAccountsTab.Padding = new System.Windows.Forms.Padding(3);
            this.UserAccountsTab.Size = new System.Drawing.Size(952, 304);
            this.UserAccountsTab.TabIndex = 3;
            this.UserAccountsTab.Text = "User Accounts";
            this.UserAccountsTab.Enter += new System.EventHandler(this.UserAccountsTab_Enter);
            // 
            // FilterUser
            // 
            this.FilterUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterUser.AutoResize = false;
            this.FilterUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.FilterUser.Depth = 0;
            this.FilterUser.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.FilterUser.DropDownHeight = 118;
            this.FilterUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FilterUser.DropDownWidth = 121;
            this.FilterUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.FilterUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FilterUser.FormattingEnabled = true;
            this.FilterUser.IntegralHeight = false;
            this.FilterUser.ItemHeight = 29;
            this.FilterUser.Location = new System.Drawing.Point(656, 9);
            this.FilterUser.MaxDropDownItems = 4;
            this.FilterUser.MouseState = MaterialSkin.MouseState.OUT;
            this.FilterUser.Name = "FilterUser";
            this.FilterUser.Size = new System.Drawing.Size(151, 35);
            this.FilterUser.StartIndex = 0;
            this.FilterUser.TabIndex = 33;
            this.FilterUser.UseTallSize = false;
            // 
            // SearchUser
            // 
            this.SearchUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchUser.AnimateReadOnly = false;
            this.SearchUser.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SearchUser.Depth = 0;
            this.SearchUser.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.SearchUser.Hint = "Search";
            this.SearchUser.LeadingIcon = null;
            this.SearchUser.Location = new System.Drawing.Point(8, 9);
            this.SearchUser.MaxLength = 50;
            this.SearchUser.MouseState = MaterialSkin.MouseState.OUT;
            this.SearchUser.Multiline = false;
            this.SearchUser.Name = "SearchUser";
            this.SearchUser.Size = new System.Drawing.Size(570, 36);
            this.SearchUser.TabIndex = 32;
            this.SearchUser.Text = "";
            this.SearchUser.TrailingIcon = null;
            this.SearchUser.UseTallSize = false;
            this.SearchUser.TextChanged += new System.EventHandler(this.SearchUser_TextChanged);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.Controls.Add(this.label15, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.label18, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.label19, 1, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(8, 62);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(936, 48);
            this.tableLayoutPanel4.TabIndex = 31;
            // 
            // label15
            // 
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(9, 9);
            this.label15.Margin = new System.Windows.Forms.Padding(8);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(356, 30);
            this.label15.TabIndex = 4;
            this.label15.Text = "Username";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label18.Location = new System.Drawing.Point(755, 9);
            this.label18.Margin = new System.Windows.Forms.Padding(8);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(172, 30);
            this.label18.TabIndex = 1;
            this.label18.Text = "Type";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label19.Location = new System.Drawing.Point(382, 9);
            this.label19.Margin = new System.Windows.Forms.Padding(8);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(356, 30);
            this.label19.TabIndex = 0;
            this.label19.Text = "Password";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.UserList);
            this.panel1.Location = new System.Drawing.Point(8, 110);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(936, 189);
            this.panel1.TabIndex = 30;
            // 
            // UserList
            // 
            this.UserList.AutoSize = true;
            this.UserList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.UserList.ColumnCount = 1;
            this.UserList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.UserList.Dock = System.Windows.Forms.DockStyle.Top;
            this.UserList.Location = new System.Drawing.Point(0, 0);
            this.UserList.Margin = new System.Windows.Forms.Padding(0);
            this.UserList.Name = "UserList";
            this.UserList.RowCount = 1;
            this.UserList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.UserList.Size = new System.Drawing.Size(936, 0);
            this.UserList.TabIndex = 0;
            // 
            // materialDivider4
            // 
            this.materialDivider4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialDivider4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider4.Depth = 0;
            this.materialDivider4.Location = new System.Drawing.Point(8, 52);
            this.materialDivider4.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.materialDivider4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider4.Name = "materialDivider4";
            this.materialDivider4.Size = new System.Drawing.Size(936, 2);
            this.materialDivider4.TabIndex = 29;
            this.materialDivider4.Text = "materialDivider4";
            // 
            // AddUserButton
            // 
            this.AddUserButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddUserButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AddUserButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.AddUserButton.Depth = 0;
            this.AddUserButton.HighEmphasis = true;
            this.AddUserButton.Icon = null;
            this.AddUserButton.Location = new System.Drawing.Point(818, 8);
            this.AddUserButton.Margin = new System.Windows.Forms.Padding(8);
            this.AddUserButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.AddUserButton.Name = "AddUserButton";
            this.AddUserButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.AddUserButton.Size = new System.Drawing.Size(126, 36);
            this.AddUserButton.TabIndex = 28;
            this.AddUserButton.Text = "Add new user";
            this.AddUserButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.AddUserButton.UseAccentColor = false;
            this.AddUserButton.UseVisualStyleBackColor = true;
            this.AddUserButton.Click += new System.EventHandler(this.AddUserButton_Click);
            // 
            // PeopleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 360);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PeopleForm";
            this.Text = "PeopleForm";
            this.Load += new System.EventHandler(this.PeopleForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.EmployeesTab.ResumeLayout(false);
            this.EmployeesTab.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.EmployeeListContainer.ResumeLayout(false);
            this.EmployeeListContainer.PerformLayout();
            this.CustomersTab.ResumeLayout(false);
            this.CustomersTab.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.CustomerListContainer.ResumeLayout(false);
            this.CustomerListContainer.PerformLayout();
            this.VehiclesTab.ResumeLayout(false);
            this.VehiclesTab.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.VehicleListContainer.ResumeLayout(false);
            this.VehicleListContainer.PerformLayout();
            this.UserAccountsTab.ResumeLayout(false);
            this.UserAccountsTab.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage EmployeesTab;
        private TabPage CustomersTab;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private MaterialSkin.Controls.MaterialButton AddNewCustomerButton;
        private TabPage VehiclesTab;
        private Panel CustomerListContainer;
        private TableLayoutPanel CustomerList;
        private TableLayoutPanel tableLayoutPanel1;
        private Label label3;
        private Label label2;
        private Label label1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label4;
        private Label label5;
        private Label label6;
        private Panel EmployeeListContainer;
        private TableLayoutPanel EmployeeList;
        private MaterialSkin.Controls.MaterialDivider materialDivider2;
        private MaterialSkin.Controls.MaterialButton AddNewEmployeeButton;
        private Label label7;
        private MaterialSkin.Controls.MaterialDivider materialDivider3;
        private MaterialSkin.Controls.MaterialButton AddNewVehicleButton;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Panel VehicleListContainer;
        private TableLayoutPanel VehicleList;
        private Label label12;
        private Label label13;
        private MaterialSkin.Controls.MaterialTextBox SearchBarEmployee;
        private MaterialSkin.Controls.MaterialTextBox SearchBarCustomer;
        private MaterialSkin.Controls.MaterialTextBox SearchBarVehicle;
        private MaterialSkin.Controls.MaterialComboBox FilterEmployee;
        private MaterialSkin.Controls.MaterialComboBox FilterCustomer;
        private MaterialSkin.Controls.MaterialComboBox FilterVehicle;
        private MaterialSkin.Controls.MaterialButton EmployeeScheduleButton;
        private TabPage UserAccountsTab;
        private MaterialSkin.Controls.MaterialComboBox FilterUser;
        private MaterialSkin.Controls.MaterialTextBox SearchUser;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label15;
        private Label label18;
        private Label label19;
        private Panel panel1;
        private TableLayoutPanel UserList;
        private MaterialSkin.Controls.MaterialDivider materialDivider4;
        private MaterialSkin.Controls.MaterialButton AddUserButton;
    }
}