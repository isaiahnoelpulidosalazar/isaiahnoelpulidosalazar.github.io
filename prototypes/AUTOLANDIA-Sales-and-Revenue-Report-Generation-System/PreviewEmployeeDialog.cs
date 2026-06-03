using MaterialSkin.Controls;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class PreviewEmployeeDialog : MaterialForm
    {
        Color DefaultBackgroundColor;
        string EmployeeID;
        string[] DocumentListArray =
        {
            "Valid ID",
            "Social Security System (SSS)",
            "PhilHealth",
            "Home Development Mutual Fund (HDMF)",
            "Department of Labor and Employment (DOLE)",
            "Tax Identification Number (TIN)"
        };

        public PreviewEmployeeDialog(string EmployeeID)
        {
            InitializeComponent();

            this.EmployeeID = EmployeeID;

            DocumentList.Controls.Clear();
            DocumentList.RowStyles.Clear();
            DocumentList.VerticalScroll.Visible = true;
            DocumentList.HorizontalScroll.Enabled = false;
            DocumentList.HorizontalScroll.Visible = false;
        }

        public void RefreshDetails()
        {
            foreach (EmployeeItem Employee in GlobalEmployeeList)
            {
                if (Employee.ID.Equals(EmployeeID))
                {
                    EmployeeNameLabel.Text = $"{Employee.LastName}, {Employee.FirstName} {Employee.MiddleName}";
                    EmployeeMobileNumberLabel.Text = Employee.MobileNumber;
                    foreach (char Document in Employee.Documents)
                    {
                        foreach (string Item in DocumentListArray)
                        {
                            if (Item.First() == Document)
                            {
                                RowStyle Row = new RowStyle(SizeType.Absolute, 55f);
                                TableLayoutPanel Panel = new TableLayoutPanel
                                {
                                    ColumnCount = 1
                                };
                                Label DocumentLabel = new Label();

                                if (DefaultBackgroundColor == null)
                                {
                                    DefaultBackgroundColor = Panel.BackColor;
                                }

                                Panel.Dock = DockStyle.Fill;
                                Panel.MouseEnter += (sndr, evnt) =>
                                {
                                    Panel.BackColor = Color.FromArgb(200, 200, 200);
                                };
                                Panel.MouseLeave += (sndr, evnt) =>
                                {
                                    Panel.BackColor = DefaultBackgroundColor;
                                };
                                Panel.ColumnStyles.Clear();
                                Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
                                Panel.Margin = new Padding(0);

                                DocumentLabel.Dock = DockStyle.Fill;
                                DocumentLabel.Text = Item;
                                DocumentLabel.TextAlign = ContentAlignment.MiddleCenter;
                                DocumentLabel.MouseEnter += (sndr, evnt) =>
                                {
                                    Panel.BackColor = Color.FromArgb(200, 200, 200);
                                };
                                DocumentLabel.MouseLeave += (sndr, evnt) =>
                                {
                                    Panel.BackColor = DefaultBackgroundColor;
                                };

                                DocumentList.RowStyles.Add(Row);
                                Panel.Controls.Add(DocumentLabel, 0, 0);
                                DocumentList.Controls.Add(Panel);

                                tableLayoutPanel2.Width = DocumentList.Width;
                            }
                        }
                    }
                }
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            new EditEmployeeDialog(this, EmployeeID).ShowDialog();
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PreviewEmployeeDialog_Load(object sender, EventArgs e)
        {
            RefreshDetails();
        }
    }
}
