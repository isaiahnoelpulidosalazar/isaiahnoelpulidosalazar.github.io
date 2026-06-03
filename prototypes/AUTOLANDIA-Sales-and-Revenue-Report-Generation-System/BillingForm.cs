using MaterialSkin.Controls;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class BillingForm : Form
    {
        Color DefaultBackgroundColor;
        List<BillingItem> Temp = new List<BillingItem>(GlobalBillingList);
        DateTime Global;
        bool PaidBillings = false;

        public BillingForm()
        {
            InitializeComponent();

            BillingList.Controls.Clear();
            BillingList.RowStyles.Clear();
            BillingList.VerticalScroll.Visible = true;
            BillingList.HorizontalScroll.Enabled = false;
            BillingList.HorizontalScroll.Visible = false;

            FilterBilling.Items.Add("ID");
            FilterBilling.Items.Add("Progress");
            FilterBilling.SelectedIndex = 0;

            Global = DateTime.Now;
            DatePickerButton.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }

        public void RefreshBillings()
        {
            RecreateGlobalBillingList();

            Temp = new List<BillingItem>(GlobalBillingList);
            Temp.Reverse();

            BillingList.Controls.Clear();
            BillingList.RowStyles.Clear();

            for (int a = 0; a < Temp.Count;)
            {
                if (!DateTime.Parse(Temp[a].DateCreated).Date.ToString("d").Equals(Global.Date.ToString("d")))
                {
                    Temp.Remove(Temp[a]);
                    a = 0;
                }
                else
                {
                    a++;
                }
            }

            foreach (BillingItem Billing in Temp)
            {
                if (PaidBillings)
                {
                    if (Billing.Status.Equals("Paid"))
                    {
                        RefreshRows(Billing);
                    }
                }
                else
                {
                    RefreshRows(Billing);
                }
            }
        }

        private void SearchBarBilling_TextChanged(object sender, EventArgs e)
        {
            BillingList.Controls.Clear();
            BillingList.RowStyles.Clear();

            foreach (BillingItem Billing in Temp)
            {
                if (FilterBilling.SelectedIndex == 0 || FilterBilling.SelectedIndex == -1)
                {
                    if (Billing.ID.Contains(SearchBarBilling.Text))
                    {
                        RefreshRows(Billing);
                    }
                }
                if (FilterBilling.SelectedIndex == 1)
                {
                    if (Billing.Status.ToUpper().Contains(SearchBarBilling.Text.ToUpper()))
                    {
                        RefreshRows(Billing);
                    }
                }
            }
        }

        private void FilterBilling_TextChanged(object sender, EventArgs e)
        {
            BillingList.Controls.Clear();
            BillingList.RowStyles.Clear();

            foreach (BillingItem Billing in Temp)
            {
                if (FilterBilling.SelectedIndex == 0 || FilterBilling.SelectedIndex == -1)
                {
                    if (Billing.ID.Contains(SearchBarBilling.Text))
                    {
                        RefreshRows(Billing);
                    }
                }
                if (FilterBilling.SelectedIndex == 1)
                {
                    if (Billing.Status.ToUpper().Contains(SearchBarBilling.Text.ToUpper()))
                    {
                        RefreshRows(Billing);
                    }
                }
            }
        }

        void RefreshRows(BillingItem Billing)
        {
            RowStyle Row = new RowStyle(SizeType.Absolute, 55f);
            TableLayoutPanel Panel = new TableLayoutPanel
            {
                ColumnCount = 6
            };
            Label Id = new Label();
            Label Balance = new Label();
            Label RemainingBalance = new Label();
            Label Progress = new Label();
            Label LastUpdated = new Label();
            Label DateCreated = new Label();

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
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15f));
            Panel.Margin = new Padding(0);

            Id.Dock = DockStyle.Fill;
            Id.Text = Billing.ID;
            Id.TextAlign = ContentAlignment.MiddleCenter;
            Id.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            Id.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            Balance.Dock = DockStyle.Fill;
            Balance.Text = $"₱{Billing.Balance.ToString("N2")}";
            Balance.TextAlign = ContentAlignment.MiddleCenter;
            Balance.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            Balance.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            RemainingBalance.Dock = DockStyle.Fill;
            RemainingBalance.Text = Billing.Balance - Billing.Paid > 0 ? $"₱{(Billing.Balance - Billing.Paid).ToString("N2")}" : $"₱{(0).ToString("N2")}";
            RemainingBalance.TextAlign = ContentAlignment.MiddleCenter;
            RemainingBalance.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            RemainingBalance.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            Progress.Dock = DockStyle.Fill;
            Progress.Text = Billing.Status;
            Progress.TextAlign = ContentAlignment.MiddleCenter;
            Progress.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            Progress.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            LastUpdated.Dock = DockStyle.Fill;
            LastUpdated.Text = Billing.LastUpdated;
            LastUpdated.TextAlign = ContentAlignment.MiddleCenter;
            LastUpdated.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            LastUpdated.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            DateCreated.Dock = DockStyle.Fill;
            DateCreated.Text = Billing.DateCreated;
            DateCreated.TextAlign = ContentAlignment.MiddleCenter;
            DateCreated.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            DateCreated.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            Panel.Click += (sndr, evnt) =>
            {
                new BillDetailDialog(this, Billing.ID).ShowDialog();
            };
            Id.Click += (sndr, evnt) =>
            {
                new BillDetailDialog(this, Billing.ID).ShowDialog();
            };
            Balance.Click += (sndr, evnt) =>
            {
                new BillDetailDialog(this, Billing.ID).ShowDialog();
            };
            RemainingBalance.Click += (sndr, evnt) =>
            {
                new BillDetailDialog(this, Billing.ID).ShowDialog();
            };
            Progress.Click += (sndr, evnt) =>
            {
                new BillDetailDialog(this, Billing.ID).ShowDialog();
            };
            LastUpdated.Click += (sndr, evnt) =>
            {
                new BillDetailDialog(this, Billing.ID).ShowDialog();
            };
            DateCreated.Click += (sndr, evnt) =>
            {
                new BillDetailDialog(this, Billing.ID).ShowDialog();
            };

            BillingList.RowStyles.Add(Row);
            Panel.Controls.Add(Id, 0, 0);
            Panel.Controls.Add(Balance, 1, 0);
            Panel.Controls.Add(RemainingBalance, 2, 0);
            Panel.Controls.Add(Progress, 3, 0);
            Panel.Controls.Add(LastUpdated, 4, 0);
            Panel.Controls.Add(DateCreated, 5, 0);
            BillingList.Controls.Add(Panel);

            tableLayoutPanel2.Width = BillingList.Width;
        }

        private void BillingForm_Load(object sender, EventArgs e)
        {
            RefreshBillings();
        }

        public void SetDate(DateTime DateTime)
        {
            Global = DateTime;
            DatePickerButton.Text = DateTime.Date.ToString("yyyy/MM/dd");

            BillingList.Controls.Clear();
            BillingList.RowStyles.Clear();

            RefreshBillings();
        }

        private void DatePickerButton_Click(object sender, EventArgs e)
        {
            new DatePickerDialog(this).ShowDialog();
        }

        private void CompletedButton_Click(object sender, EventArgs e)
        {
            if (!PaidBillings)
            {
                PaidBillings = true;
                CompletedButton.Text = "Switch to all";
                BillingList.Controls.Clear();
                BillingList.RowStyles.Clear();

                foreach (BillingItem Bills in Temp)
                {
                    if (Bills.Status.Equals("Paid"))
                    {
                        RefreshRows(Bills);
                    }
                }
            }
            else
            {
                PaidBillings = false;
                CompletedButton.Text = "Switch to paid";
                BillingList.Controls.Clear();
                BillingList.RowStyles.Clear();

                foreach (BillingItem Bills in Temp)
                {
                    RefreshRows(Bills);
                }
            }
        }
    }
}
