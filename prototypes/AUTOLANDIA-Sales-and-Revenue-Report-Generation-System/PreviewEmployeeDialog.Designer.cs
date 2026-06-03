namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class PreviewEmployeeDialog
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
            this.EditButton = new MaterialSkin.Controls.MaterialButton();
            this.CloseButton = new MaterialSkin.Controls.MaterialButton();
            this.EmployeeMobileNumberLabel = new MaterialSkin.Controls.MaterialLabel();
            this.EmployeeNameLabel = new MaterialSkin.Controls.MaterialLabel();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.DocumentListContainer = new System.Windows.Forms.Panel();
            this.DocumentList = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2.SuspendLayout();
            this.DocumentListContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // EditButton
            // 
            this.EditButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.EditButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.EditButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.EditButton.Depth = 0;
            this.EditButton.HighEmphasis = true;
            this.EditButton.Icon = null;
            this.EditButton.Location = new System.Drawing.Point(406, 434);
            this.EditButton.Margin = new System.Windows.Forms.Padding(8);
            this.EditButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.EditButton.Name = "EditButton";
            this.EditButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.EditButton.Size = new System.Drawing.Size(64, 36);
            this.EditButton.TabIndex = 21;
            this.EditButton.Text = "Edit";
            this.EditButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.EditButton.UseAccentColor = false;
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.CloseButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CloseButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.CloseButton.Depth = 0;
            this.CloseButton.HighEmphasis = true;
            this.CloseButton.Icon = null;
            this.CloseButton.Location = new System.Drawing.Point(10, 434);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(8);
            this.CloseButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.CloseButton.Size = new System.Drawing.Size(66, 36);
            this.CloseButton.TabIndex = 20;
            this.CloseButton.Text = "Close";
            this.CloseButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.CloseButton.UseAccentColor = false;
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // EmployeeMobileNumberLabel
            // 
            this.EmployeeMobileNumberLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EmployeeMobileNumberLabel.Depth = 0;
            this.EmployeeMobileNumberLabel.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.EmployeeMobileNumberLabel.FontType = MaterialSkin.MaterialSkinManager.fontType.Subtitle1;
            this.EmployeeMobileNumberLabel.Location = new System.Drawing.Point(10, 110);
            this.EmployeeMobileNumberLabel.Margin = new System.Windows.Forms.Padding(8);
            this.EmployeeMobileNumberLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.EmployeeMobileNumberLabel.Name = "EmployeeMobileNumberLabel";
            this.EmployeeMobileNumberLabel.Size = new System.Drawing.Size(460, 29);
            this.EmployeeMobileNumberLabel.TabIndex = 23;
            this.EmployeeMobileNumberLabel.Text = "0000 000 0000";
            this.EmployeeMobileNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EmployeeNameLabel
            // 
            this.EmployeeNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EmployeeNameLabel.Depth = 0;
            this.EmployeeNameLabel.Font = new System.Drawing.Font("Roboto", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.EmployeeNameLabel.FontType = MaterialSkin.MaterialSkinManager.fontType.H5;
            this.EmployeeNameLabel.Location = new System.Drawing.Point(10, 36);
            this.EmployeeNameLabel.Margin = new System.Windows.Forms.Padding(8, 16, 8, 8);
            this.EmployeeNameLabel.MouseState = MaterialSkin.MouseState.HOVER;
            this.EmployeeNameLabel.Name = "EmployeeNameLabel";
            this.EmployeeNameLabel.Size = new System.Drawing.Size(460, 58);
            this.EmployeeNameLabel.TabIndex = 22;
            this.EmployeeNameLabel.Text = "Name";
            this.EmployeeNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.materialLabel1.FontType = MaterialSkin.MaterialSkinManager.fontType.Subtitle1;
            this.materialLabel1.Location = new System.Drawing.Point(10, 173);
            this.materialLabel1.Margin = new System.Windows.Forms.Padding(8);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(60, 19);
            this.materialLabel1.TabIndex = 25;
            this.materialLabel1.Text = "Vehicles";
            // 
            // materialDivider1
            // 
            this.materialDivider1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(10, 155);
            this.materialDivider1.Margin = new System.Windows.Forms.Padding(8);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(460, 2);
            this.materialDivider1.TabIndex = 24;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(10, 200);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(460, 48);
            this.tableLayoutPanel2.TabIndex = 27;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(9, 9);
            this.label6.Margin = new System.Windows.Forms.Padding(8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(442, 30);
            this.label6.TabIndex = 0;
            this.label6.Text = "Legal Documents";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DocumentListContainer
            // 
            this.DocumentListContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DocumentListContainer.AutoScroll = true;
            this.DocumentListContainer.Controls.Add(this.DocumentList);
            this.DocumentListContainer.Location = new System.Drawing.Point(10, 248);
            this.DocumentListContainer.Margin = new System.Windows.Forms.Padding(0);
            this.DocumentListContainer.Name = "DocumentListContainer";
            this.DocumentListContainer.Size = new System.Drawing.Size(460, 178);
            this.DocumentListContainer.TabIndex = 26;
            // 
            // DocumentList
            // 
            this.DocumentList.AutoSize = true;
            this.DocumentList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DocumentList.ColumnCount = 1;
            this.DocumentList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DocumentList.Dock = System.Windows.Forms.DockStyle.Top;
            this.DocumentList.Location = new System.Drawing.Point(0, 0);
            this.DocumentList.Margin = new System.Windows.Forms.Padding(0);
            this.DocumentList.Name = "DocumentList";
            this.DocumentList.RowCount = 1;
            this.DocumentList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.DocumentList.Size = new System.Drawing.Size(460, 0);
            this.DocumentList.TabIndex = 0;
            // 
            // PreviewEmployeeDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 480);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.DocumentListContainer);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.materialDivider1);
            this.Controls.Add(this.EmployeeMobileNumberLabel);
            this.Controls.Add(this.EmployeeNameLabel);
            this.Controls.Add(this.EditButton);
            this.Controls.Add(this.CloseButton);
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None;
            this.Name = "PreviewEmployeeDialog";
            this.Padding = new System.Windows.Forms.Padding(2, 20, 2, 2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EditEmployeeDialog";
            this.Load += new System.EventHandler(this.PreviewEmployeeDialog_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.DocumentListContainer.ResumeLayout(false);
            this.DocumentListContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MaterialSkin.Controls.MaterialButton EditButton;
        private MaterialSkin.Controls.MaterialButton CloseButton;
        private MaterialSkin.Controls.MaterialLabel EmployeeMobileNumberLabel;
        private MaterialSkin.Controls.MaterialLabel EmployeeNameLabel;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel DocumentListContainer;
        private System.Windows.Forms.TableLayoutPanel DocumentList;
    }
}