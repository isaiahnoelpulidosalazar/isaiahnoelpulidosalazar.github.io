namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    partial class ReportsForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            this.SalesChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.DailyButton = new MaterialSkin.Controls.MaterialButton();
            this.MonthlyButton = new MaterialSkin.Controls.MaterialButton();
            this.ExportDataButton = new MaterialSkin.Controls.MaterialButton();
            this.SummaryButton = new MaterialSkin.Controls.MaterialButton();
            ((System.ComponentModel.ISupportInitialize)(this.SalesChart)).BeginInit();
            this.SuspendLayout();
            // 
            // SalesChart
            // 
            this.SalesChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.SalesChart.ChartAreas.Add(chartArea1);
            this.SalesChart.Location = new System.Drawing.Point(169, 8);
            this.SalesChart.Margin = new System.Windows.Forms.Padding(0, 8, 8, 8);
            this.SalesChart.Name = "SalesChart";
            this.SalesChart.PaletteCustomColors = new System.Drawing.Color[] {
        System.Drawing.Color.Red};
            this.SalesChart.Size = new System.Drawing.Size(460, 461);
            this.SalesChart.TabIndex = 11;
            this.SalesChart.Text = "Sales";
            // 
            // DailyButton
            // 
            this.DailyButton.AutoSize = false;
            this.DailyButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DailyButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.DailyButton.Depth = 0;
            this.DailyButton.HighEmphasis = true;
            this.DailyButton.Icon = null;
            this.DailyButton.Location = new System.Drawing.Point(11, 8);
            this.DailyButton.Margin = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this.DailyButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.DailyButton.Name = "DailyButton";
            this.DailyButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.DailyButton.Size = new System.Drawing.Size(150, 36);
            this.DailyButton.TabIndex = 27;
            this.DailyButton.Text = "Daily Sales";
            this.DailyButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.DailyButton.UseAccentColor = false;
            this.DailyButton.UseVisualStyleBackColor = true;
            this.DailyButton.Click += new System.EventHandler(this.DailyButton_Click);
            // 
            // MonthlyButton
            // 
            this.MonthlyButton.AutoSize = false;
            this.MonthlyButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.MonthlyButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.MonthlyButton.Depth = 0;
            this.MonthlyButton.HighEmphasis = true;
            this.MonthlyButton.Icon = null;
            this.MonthlyButton.Location = new System.Drawing.Point(11, 52);
            this.MonthlyButton.Margin = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this.MonthlyButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.MonthlyButton.Name = "MonthlyButton";
            this.MonthlyButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.MonthlyButton.Size = new System.Drawing.Size(150, 36);
            this.MonthlyButton.TabIndex = 29;
            this.MonthlyButton.Text = "Monthly Sales";
            this.MonthlyButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.MonthlyButton.UseAccentColor = false;
            this.MonthlyButton.UseVisualStyleBackColor = true;
            this.MonthlyButton.Click += new System.EventHandler(this.MonthlyButton_Click);
            // 
            // ExportDataButton
            // 
            this.ExportDataButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ExportDataButton.AutoSize = false;
            this.ExportDataButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ExportDataButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.ExportDataButton.Depth = 0;
            this.ExportDataButton.HighEmphasis = true;
            this.ExportDataButton.Icon = null;
            this.ExportDataButton.Location = new System.Drawing.Point(11, 433);
            this.ExportDataButton.Margin = new System.Windows.Forms.Padding(8, 0, 8, 8);
            this.ExportDataButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.ExportDataButton.Name = "ExportDataButton";
            this.ExportDataButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.ExportDataButton.Size = new System.Drawing.Size(150, 36);
            this.ExportDataButton.TabIndex = 34;
            this.ExportDataButton.Text = "Export to PDF";
            this.ExportDataButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.ExportDataButton.UseAccentColor = false;
            this.ExportDataButton.UseVisualStyleBackColor = true;
            this.ExportDataButton.Click += new System.EventHandler(this.ExportDataButton_Click);
            // 
            // SummaryButton
            // 
            this.SummaryButton.AutoSize = false;
            this.SummaryButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SummaryButton.Density = MaterialSkin.Controls.MaterialButton.MaterialButtonDensity.Default;
            this.SummaryButton.Depth = 0;
            this.SummaryButton.HighEmphasis = true;
            this.SummaryButton.Icon = null;
            this.SummaryButton.Location = new System.Drawing.Point(11, 96);
            this.SummaryButton.Margin = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this.SummaryButton.MouseState = MaterialSkin.MouseState.HOVER;
            this.SummaryButton.Name = "SummaryButton";
            this.SummaryButton.NoAccentTextColor = System.Drawing.Color.Empty;
            this.SummaryButton.Size = new System.Drawing.Size(150, 36);
            this.SummaryButton.TabIndex = 35;
            this.SummaryButton.Text = "Summary";
            this.SummaryButton.Type = MaterialSkin.Controls.MaterialButton.MaterialButtonType.Contained;
            this.SummaryButton.UseAccentColor = false;
            this.SummaryButton.UseVisualStyleBackColor = true;
            this.SummaryButton.Click += new System.EventHandler(this.SummaryButton_Click);
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.Controls.Add(this.SummaryButton);
            this.Controls.Add(this.ExportDataButton);
            this.Controls.Add(this.MonthlyButton);
            this.Controls.Add(this.DailyButton);
            this.Controls.Add(this.SalesChart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ReportsForm";
            this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ReportsForm";
            this.Load += new System.EventHandler(this.ReportsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.SalesChart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart SalesChart;
        private MaterialSkin.Controls.MaterialButton DailyButton;
        private MaterialSkin.Controls.MaterialButton MonthlyButton;
        private MaterialSkin.Controls.MaterialButton ExportDataButton;
        private MaterialSkin.Controls.MaterialButton SummaryButton;
    }
}