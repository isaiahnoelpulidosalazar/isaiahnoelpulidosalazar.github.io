namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class TransactionDetailDialog
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
            this.PickEmployeeButton = new MaterialSkin.Controls.MaterialButton();
            this.materialLabel3 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.CancelButton = new MaterialSkin.Controls.MaterialButton();
            this.DoneButton = new MaterialSkin.Controls.MaterialButton();
            this.TB_Employee = new MaterialSkin.Controls.MaterialTextBox();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.SelectServicesButton = new MaterialSkin.Controls.MaterialButton();
            this.PackageLabel = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel5 = new MaterialSkin.Controls.MaterialLabel();
            this.TB_Vehicle = new MaterialSkin.Controls.MaterialTextBox();
            this.TB_ID = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel8 = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel9 = new MaterialSkin.Controls.MaterialLabel();
            this.TB_LastUpdated = new MaterialSkin.Controls.MaterialLabel();
            this.TB_DateCreated = new MaterialSkin.Controls.MaterialLabel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ServiceListContainer = new System.Windows.Forms.Panel();
            this.ServiceList = new System.Windows.Forms.TableLayoutPanel();
            this.CB_Packages = new MaterialSkin.Controls.MaterialComboBox();
            this.ExtraListCheckBox = new MaterialSkin.Controls.MaterialCheckedListBox();
            this.materialLabel4 = new MaterialSkin.Controls.MaterialLabel();
            this.tableLayoutPanel2.SuspendLayout();
            this.ServiceListContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // PickEmployeeButton
            // 
            this.PickEmployeeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PickEmployeeButton.AutoSize = false;
            this.PickEmployeeButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PickEmployeeButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.PickEmployeeButton.Depth = 0;
            this.PickEmployeeButton.HighEmphasis = true;
            this.PickEmployeeButton.Icon = null;
            this.PickEmployeeButton.Location = new System.Drawing.Point(446, 165);
            this.PickEmployeeButton.Margin = new System.Windows.Forms.Padding(8);
            this.PickEmployeeButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.PickEmployeeButton.Name = "PickEmployeeButton";
            this.PickEmployeeButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.PickEmployeeButton.Size = new System.Drawing.Size(36, 50);
            this.PickEmployeeButton.TabIndex = 75;
            this.PickEmployeeButton.Text = ">";
            this.PickEmployeeButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.PickEmployeeButton.UseAccentColor = false;
            this.PickEmployeeButton.UseVisualStyleBackColor = true;
            this.PickEmployeeButton.Click += new System.EventHandler(this.PickEmployeeButton_Click);
            // 
            // materialLabel3
            // 
            this.materialLabel3.AutoSize = true;
            this.materialLabel3.Depth = 0;
            this.materialLabel3.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel3.Location = new System.Drawing.Point(12, 180);
            this.materialLabel3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.materialLabel3.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel3.Name = "materialLabel3";
            this.materialLabel3.Size = new System.Drawing.Size(89, 19);
            this.materialLabel3.TabIndex = 71;
            this.materialLabel3.Text = "Employee(s)";
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
            this.materialLabel2.Size = new System.Drawing.Size(58, 19);
            this.materialLabel2.TabIndex = 70;
            this.materialLabel2.Text = "Order ID";
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.CancelButton.Depth = 0;
            this.CancelButton.HighEmphasis = true;
            this.CancelButton.Icon = null;
            this.CancelButton.Location = new System.Drawing.Point(10, 611);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(8);
            this.CancelButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.CancelButton.Size = new System.Drawing.Size(77, 36);
            this.CancelButton.TabIndex = 69;
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
            this.DoneButton.Location = new System.Drawing.Point(694, 611);
            this.DoneButton.Margin = new System.Windows.Forms.Padding(8);
            this.DoneButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.DoneButton.Size = new System.Drawing.Size(64, 36);
            this.DoneButton.TabIndex = 68;
            this.DoneButton.Text = "Done";
            this.DoneButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.DoneButton.UseAccentColor = false;
            this.DoneButton.UseVisualStyleBackColor = true;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // TB_Employee
            // 
            this.TB_Employee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Employee.AnimateReadOnly = false;
            this.TB_Employee.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_Employee.Depth = 0;
            this.TB_Employee.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TB_Employee.LeadingIcon = null;
            this.TB_Employee.Location = new System.Drawing.Point(176, 165);
            this.TB_Employee.Margin = new System.Windows.Forms.Padding(8);
            this.TB_Employee.MaxLength = 50;
            this.TB_Employee.MouseState = MaterialSkin.MouseState.OUT;
            this.TB_Employee.Multiline = false;
            this.TB_Employee.Name = "TB_Employee";
            this.TB_Employee.Size = new System.Drawing.Size(254, 50);
            this.TB_Employee.TabIndex = 61;
            this.TB_Employee.Text = "";
            this.TB_Employee.TrailingIcon = null;
            this.TB_Employee.Click += new System.EventHandler(this.TB_Employee_Click);
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
            this.materialLabel1.Size = new System.Drawing.Size(248, 29);
            this.materialLabel1.TabIndex = 59;
            this.materialLabel1.Text = "Edit transaction details";
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
            this.materialDivider1.Size = new System.Drawing.Size(748, 2);
            this.materialDivider1.TabIndex = 58;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // SelectServicesButton
            // 
            this.SelectServicesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SelectServicesButton.AutoSize = false;
            this.SelectServicesButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SelectServicesButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.SelectServicesButton.Depth = 0;
            this.SelectServicesButton.HighEmphasis = true;
            this.SelectServicesButton.Icon = null;
            this.SelectServicesButton.Location = new System.Drawing.Point(176, 231);
            this.SelectServicesButton.Margin = new System.Windows.Forms.Padding(8);
            this.SelectServicesButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.SelectServicesButton.Name = "SelectServicesButton";
            this.SelectServicesButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.SelectServicesButton.Size = new System.Drawing.Size(127, 49);
            this.SelectServicesButton.TabIndex = 78;
            this.SelectServicesButton.Text = "Select services";
            this.SelectServicesButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.SelectServicesButton.UseAccentColor = false;
            this.SelectServicesButton.UseVisualStyleBackColor = true;
            this.SelectServicesButton.Click += new System.EventHandler(this.SelectServicesButton_Click);
            // 
            // PackageLabel
            // 
            this.PackageLabel.AutoSize = true;
            this.PackageLabel.Depth = 0;
            this.PackageLabel.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.PackageLabel.Location = new System.Drawing.Point(12, 246);
            this.PackageLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PackageLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.PackageLabel.Name = "PackageLabel";
            this.PackageLabel.Size = new System.Drawing.Size(154, 19);
            this.PackageLabel.TabIndex = 77;
            this.PackageLabel.Text = "Pick Service/Package";
            // 
            // materialLabel5
            // 
            this.materialLabel5.AutoSize = true;
            this.materialLabel5.Depth = 0;
            this.materialLabel5.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel5.Location = new System.Drawing.Point(12, 311);
            this.materialLabel5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.materialLabel5.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel5.Name = "materialLabel5";
            this.materialLabel5.Size = new System.Drawing.Size(97, 19);
            this.materialLabel5.TabIndex = 80;
            this.materialLabel5.Text = "Plate Number";
            // 
            // TB_Vehicle
            // 
            this.TB_Vehicle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Vehicle.AnimateReadOnly = false;
            this.TB_Vehicle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_Vehicle.Depth = 0;
            this.TB_Vehicle.Enabled = false;
            this.TB_Vehicle.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TB_Vehicle.LeadingIcon = null;
            this.TB_Vehicle.Location = new System.Drawing.Point(176, 296);
            this.TB_Vehicle.Margin = new System.Windows.Forms.Padding(8);
            this.TB_Vehicle.MaxLength = 50;
            this.TB_Vehicle.MouseState = MaterialSkin.MouseState.OUT;
            this.TB_Vehicle.Multiline = false;
            this.TB_Vehicle.Name = "TB_Vehicle";
            this.TB_Vehicle.Size = new System.Drawing.Size(306, 50);
            this.TB_Vehicle.TabIndex = 79;
            this.TB_Vehicle.Text = "";
            this.TB_Vehicle.TrailingIcon = null;
            this.TB_Vehicle.Click += new System.EventHandler(this.TB_Vehicle_Click);
            // 
            // TB_ID
            // 
            this.TB_ID.AutoSize = true;
            this.TB_ID.Depth = 0;
            this.TB_ID.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TB_ID.Location = new System.Drawing.Point(173, 114);
            this.TB_ID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TB_ID.MouseState = MaterialSkin.MouseState.HOVER;
            this.TB_ID.Name = "TB_ID";
            this.TB_ID.Size = new System.Drawing.Size(58, 19);
            this.TB_ID.TabIndex = 82;
            this.TB_ID.Text = "Order ID";
            // 
            // materialLabel8
            // 
            this.materialLabel8.AutoSize = true;
            this.materialLabel8.Depth = 0;
            this.materialLabel8.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel8.Location = new System.Drawing.Point(12, 501);
            this.materialLabel8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.materialLabel8.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel8.Name = "materialLabel8";
            this.materialLabel8.Size = new System.Drawing.Size(95, 19);
            this.materialLabel8.TabIndex = 86;
            this.materialLabel8.Text = "Last Updated";
            // 
            // materialLabel9
            // 
            this.materialLabel9.AutoSize = true;
            this.materialLabel9.Depth = 0;
            this.materialLabel9.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel9.Location = new System.Drawing.Point(12, 545);
            this.materialLabel9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.materialLabel9.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel9.Name = "materialLabel9";
            this.materialLabel9.Size = new System.Drawing.Size(92, 19);
            this.materialLabel9.TabIndex = 89;
            this.materialLabel9.Text = "Date Created";
            // 
            // TB_LastUpdated
            // 
            this.TB_LastUpdated.AutoSize = true;
            this.TB_LastUpdated.Depth = 0;
            this.TB_LastUpdated.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TB_LastUpdated.Location = new System.Drawing.Point(173, 501);
            this.TB_LastUpdated.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TB_LastUpdated.MouseState = MaterialSkin.MouseState.HOVER;
            this.TB_LastUpdated.Name = "TB_LastUpdated";
            this.TB_LastUpdated.Size = new System.Drawing.Size(100, 19);
            this.TB_LastUpdated.TabIndex = 91;
            this.TB_LastUpdated.Text = "Vehicle Model";
            // 
            // TB_DateCreated
            // 
            this.TB_DateCreated.AutoSize = true;
            this.TB_DateCreated.Depth = 0;
            this.TB_DateCreated.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.TB_DateCreated.Location = new System.Drawing.Point(173, 545);
            this.TB_DateCreated.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TB_DateCreated.MouseState = MaterialSkin.MouseState.HOVER;
            this.TB_DateCreated.Name = "TB_DateCreated";
            this.TB_DateCreated.Size = new System.Drawing.Size(100, 19);
            this.TB_DateCreated.TabIndex = 92;
            this.TB_DateCreated.Text = "Vehicle Model";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel2.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(490, 91);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(268, 48);
            this.tableLayoutPanel2.TabIndex = 94;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(182, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 30);
            this.label5.TabIndex = 1;
            this.label5.Text = "Price";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(9, 9);
            this.label6.Margin = new System.Windows.Forms.Padding(8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(156, 30);
            this.label6.TabIndex = 0;
            this.label6.Text = "Service Name";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ServiceListContainer
            // 
            this.ServiceListContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ServiceListContainer.AutoScroll = true;
            this.ServiceListContainer.Controls.Add(this.ServiceList);
            this.ServiceListContainer.Location = new System.Drawing.Point(490, 139);
            this.ServiceListContainer.Margin = new System.Windows.Forms.Padding(0);
            this.ServiceListContainer.Name = "ServiceListContainer";
            this.ServiceListContainer.Size = new System.Drawing.Size(268, 444);
            this.ServiceListContainer.TabIndex = 93;
            // 
            // ServiceList
            // 
            this.ServiceList.AutoSize = true;
            this.ServiceList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ServiceList.ColumnCount = 1;
            this.ServiceList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ServiceList.Dock = System.Windows.Forms.DockStyle.Top;
            this.ServiceList.Location = new System.Drawing.Point(0, 0);
            this.ServiceList.Margin = new System.Windows.Forms.Padding(0);
            this.ServiceList.Name = "ServiceList";
            this.ServiceList.RowCount = 1;
            this.ServiceList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ServiceList.Size = new System.Drawing.Size(268, 0);
            this.ServiceList.TabIndex = 0;
            // 
            // CB_Packages
            // 
            this.CB_Packages.AutoResize = false;
            this.CB_Packages.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.CB_Packages.Depth = 0;
            this.CB_Packages.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.CB_Packages.DropDownHeight = 174;
            this.CB_Packages.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Packages.DropDownWidth = 121;
            this.CB_Packages.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.CB_Packages.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.CB_Packages.FormattingEnabled = true;
            this.CB_Packages.IntegralHeight = false;
            this.CB_Packages.ItemHeight = 43;
            this.CB_Packages.Location = new System.Drawing.Point(319, 231);
            this.CB_Packages.Margin = new System.Windows.Forms.Padding(8);
            this.CB_Packages.MaxDropDownItems = 4;
            this.CB_Packages.MouseState = MaterialSkin.MouseState.OUT;
            this.CB_Packages.Name = "CB_Packages";
            this.CB_Packages.Size = new System.Drawing.Size(163, 49);
            this.CB_Packages.StartIndex = 0;
            this.CB_Packages.TabIndex = 98;
            this.CB_Packages.TextChanged += new System.EventHandler(this.CB_Packages_TextChanged);
            // 
            // ExtraListCheckBox
            // 
            this.ExtraListCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ExtraListCheckBox.AutoScroll = true;
            this.ExtraListCheckBox.BackColor = System.Drawing.SystemColors.Control;
            this.ExtraListCheckBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ExtraListCheckBox.Depth = 0;
            this.ExtraListCheckBox.Location = new System.Drawing.Point(176, 354);
            this.ExtraListCheckBox.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.ExtraListCheckBox.MouseState = MaterialSkin.MouseState.HOVER;
            this.ExtraListCheckBox.Name = "ExtraListCheckBox";
            this.ExtraListCheckBox.Size = new System.Drawing.Size(306, 122);
            this.ExtraListCheckBox.Striped = false;
            this.ExtraListCheckBox.StripeDarkColor = System.Drawing.Color.Empty;
            this.ExtraListCheckBox.TabIndex = 100;
            // 
            // materialLabel4
            // 
            this.materialLabel4.AutoSize = true;
            this.materialLabel4.Depth = 0;
            this.materialLabel4.Font = new System.Drawing.Font("Roboto", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel4.Location = new System.Drawing.Point(12, 367);
            this.materialLabel4.Margin = new System.Windows.Forms.Padding(8);
            this.materialLabel4.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel4.Name = "materialLabel4";
            this.materialLabel4.Size = new System.Drawing.Size(92, 19);
            this.materialLabel4.TabIndex = 99;
            this.materialLabel4.Text = "Select Extras";
            // 
            // EditTransactionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 657);
            this.Controls.Add(this.ExtraListCheckBox);
            this.Controls.Add(this.materialLabel4);
            this.Controls.Add(this.SelectServicesButton);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.ServiceListContainer);
            this.Controls.Add(this.TB_DateCreated);
            this.Controls.Add(this.TB_LastUpdated);
            this.Controls.Add(this.materialLabel9);
            this.Controls.Add(this.materialLabel8);
            this.Controls.Add(this.TB_ID);
            this.Controls.Add(this.materialLabel5);
            this.Controls.Add(this.TB_Vehicle);
            this.Controls.Add(this.PackageLabel);
            this.Controls.Add(this.PickEmployeeButton);
            this.Controls.Add(this.materialLabel3);
            this.Controls.Add(this.materialLabel2);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.TB_Employee);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.materialDivider1);
            this.Controls.Add(this.CB_Packages);
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None;
            this.Name = "EditTransactionDialog";
            this.Padding = new System.Windows.Forms.Padding(2, 20, 2, 2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditTransactionDialog";
            this.Load += new System.EventHandler(this.EditTransactionDialog_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ServiceListContainer.ResumeLayout(false);
            this.ServiceListContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialButton PickEmployeeButton;
        private MaterialSkin.Controls.MaterialLabel materialLabel3;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
        private MaterialSkin.Controls.MaterialButton CancelButton;
        private MaterialSkin.Controls.MaterialButton DoneButton;
        private MaterialSkin.Controls.MaterialTextBox TB_Employee;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private MaterialSkin.Controls.MaterialButton SelectServicesButton;
        private MaterialSkin.Controls.MaterialLabel PackageLabel;
        private MaterialSkin.Controls.MaterialLabel materialLabel5;
        private MaterialSkin.Controls.MaterialTextBox TB_Vehicle;
        private MaterialSkin.Controls.MaterialLabel TB_ID;
        private MaterialSkin.Controls.MaterialLabel materialLabel8;
        private MaterialSkin.Controls.MaterialLabel materialLabel9;
        private MaterialSkin.Controls.MaterialLabel TB_LastUpdated;
        private MaterialSkin.Controls.MaterialLabel TB_DateCreated;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel ServiceListContainer;
        private System.Windows.Forms.TableLayoutPanel ServiceList;
        private MaterialSkin.Controls.MaterialComboBox CB_Packages;
        private MaterialSkin.Controls.MaterialCheckedListBox ExtraListCheckBox;
        private MaterialSkin.Controls.MaterialLabel materialLabel4;
    }
}