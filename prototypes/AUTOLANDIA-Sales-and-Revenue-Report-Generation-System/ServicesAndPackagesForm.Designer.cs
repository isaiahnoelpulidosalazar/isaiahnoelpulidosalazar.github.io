namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class ServicesAndPackagesForm
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
            this.ServicesTab = new System.Windows.Forms.TabPage();
            this.FilterService = new MaterialSkin.Controls.MaterialComboBox();
            this.SearchBarService = new MaterialSkin.Controls.MaterialTextBox();
            this.materialDivider3 = new MaterialSkin.Controls.MaterialDivider();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.ServiceListContainer = new System.Windows.Forms.Panel();
            this.ServiceList = new System.Windows.Forms.TableLayoutPanel();
            this.PackagesTab = new System.Windows.Forms.TabPage();
            this.FilterPackage = new MaterialSkin.Controls.MaterialComboBox();
            this.SearchBarPackage = new MaterialSkin.Controls.MaterialTextBox();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.PackageListContainer = new System.Windows.Forms.Panel();
            this.PackageList = new System.Windows.Forms.TableLayoutPanel();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.ServicesTab.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.ServiceListContainer.SuspendLayout();
            this.PackagesTab.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.PackageListContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.ServicesTab);
            this.tabControl1.Controls.Add(this.PackagesTab);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(96, 48);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(640, 360);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 1;
            // 
            // ServicesTab
            // 
            this.ServicesTab.BackColor = System.Drawing.Color.White;
            this.ServicesTab.Controls.Add(this.FilterService);
            this.ServicesTab.Controls.Add(this.SearchBarService);
            this.ServicesTab.Controls.Add(this.materialDivider3);
            this.ServicesTab.Controls.Add(this.tableLayoutPanel2);
            this.ServicesTab.Controls.Add(this.ServiceListContainer);
            this.ServicesTab.Location = new System.Drawing.Point(4, 52);
            this.ServicesTab.Margin = new System.Windows.Forms.Padding(0);
            this.ServicesTab.Name = "ServicesTab";
            this.ServicesTab.Size = new System.Drawing.Size(632, 304);
            this.ServicesTab.TabIndex = 0;
            this.ServicesTab.Text = "Services";
            this.ServicesTab.Enter += new System.EventHandler(this.ServicesTab_Enter);
            // 
            // FilterService
            // 
            this.FilterService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterService.AutoResize = false;
            this.FilterService.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.FilterService.Depth = 0;
            this.FilterService.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.FilterService.DropDownHeight = 118;
            this.FilterService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FilterService.DropDownWidth = 121;
            this.FilterService.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.FilterService.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FilterService.FormattingEnabled = true;
            this.FilterService.IntegralHeight = false;
            this.FilterService.ItemHeight = 29;
            this.FilterService.Location = new System.Drawing.Point(474, 8);
            this.FilterService.MaxDropDownItems = 4;
            this.FilterService.MouseState = MaterialSkin.MouseState.OUT;
            this.FilterService.Name = "FilterService";
            this.FilterService.Size = new System.Drawing.Size(150, 35);
            this.FilterService.StartIndex = 0;
            this.FilterService.TabIndex = 25;
            this.FilterService.UseTallSize = false;
            this.FilterService.TextChanged += new System.EventHandler(this.FilterService_TextChanged);
            // 
            // SearchBarService
            // 
            this.SearchBarService.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBarService.AnimateReadOnly = false;
            this.SearchBarService.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SearchBarService.Depth = 0;
            this.SearchBarService.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.SearchBarService.Hint = "Search";
            this.SearchBarService.LeadingIcon = null;
            this.SearchBarService.Location = new System.Drawing.Point(8, 8);
            this.SearchBarService.MaxLength = 50;
            this.SearchBarService.MouseState = MaterialSkin.MouseState.OUT;
            this.SearchBarService.Multiline = false;
            this.SearchBarService.Name = "SearchBarService";
            this.SearchBarService.Size = new System.Drawing.Size(360, 36);
            this.SearchBarService.TabIndex = 23;
            this.SearchBarService.Text = "";
            this.SearchBarService.TrailingIcon = null;
            this.SearchBarService.UseTallSize = false;
            this.SearchBarService.TextChanged += new System.EventHandler(this.SearchBarService_TextChanged);
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
            this.materialDivider3.Size = new System.Drawing.Size(616, 2);
            this.materialDivider3.TabIndex = 22;
            this.materialDivider3.Text = "materialDivider3";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 8;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.Controls.Add(this.label15, 7, 0);
            this.tableLayoutPanel2.Controls.Add(this.label4, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label7, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.label10, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.label11, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.label12, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.label14, 6, 0);
            this.tableLayoutPanel2.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(8, 62);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(616, 48);
            this.tableLayoutPanel2.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(9, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 30);
            this.label5.TabIndex = 1;
            this.label5.Text = "ID";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ServiceListContainer
            // 
            this.ServiceListContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ServiceListContainer.AutoScroll = true;
            this.ServiceListContainer.Controls.Add(this.ServiceList);
            this.ServiceListContainer.Location = new System.Drawing.Point(8, 110);
            this.ServiceListContainer.Margin = new System.Windows.Forms.Padding(0);
            this.ServiceListContainer.Name = "ServiceListContainer";
            this.ServiceListContainer.Size = new System.Drawing.Size(616, 189);
            this.ServiceListContainer.TabIndex = 12;
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
            this.ServiceList.Size = new System.Drawing.Size(616, 0);
            this.ServiceList.TabIndex = 0;
            // 
            // PackagesTab
            // 
            this.PackagesTab.BackColor = System.Drawing.Color.White;
            this.PackagesTab.Controls.Add(this.FilterPackage);
            this.PackagesTab.Controls.Add(this.SearchBarPackage);
            this.PackagesTab.Controls.Add(this.materialDivider1);
            this.PackagesTab.Controls.Add(this.tableLayoutPanel1);
            this.PackagesTab.Controls.Add(this.PackageListContainer);
            this.PackagesTab.Location = new System.Drawing.Point(4, 52);
            this.PackagesTab.Margin = new System.Windows.Forms.Padding(0);
            this.PackagesTab.Name = "PackagesTab";
            this.PackagesTab.Size = new System.Drawing.Size(632, 304);
            this.PackagesTab.TabIndex = 1;
            this.PackagesTab.Text = "Packages";
            this.PackagesTab.Enter += new System.EventHandler(this.PackagesTab_Enter);
            // 
            // FilterPackage
            // 
            this.FilterPackage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterPackage.AutoResize = false;
            this.FilterPackage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.FilterPackage.Depth = 0;
            this.FilterPackage.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.FilterPackage.DropDownHeight = 118;
            this.FilterPackage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FilterPackage.DropDownWidth = 121;
            this.FilterPackage.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.FilterPackage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.FilterPackage.FormattingEnabled = true;
            this.FilterPackage.IntegralHeight = false;
            this.FilterPackage.ItemHeight = 29;
            this.FilterPackage.Location = new System.Drawing.Point(474, 8);
            this.FilterPackage.MaxDropDownItems = 4;
            this.FilterPackage.MouseState = MaterialSkin.MouseState.OUT;
            this.FilterPackage.Name = "FilterPackage";
            this.FilterPackage.Size = new System.Drawing.Size(150, 35);
            this.FilterPackage.StartIndex = 0;
            this.FilterPackage.TabIndex = 24;
            this.FilterPackage.UseTallSize = false;
            this.FilterPackage.TextChanged += new System.EventHandler(this.FilterPackage_TextChanged);
            // 
            // SearchBarPackage
            // 
            this.SearchBarPackage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBarPackage.AnimateReadOnly = false;
            this.SearchBarPackage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SearchBarPackage.Depth = 0;
            this.SearchBarPackage.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.SearchBarPackage.Hint = "Search";
            this.SearchBarPackage.LeadingIcon = null;
            this.SearchBarPackage.Location = new System.Drawing.Point(8, 8);
            this.SearchBarPackage.MaxLength = 50;
            this.SearchBarPackage.MouseState = MaterialSkin.MouseState.OUT;
            this.SearchBarPackage.Multiline = false;
            this.SearchBarPackage.Name = "SearchBarPackage";
            this.SearchBarPackage.Size = new System.Drawing.Size(360, 36);
            this.SearchBarPackage.TabIndex = 23;
            this.SearchBarPackage.Text = "";
            this.SearchBarPackage.TrailingIcon = null;
            this.SearchBarPackage.UseTallSize = false;
            this.SearchBarPackage.TextChanged += new System.EventHandler(this.SearchBarPackage_TextChanged);
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
            this.materialDivider1.Size = new System.Drawing.Size(616, 2);
            this.materialDivider1.TabIndex = 22;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 9;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 8F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel1.Controls.Add(this.label17, 8, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label8, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.label9, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 5, 0);
            this.tableLayoutPanel1.Controls.Add(this.label13, 6, 0);
            this.tableLayoutPanel1.Controls.Add(this.label16, 7, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 62);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(616, 48);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // PackageListContainer
            // 
            this.PackageListContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PackageListContainer.AutoScroll = true;
            this.PackageListContainer.Controls.Add(this.PackageList);
            this.PackageListContainer.Location = new System.Drawing.Point(8, 110);
            this.PackageListContainer.Margin = new System.Windows.Forms.Padding(0);
            this.PackageListContainer.Name = "PackageListContainer";
            this.PackageListContainer.Size = new System.Drawing.Size(616, 189);
            this.PackageListContainer.TabIndex = 8;
            // 
            // PackageList
            // 
            this.PackageList.AutoSize = true;
            this.PackageList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PackageList.ColumnCount = 1;
            this.PackageList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PackageList.Dock = System.Windows.Forms.DockStyle.Top;
            this.PackageList.Location = new System.Drawing.Point(0, 0);
            this.PackageList.Margin = new System.Windows.Forms.Padding(0);
            this.PackageList.Name = "PackageList";
            this.PackageList.RowCount = 1;
            this.PackageList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.PackageList.Size = new System.Drawing.Size(616, 0);
            this.PackageList.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Location = new System.Drawing.Point(466, 9);
            this.label14.Margin = new System.Windows.Forms.Padding(8);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(44, 30);
            this.label14.TabIndex = 8;
            this.label14.Text = "XXL";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Location = new System.Drawing.Point(405, 9);
            this.label12.Margin = new System.Windows.Forms.Padding(8);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(44, 30);
            this.label12.TabIndex = 9;
            this.label12.Text = "XL";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Location = new System.Drawing.Point(344, 9);
            this.label11.Margin = new System.Windows.Forms.Padding(8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(44, 30);
            this.label11.TabIndex = 10;
            this.label11.Text = "L";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Location = new System.Drawing.Point(283, 9);
            this.label10.Margin = new System.Windows.Forms.Padding(8);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 30);
            this.label10.TabIndex = 11;
            this.label10.Text = "M";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label7.Location = new System.Drawing.Point(222, 9);
            this.label7.Margin = new System.Windows.Forms.Padding(8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 30);
            this.label7.TabIndex = 12;
            this.label7.Text = "S";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(70, 9);
            this.label4.Margin = new System.Windows.Forms.Padding(8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 30);
            this.label4.TabIndex = 13;
            this.label4.Text = "Name";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Location = new System.Drawing.Point(527, 9);
            this.label15.Margin = new System.Windows.Forms.Padding(8);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 30);
            this.label15.TabIndex = 14;
            this.label15.Text = "Status";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Location = new System.Drawing.Point(479, 9);
            this.label16.Margin = new System.Windows.Forms.Padding(8);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(32, 30);
            this.label16.TabIndex = 7;
            this.label16.Text = "XXL";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label13.Location = new System.Drawing.Point(430, 9);
            this.label13.Margin = new System.Windows.Forms.Padding(8);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(32, 30);
            this.label13.TabIndex = 8;
            this.label13.Text = "XL";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(381, 9);
            this.label6.Margin = new System.Windows.Forms.Padding(8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 30);
            this.label6.TabIndex = 9;
            this.label6.Text = "L";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Location = new System.Drawing.Point(332, 9);
            this.label9.Margin = new System.Windows.Forms.Padding(8);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 30);
            this.label9.TabIndex = 10;
            this.label9.Text = "M";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label8.Location = new System.Drawing.Point(283, 9);
            this.label8.Margin = new System.Windows.Forms.Padding(8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(32, 30);
            this.label8.TabIndex = 11;
            this.label8.Text = "S";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(161, 9);
            this.label3.Margin = new System.Windows.Forms.Padding(8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 30);
            this.label3.TabIndex = 12;
            this.label3.Text = "Services Included";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(70, 9);
            this.label2.Margin = new System.Windows.Forms.Padding(8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 30);
            this.label2.TabIndex = 13;
            this.label2.Text = "Name";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 30);
            this.label1.TabIndex = 14;
            this.label1.Text = "ID";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Location = new System.Drawing.Point(528, 9);
            this.label17.Margin = new System.Windows.Forms.Padding(8);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(79, 30);
            this.label17.TabIndex = 15;
            this.label17.Text = "Status";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ServicesAndPackagesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 360);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ServicesAndPackagesForm";
            this.Text = "ServicesAndPackagesForm";
            this.Load += new System.EventHandler(this.ServicesAndPackagesForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.ServicesTab.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ServiceListContainer.ResumeLayout(false);
            this.ServiceListContainer.PerformLayout();
            this.PackagesTab.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.PackageListContainer.ResumeLayout(false);
            this.PackageListContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage ServicesTab;
        private System.Windows.Forms.TabPage PackagesTab;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel PackageListContainer;
        private System.Windows.Forms.TableLayoutPanel PackageList;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel ServiceListContainer;
        private System.Windows.Forms.TableLayoutPanel ServiceList;
        private MaterialSkin.Controls.MaterialTextBox SearchBarService;
        private MaterialSkin.Controls.MaterialDivider materialDivider3;
        private MaterialSkin.Controls.MaterialTextBox SearchBarPackage;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private MaterialSkin.Controls.MaterialComboBox FilterPackage;
        private MaterialSkin.Controls.MaterialComboBox FilterService;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label16;
    }
}