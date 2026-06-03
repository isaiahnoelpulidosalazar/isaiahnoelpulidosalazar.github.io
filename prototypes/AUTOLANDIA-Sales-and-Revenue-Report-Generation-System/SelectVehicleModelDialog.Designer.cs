namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class SelectVehicleModelDialog
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
            this.SearchBarVehicle = new MaterialSkin.Controls.MaterialTextBox();
            this.CancelButton = new MaterialSkin.Controls.MaterialButton();
            this.VehicleListContainer = new System.Windows.Forms.Panel();
            this.VehicleList = new System.Windows.Forms.TableLayoutPanel();
            this.VehicleListContainer.SuspendLayout();
            this.SuspendLayout();
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
            this.SearchBarVehicle.Location = new System.Drawing.Point(10, 36);
            this.SearchBarVehicle.Margin = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.SearchBarVehicle.MaxLength = 50;
            this.SearchBarVehicle.MouseState = MaterialSkin.MouseState.OUT;
            this.SearchBarVehicle.Multiline = false;
            this.SearchBarVehicle.Name = "SearchBarVehicle";
            this.SearchBarVehicle.Size = new System.Drawing.Size(380, 36);
            this.SearchBarVehicle.TabIndex = 28;
            this.SearchBarVehicle.Text = "";
            this.SearchBarVehicle.TrailingIcon = null;
            this.SearchBarVehicle.UseTallSize = false;
            this.SearchBarVehicle.TextChanged += new System.EventHandler(this.SearchBarVehicle_TextChanged);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CancelButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.CancelButton.Depth = 0;
            this.CancelButton.HighEmphasis = true;
            this.CancelButton.Icon = null;
            this.CancelButton.Location = new System.Drawing.Point(10, 354);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(8);
            this.CancelButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.CancelButton.Size = new System.Drawing.Size(77, 36);
            this.CancelButton.TabIndex = 26;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.CancelButton.UseAccentColor = false;
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // VehicleListContainer
            // 
            this.VehicleListContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VehicleListContainer.AutoScroll = true;
            this.VehicleListContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VehicleListContainer.Controls.Add(this.VehicleList);
            this.VehicleListContainer.Location = new System.Drawing.Point(10, 80);
            this.VehicleListContainer.Margin = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this.VehicleListContainer.Name = "VehicleListContainer";
            this.VehicleListContainer.Size = new System.Drawing.Size(380, 266);
            this.VehicleListContainer.TabIndex = 27;
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
            this.VehicleList.Size = new System.Drawing.Size(378, 0);
            this.VehicleList.TabIndex = 0;
            // 
            // SelectVehicleModelDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.Controls.Add(this.SearchBarVehicle);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.VehicleListContainer);
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None;
            this.Name = "SelectVehicleModelDialog";
            this.Padding = new System.Windows.Forms.Padding(2, 20, 2, 2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectVehicleModelDialog";
            this.VehicleListContainer.ResumeLayout(false);
            this.VehicleListContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialTextBox SearchBarVehicle;
        private MaterialSkin.Controls.MaterialButton CancelButton;
        private System.Windows.Forms.Panel VehicleListContainer;
        private System.Windows.Forms.TableLayoutPanel VehicleList;
    }
}