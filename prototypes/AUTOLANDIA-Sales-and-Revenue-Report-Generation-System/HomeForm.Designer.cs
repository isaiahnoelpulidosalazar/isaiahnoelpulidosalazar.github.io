namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class HomeForm
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
            this.GreetingLabel = new MaterialSkin.Controls.MaterialLabel();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.QuickSalary = new MaterialSkin.Controls.MaterialButton();
            this.QuickOrder = new MaterialSkin.Controls.MaterialButton();
            this.QuickTimeSchedule = new MaterialSkin.Controls.MaterialButton();
            this.QuickVehicle = new MaterialSkin.Controls.MaterialButton();
            this.QuickCustomer = new MaterialSkin.Controls.MaterialButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GreetingLabel
            // 
            this.GreetingLabel.AutoSize = true;
            this.GreetingLabel.Depth = 0;
            this.GreetingLabel.Font = new System.Drawing.Font("Roboto", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.GreetingLabel.FontType = MaterialSkin.MaterialSkinManager.fontType.H5;
            this.GreetingLabel.Location = new System.Drawing.Point(17, 17);
            this.GreetingLabel.Margin = new System.Windows.Forms.Padding(8);
            this.GreetingLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.GreetingLabel.Name = "GreetingLabel";
            this.GreetingLabel.Size = new System.Drawing.Size(109, 29);
            this.GreetingLabel.TabIndex = 5;
            this.GreetingLabel.Text = "Good day!";
            // 
            // materialDivider1
            // 
            this.materialDivider1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(17, 62);
            this.materialDivider1.Margin = new System.Windows.Forms.Padding(8);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(606, 2);
            this.materialDivider1.TabIndex = 4;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Controls.Add(this.QuickSalary, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.QuickOrder, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.QuickTimeSchedule, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.QuickVehicle, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.QuickCustomer, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(17, 80);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(8);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(606, 160);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // QuickSalary
            // 
            this.QuickSalary.AutoSize = false;
            this.QuickSalary.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.QuickSalary.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.QuickSalary.Depth = 0;
            this.QuickSalary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QuickSalary.HighEmphasis = true;
            this.QuickSalary.Icon = null;
            this.QuickSalary.Location = new System.Drawing.Point(205, 84);
            this.QuickSalary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 0);
            this.QuickSalary.MouseState = MaterialSkin.MouseState.HOVER;
            this.QuickSalary.Name = "QuickSalary";
            this.QuickSalary.NoAccentTextColor = System.Drawing.Color.Empty;
            this.QuickSalary.Size = new System.Drawing.Size(193, 76);
            this.QuickSalary.TabIndex = 4;
            this.QuickSalary.Text = "Quick Salary Calculation";
            this.QuickSalary.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.QuickSalary.UseAccentColor = false;
            this.QuickSalary.UseVisualStyleBackColor = true;
            this.QuickSalary.Click += new System.EventHandler(this.QuickSalary_Click);
            // 
            // QuickOrder
            // 
            this.QuickOrder.AutoSize = false;
            this.QuickOrder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.QuickOrder.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.QuickOrder.Depth = 0;
            this.QuickOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QuickOrder.HighEmphasis = true;
            this.QuickOrder.Icon = null;
            this.QuickOrder.Location = new System.Drawing.Point(0, 84);
            this.QuickOrder.Margin = new System.Windows.Forms.Padding(0, 4, 4, 0);
            this.QuickOrder.MouseState = MaterialSkin.MouseState.HOVER;
            this.QuickOrder.Name = "QuickOrder";
            this.QuickOrder.NoAccentTextColor = System.Drawing.Color.Empty;
            this.QuickOrder.Size = new System.Drawing.Size(197, 76);
            this.QuickOrder.TabIndex = 3;
            this.QuickOrder.Text = "Quick Order";
            this.QuickOrder.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.QuickOrder.UseAccentColor = false;
            this.QuickOrder.UseVisualStyleBackColor = true;
            this.QuickOrder.Click += new System.EventHandler(this.QuickOrder_Click);
            // 
            // QuickTimeSchedule
            // 
            this.QuickTimeSchedule.AutoSize = false;
            this.QuickTimeSchedule.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.QuickTimeSchedule.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.QuickTimeSchedule.Depth = 0;
            this.QuickTimeSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QuickTimeSchedule.HighEmphasis = true;
            this.QuickTimeSchedule.Icon = null;
            this.QuickTimeSchedule.Location = new System.Drawing.Point(406, 0);
            this.QuickTimeSchedule.Margin = new System.Windows.Forms.Padding(4, 0, 0, 4);
            this.QuickTimeSchedule.MouseState = MaterialSkin.MouseState.HOVER;
            this.QuickTimeSchedule.Name = "QuickTimeSchedule";
            this.QuickTimeSchedule.NoAccentTextColor = System.Drawing.Color.Empty;
            this.QuickTimeSchedule.Size = new System.Drawing.Size(200, 76);
            this.QuickTimeSchedule.TabIndex = 2;
            this.QuickTimeSchedule.Text = "Employee Time Schedule";
            this.QuickTimeSchedule.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.QuickTimeSchedule.UseAccentColor = false;
            this.QuickTimeSchedule.UseVisualStyleBackColor = true;
            this.QuickTimeSchedule.Click += new System.EventHandler(this.QuickTimeSchedule_Click);
            // 
            // QuickVehicle
            // 
            this.QuickVehicle.AutoSize = false;
            this.QuickVehicle.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.QuickVehicle.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.QuickVehicle.Depth = 0;
            this.QuickVehicle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QuickVehicle.HighEmphasis = true;
            this.QuickVehicle.Icon = null;
            this.QuickVehicle.Location = new System.Drawing.Point(205, 0);
            this.QuickVehicle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 4);
            this.QuickVehicle.MouseState = MaterialSkin.MouseState.HOVER;
            this.QuickVehicle.Name = "QuickVehicle";
            this.QuickVehicle.NoAccentTextColor = System.Drawing.Color.Empty;
            this.QuickVehicle.Size = new System.Drawing.Size(193, 76);
            this.QuickVehicle.TabIndex = 1;
            this.QuickVehicle.Text = "Add a vehicle";
            this.QuickVehicle.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.QuickVehicle.UseAccentColor = false;
            this.QuickVehicle.UseVisualStyleBackColor = true;
            this.QuickVehicle.Click += new System.EventHandler(this.QuickVehicle_Click);
            // 
            // QuickCustomer
            // 
            this.QuickCustomer.AutoSize = false;
            this.QuickCustomer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.QuickCustomer.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.QuickCustomer.Depth = 0;
            this.QuickCustomer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.QuickCustomer.HighEmphasis = true;
            this.QuickCustomer.Icon = null;
            this.QuickCustomer.Location = new System.Drawing.Point(0, 0);
            this.QuickCustomer.Margin = new System.Windows.Forms.Padding(0, 0, 4, 4);
            this.QuickCustomer.MouseState = MaterialSkin.MouseState.HOVER;
            this.QuickCustomer.Name = "QuickCustomer";
            this.QuickCustomer.NoAccentTextColor = System.Drawing.Color.Empty;
            this.QuickCustomer.Size = new System.Drawing.Size(197, 76);
            this.QuickCustomer.TabIndex = 0;
            this.QuickCustomer.Text = "Add a customer";
            this.QuickCustomer.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.QuickCustomer.UseAccentColor = false;
            this.QuickCustomer.UseVisualStyleBackColor = true;
            this.QuickCustomer.Click += new System.EventHandler(this.QuickCustomer_Click);
            // 
            // HomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 360);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.GreetingLabel);
            this.Controls.Add(this.materialDivider1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "HomeForm";
            this.Text = "HomeForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HomeForm_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MaterialSkin.Controls.MaterialLabel GreetingLabel;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private MaterialSkin.Controls.MaterialButton QuickTimeSchedule;
        private MaterialSkin.Controls.MaterialButton QuickVehicle;
        private MaterialSkin.Controls.MaterialButton QuickCustomer;
        private MaterialSkin.Controls.MaterialButton QuickSalary;
        private MaterialSkin.Controls.MaterialButton QuickOrder;
    }
}