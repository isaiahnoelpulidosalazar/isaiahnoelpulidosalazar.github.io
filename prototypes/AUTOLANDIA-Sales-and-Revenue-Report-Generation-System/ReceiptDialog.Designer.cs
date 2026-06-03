namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class ReceiptDialog
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
            this.Banner = new System.Windows.Forms.PictureBox();
            this.BillingListContainer = new System.Windows.Forms.Panel();
            this.BillingList = new System.Windows.Forms.TableLayoutPanel();
            this.DoneButton = new MaterialSkin.Controls.MaterialButton();
            ((System.ComponentModel.ISupportInitialize)(this.Banner)).BeginInit();
            this.BillingListContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // Banner
            // 
            this.Banner.Location = new System.Drawing.Point(53, 53);
            this.Banner.Margin = new System.Windows.Forms.Padding(0);
            this.Banner.Name = "Banner";
            this.Banner.Size = new System.Drawing.Size(375, 162);
            this.Banner.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Banner.TabIndex = 2;
            this.Banner.TabStop = false;
            // 
            // BillingListContainer
            // 
            this.BillingListContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BillingListContainer.AutoScroll = true;
            this.BillingListContainer.Controls.Add(this.BillingList);
            this.BillingListContainer.Location = new System.Drawing.Point(11, 215);
            this.BillingListContainer.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.BillingListContainer.Name = "BillingListContainer";
            this.BillingListContainer.Size = new System.Drawing.Size(458, 442);
            this.BillingListContainer.TabIndex = 21;
            // 
            // BillingList
            // 
            this.BillingList.AutoSize = true;
            this.BillingList.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BillingList.ColumnCount = 1;
            this.BillingList.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BillingList.Dock = System.Windows.Forms.DockStyle.Top;
            this.BillingList.Location = new System.Drawing.Point(0, 0);
            this.BillingList.Margin = new System.Windows.Forms.Padding(0);
            this.BillingList.Name = "BillingList";
            this.BillingList.RowCount = 1;
            this.BillingList.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.BillingList.Size = new System.Drawing.Size(458, 0);
            this.BillingList.TabIndex = 0;
            // 
            // DoneButton
            // 
            this.DoneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DoneButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DoneButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.DoneButton.Depth = 0;
            this.DoneButton.HighEmphasis = true;
            this.DoneButton.Icon = null;
            this.DoneButton.Location = new System.Drawing.Point(405, 673);
            this.DoneButton.Margin = new System.Windows.Forms.Padding(8);
            this.DoneButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.DoneButton.Size = new System.Drawing.Size(64, 36);
            this.DoneButton.TabIndex = 32;
            this.DoneButton.Text = "Done";
            this.DoneButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.DoneButton.UseAccentColor = false;
            this.DoneButton.UseVisualStyleBackColor = true;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // ReceiptDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 720);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.BillingListContainer);
            this.Controls.Add(this.Banner);
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None;
            this.Name = "ReceiptDialog";
            this.Padding = new System.Windows.Forms.Padding(3, 24, 3, 3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReceiptDialog";
            ((System.ComponentModel.ISupportInitialize)(this.Banner)).EndInit();
            this.BillingListContainer.ResumeLayout(false);
            this.BillingListContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Banner;
        private System.Windows.Forms.Panel BillingListContainer;
        private System.Windows.Forms.TableLayoutPanel BillingList;
        private MaterialSkin.Controls.MaterialButton DoneButton;
    }
}