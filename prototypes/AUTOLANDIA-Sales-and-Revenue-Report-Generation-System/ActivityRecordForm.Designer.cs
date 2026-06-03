namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class ActivityRecordForm
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
            this.materialDivider2 = new MaterialSkin.Controls.MaterialDivider();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ActivityListContainer = new System.Windows.Forms.Panel();
            this.ActivityList = new System.Windows.Forms.TableLayoutPanel();
            this.SearchBarActivity = new MaterialSkin.Controls.MaterialTextBox();
            this.tableLayoutPanel2.SuspendLayout();
            this.ActivityListContainer.SuspendLayout();
            this.SuspendLayout();
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
            this.materialDivider2.Size = new System.Drawing.Size(916, 2);
            this.materialDivider2.TabIndex = 23;
            this.materialDivider2.Text = "materialDivider2";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel2.Controls.Add(this.label5, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(9, 63);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(916, 48);
            this.tableLayoutPanel2.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Location = new System.Drawing.Point(192, 9);
            this.label5.Margin = new System.Windows.Forms.Padding(8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(715, 30);
            this.label5.TabIndex = 1;
            this.label5.Text = "Message";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Location = new System.Drawing.Point(9, 9);
            this.label6.Margin = new System.Windows.Forms.Padding(8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(166, 30);
            this.label6.TabIndex = 0;
            this.label6.Text = "Date";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ActivityListContainer
            // 
            this.ActivityListContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ActivityListContainer.AutoScroll = true;
            this.ActivityListContainer.Controls.Add(this.ActivityList);
            this.ActivityListContainer.Location = new System.Drawing.Point(9, 111);
            this.ActivityListContainer.Margin = new System.Windows.Forms.Padding(0);
            this.ActivityListContainer.Name = "ActivityListContainer";
            this.ActivityListContainer.Size = new System.Drawing.Size(916, 240);
            this.ActivityListContainer.TabIndex = 25;
            // 
            // ActivityList
            // 
            this.ActivityList.AutoSize = true;
            this.ActivityList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ActivityList.ColumnCount = 1;
            this.ActivityList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ActivityList.Dock = System.Windows.Forms.DockStyle.Top;
            this.ActivityList.Location = new System.Drawing.Point(0, 0);
            this.ActivityList.Margin = new System.Windows.Forms.Padding(0);
            this.ActivityList.Name = "ActivityList";
            this.ActivityList.RowCount = 1;
            this.ActivityList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ActivityList.Size = new System.Drawing.Size(916, 0);
            this.ActivityList.TabIndex = 0;
            // 
            // SearchBarActivity
            // 
            this.SearchBarActivity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBarActivity.AnimateReadOnly = false;
            this.SearchBarActivity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.SearchBarActivity.Depth = 0;
            this.SearchBarActivity.Font = new System.Drawing.Font("Roboto", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.SearchBarActivity.Hint = "Search";
            this.SearchBarActivity.LeadingIcon = null;
            this.SearchBarActivity.Location = new System.Drawing.Point(9, 9);
            this.SearchBarActivity.Margin = new System.Windows.Forms.Padding(0);
            this.SearchBarActivity.MaxLength = 50;
            this.SearchBarActivity.MouseState = MaterialSkin.MouseState.OUT;
            this.SearchBarActivity.Multiline = false;
            this.SearchBarActivity.Name = "SearchBarActivity";
            this.SearchBarActivity.Size = new System.Drawing.Size(916, 36);
            this.SearchBarActivity.TabIndex = 27;
            this.SearchBarActivity.Text = "";
            this.SearchBarActivity.TrailingIcon = null;
            this.SearchBarActivity.UseTallSize = false;
            this.SearchBarActivity.TextChanged += new System.EventHandler(this.SearchBarActivity_TextChanged);
            // 
            // ActivityRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 360);
            this.Controls.Add(this.SearchBarActivity);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.ActivityListContainer);
            this.Controls.Add(this.materialDivider2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ActivityRecordForm";
            this.Text = "ActivityRecordForm";
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ActivityListContainer.ResumeLayout(false);
            this.ActivityListContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MaterialSkin.Controls.MaterialDivider materialDivider2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel ActivityListContainer;
        private System.Windows.Forms.TableLayoutPanel ActivityList;
        private MaterialSkin.Controls.MaterialTextBox SearchBarActivity;
    }
}