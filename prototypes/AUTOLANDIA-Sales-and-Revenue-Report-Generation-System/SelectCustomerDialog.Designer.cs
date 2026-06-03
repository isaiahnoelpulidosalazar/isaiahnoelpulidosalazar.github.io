namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class SelectCustomerDialog
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
            this.CustomerListContainer = new System.Windows.Forms.Panel();
            this.CustomerList = new System.Windows.Forms.TableLayoutPanel();
            this.CancelButton = new MaterialSkin.Controls.MaterialButton();
            this.SearchBarCustomer = new MaterialSkin.Controls.MaterialTextBox();
            this.CustomerListContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // CustomerListContainer
            // 
            this.CustomerListContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CustomerListContainer.AutoScroll = true;
            this.CustomerListContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CustomerListContainer.Controls.Add(this.CustomerList);
            this.CustomerListContainer.Location = new System.Drawing.Point(10, 80);
            this.CustomerListContainer.Margin = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this.CustomerListContainer.Name = "CustomerListContainer";
            this.CustomerListContainer.Size = new System.Drawing.Size(380, 266);
            this.CustomerListContainer.TabIndex = 13;
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
            this.CustomerList.Size = new System.Drawing.Size(378, 0);
            this.CustomerList.TabIndex = 0;
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
            this.CancelButton.TabIndex = 18;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.CancelButton.UseAccentColor = false;
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
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
            this.SearchBarCustomer.Location = new System.Drawing.Point(10, 36);
            this.SearchBarCustomer.Margin = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.SearchBarCustomer.MaxLength = 50;
            this.SearchBarCustomer.MouseState = MaterialSkin.MouseState.OUT;
            this.SearchBarCustomer.Multiline = false;
            this.SearchBarCustomer.Name = "SearchBarCustomer";
            this.SearchBarCustomer.Size = new System.Drawing.Size(380, 36);
            this.SearchBarCustomer.TabIndex = 26;
            this.SearchBarCustomer.Text = "";
            this.SearchBarCustomer.TrailingIcon = null;
            this.SearchBarCustomer.UseTallSize = false;
            this.SearchBarCustomer.TextChanged += new System.EventHandler(this.SearchBarCustomer_TextChanged);
            // 
            // SelectCustomerDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.Controls.Add(this.SearchBarCustomer);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.CustomerListContainer);
            this.FormStyle = MaterialSkin.Controls.MaterialForm.FormStyles.ActionBar_None;
            this.Name = "SelectCustomerDialog";
            this.Padding = new System.Windows.Forms.Padding(2, 20, 2, 2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectCustomer";
            this.CustomerListContainer.ResumeLayout(false);
            this.CustomerListContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel CustomerListContainer;
        private System.Windows.Forms.TableLayoutPanel CustomerList;
        private MaterialSkin.Controls.MaterialButton CancelButton;
        private MaterialSkin.Controls.MaterialTextBox SearchBarCustomer;
    }
}