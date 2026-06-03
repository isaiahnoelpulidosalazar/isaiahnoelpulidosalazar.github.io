using MaterialSkin.Controls;
using Microsoft.Data.Sqlite;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;
using static TheArtOfDev.HtmlRenderer.Adapters.RGraphicsPath;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class ServicesAndPackagesForm : Form
    {
        Color DefaultBackgroundColor;

        public ServicesAndPackagesForm()
        {
            InitializeComponent();

            ServiceList.Controls.Clear();
            ServiceList.RowStyles.Clear();
            ServiceList.VerticalScroll.Visible = true;
            ServiceList.HorizontalScroll.Enabled = false;
            ServiceList.HorizontalScroll.Visible = false;

            PackageList.Controls.Clear();
            PackageList.RowStyles.Clear();
            PackageList.VerticalScroll.Visible = true;
            PackageList.HorizontalScroll.Enabled = false;
            PackageList.HorizontalScroll.Visible = false;

            FilterService.Items.Add("ID");
            FilterService.Items.Add("Name");
            FilterService.Items.Add("Status");
            FilterService.SelectedIndex = 0;

            FilterPackage.Items.Add("ID");
            FilterPackage.Items.Add("Name");
            FilterPackage.Items.Add("Services Included");
            FilterPackage.Items.Add("Status");
            FilterPackage.SelectedIndex = 0;
        }

        public void RefreshServices()
        {
            RecreateGlobalServiceList();

            ServiceList.Controls.Clear();
            ServiceList.RowStyles.Clear();

            foreach (ServiceItem Service in GlobalServiceList)
            {
                RefreshRows(Service);
            }
        }

        public void RefreshPackages()
        {
            RecreateGlobalPackageList();

            PackageList.Controls.Clear();
            PackageList.RowStyles.Clear();

            foreach (PackageItem Package in GlobalPackageList)
            {
                RefreshRows(Package);
            }
        }

        private void ServicesAndPackagesForm_Load(object sender, EventArgs e)
        {
            tableLayoutPanel2.Width = ServiceList.Width;
            tableLayoutPanel1.Width = PackageList.Width;
            RefreshServices();
            RefreshPackages();
        }

        private void ServicesTab_Enter(object sender, EventArgs e)
        {
            tableLayoutPanel2.Width = ServiceList.Width;
        }

        private void PackagesTab_Enter(object sender, EventArgs e)
        {
            tableLayoutPanel1.Width = PackageList.Width;
        }

        private void SearchBarService_TextChanged(object sender, EventArgs e)
        {
            ServiceFilter();
        }

        private void SearchBarPackage_TextChanged(object sender, EventArgs e)
        {
            PackageFilter();
        }

        private void FilterPackage_TextChanged(object sender, EventArgs e)
        {
            PackageFilter();
        }

        private void FilterService_TextChanged(object sender, EventArgs e)
        {
            ServiceFilter();
        }

        void ServiceFilter()
        {
            ServiceList.Controls.Clear();
            ServiceList.RowStyles.Clear();

            foreach (ServiceItem Service in GlobalServiceList)
            {
                if (FilterService.SelectedIndex == 0 || FilterService.SelectedIndex == -1)
                {
                    if (Service.ID.ToUpper().Contains(SearchBarService.Text.ToUpper()))
                    {
                        RefreshRows(Service);
                    }
                }
                if (FilterService.SelectedIndex == 1)
                {
                    if (Service.Name.ToUpper().Contains(SearchBarService.Text.ToUpper()))
                    {
                        RefreshRows(Service);
                    }
                }
                if (FilterService.SelectedIndex == 2)
                {
                    if (Service.Status.ToUpper().Contains(SearchBarService.Text.ToUpper()))
                    {
                        RefreshRows(Service);
                    }
                }
            }
        }

        void PackageFilter()
        {
            PackageList.Controls.Clear();
            PackageList.RowStyles.Clear();

            foreach (PackageItem Package in GlobalPackageList)
            {
                if (FilterPackage.SelectedIndex == 0 || FilterPackage.SelectedIndex == -1)
                {
                    if (Package.ID.ToUpper().Contains(SearchBarPackage.Text.ToUpper()))
                    {
                        RefreshRows(Package);
                    }
                }
                if (FilterPackage.SelectedIndex == 1)
                {
                    if (Package.Name.ToUpper().Contains(SearchBarPackage.Text.ToUpper()))
                    {
                        RefreshRows(Package);
                    }
                }
                if (FilterPackage.SelectedIndex == 2)
                {
                    if (Package.Details.ToUpper().Contains(SearchBarPackage.Text.ToUpper()))
                    {
                        RefreshRows(Package);
                    }
                }
                if (FilterPackage.SelectedIndex == 3)
                {
                    if (Package.Status.ToUpper().Contains(SearchBarPackage.Text.ToUpper()))
                    {
                        RefreshRows(Package);
                    }
                }
            }
        }

        void RefreshRows(PackageItem Package)
        {
            RowStyle Row = new RowStyle(SizeType.Absolute, 55f);
            TableLayoutPanel Panel = new TableLayoutPanel
            {
                ColumnCount = 9
            };
            Label ID = new Label();
            Label Name = new Label();
            Label Details = new Label();
            Label S = new Label();
            Label M = new Label();
            Label L = new Label();
            Label XL = new Label();
            Label XXL = new Label();
            MaterialComboBox Status = new MaterialComboBox();

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
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 8f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15f));
            Panel.Margin = new Padding(0);

            ID.Dock = DockStyle.Fill;
            ID.Text = Package.ID;
            ID.TextAlign = ContentAlignment.MiddleCenter;
            ID.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            ID.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            Name.Dock = DockStyle.Fill;
            Name.Text = Package.Name;
            Name.TextAlign = ContentAlignment.MiddleCenter;
            Name.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            Name.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            Details.Dock = DockStyle.Fill;
            Details.Text = Package.Details;
            Details.TextAlign = ContentAlignment.MiddleCenter;
            Details.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            Details.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            S.Dock = DockStyle.Fill;
            S.Text = Package.S > 0 ? $"₱{Package.S.ToString("N2")}" : "-";
            S.TextAlign = ContentAlignment.MiddleCenter;
            S.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            S.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            M.Dock = DockStyle.Fill;
            M.Text = Package.M > 0 ? $"₱{Package.M.ToString("N2")}" : "-";
            M.TextAlign = ContentAlignment.MiddleCenter;
            M.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            M.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            L.Dock = DockStyle.Fill;
            L.Text = Package.L > 0 ? $"₱{Package.L.ToString("N2")}" : "-";
            L.TextAlign = ContentAlignment.MiddleCenter;
            L.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            L.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            XL.Dock = DockStyle.Fill;
            XL.Text = Package.XL > 0 ? $"₱{Package.XL.ToString("N2")}" : "-";
            XL.TextAlign = ContentAlignment.MiddleCenter;
            XL.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            XL.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            XXL.Dock = DockStyle.Fill;
            XXL.Text = Package.XXL > 0 ? $"₱{Package.XXL.ToString("N2")}" : "-";
            XXL.TextAlign = ContentAlignment.MiddleCenter;
            XXL.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            XXL.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            Status.Dock = DockStyle.Top;
            Status.Items.Add("Available");
            Status.Items.Add("Unavailable");
            if (Package.Status.Equals("Available"))
            {
                Status.SelectedIndex = 0;
            }
            if (Package.Status.Equals("Unavailable"))
            {
                Status.SelectedIndex = 1;
            }
            Status.SelectedIndexChanged += (sndr, evnt) =>
            {
                try
                {
                    DateTime Now = DateTime.Now;

                    RecordActivity($"Changed package [{Package.ID}] status from [{Package.Status}] to [{Status.Text}]");

                    SqliteCommand Command = new SqliteCommand($"UPDATE AUTOLANDIA_PackageList SET PackageStatus='{Status.Text}' WHERE PackageId='{Package.ID}'", SQL);

                    Command.ExecuteNonQuery();

                    OkMessageBox("Successfully changed package status!");
                    RefreshPackages();
                    GlobalActivityRecordForm.RefreshActivities();
                }
                catch (Exception Exception)
                {
                    AlertMessageBox(Exception.Message);
                }
            };

            Panel.Click += (sndr, evnt) =>
            {
                new EditPackageDialog(this, Package.ID).ShowDialog();
            };
            ID.Click += (sndr, evnt) =>
            {
                new EditPackageDialog(this, Package.ID).ShowDialog();
            };
            Name.Click += (sndr, evnt) =>
            {
                new EditPackageDialog(this, Package.ID).ShowDialog();
            };
            Details.Click += (sndr, evnt) =>
            {
                new EditPackageDialog(this, Package.ID).ShowDialog();
            };
            S.Click += (sndr, evnt) =>
            {
                new EditPackageDialog(this, Package.ID).ShowDialog();
            };
            M.Click += (sndr, evnt) =>
            {
                new EditPackageDialog(this, Package.ID).ShowDialog();
            };
            L.Click += (sndr, evnt) =>
            {
                new EditPackageDialog(this, Package.ID).ShowDialog();
            };
            XL.Click += (sndr, evnt) =>
            {
                new EditPackageDialog(this, Package.ID).ShowDialog();
            };
            XXL.Click += (sndr, evnt) =>
            {
                new EditPackageDialog(this, Package.ID).ShowDialog();
            };

            PackageList.RowStyles.Add(Row);
            Panel.Controls.Add(ID, 0, 0);
            Panel.Controls.Add(Name, 1, 0);
            Panel.Controls.Add(Details, 2, 0);
            Panel.Controls.Add(S, 3, 0);
            Panel.Controls.Add(M, 4, 0);
            Panel.Controls.Add(L, 5, 0);
            Panel.Controls.Add(XL, 6, 0);
            Panel.Controls.Add(XXL, 7, 0);
            Panel.Controls.Add(Status, 8, 0);
            PackageList.Controls.Add(Panel);

            tableLayoutPanel1.Width = PackageList.Width;
        }

        void RefreshRows(ServiceItem Service)
        {
            RowStyle Row = new RowStyle(SizeType.Absolute, 55f);
            TableLayoutPanel Panel = new TableLayoutPanel
            {
                ColumnCount = 8
            };
            Label ID = new Label();
            Label Name = new Label();
            Label S = new Label();
            Label M = new Label();
            Label L = new Label();
            Label XL = new Label();
            Label XXL = new Label();
            MaterialComboBox Status = new MaterialComboBox();

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
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10f));
            Panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15f));
            Panel.Margin = new Padding(0);

            ID.Dock = DockStyle.Fill;
            ID.Text = Service.ID;
            ID.TextAlign = ContentAlignment.MiddleCenter;
            ID.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            ID.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            Name.Dock = DockStyle.Fill;
            Name.Text = Service.Name;
            Name.TextAlign = ContentAlignment.MiddleCenter;
            Name.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            Name.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            S.Dock = DockStyle.Fill;
            S.Text = Service.S > 0 ? $"₱{Service.S.ToString("N2")}" : "-";
            S.TextAlign = ContentAlignment.MiddleCenter;
            S.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            S.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            M.Dock = DockStyle.Fill;
            M.Text = Service.M > 0 ? $"₱{Service.M.ToString("N2")}" : "-";
            M.TextAlign = ContentAlignment.MiddleCenter;
            M.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            M.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            L.Dock = DockStyle.Fill;
            L.Text = Service.L > 0 ? $"₱{Service.L.ToString("N2")}" : "-";
            L.TextAlign = ContentAlignment.MiddleCenter;
            L.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            L.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            XL.Dock = DockStyle.Fill;
            XL.Text = Service.XL > 0 ? $"₱{Service.XL.ToString("N2")}" : "-";
            XL.TextAlign = ContentAlignment.MiddleCenter;
            XL.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            XL.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            XXL.Dock = DockStyle.Fill;
            XXL.Text = Service.XXL > 0 ? $"₱{Service.XXL.ToString("N2")}" : "-";
            XXL.TextAlign = ContentAlignment.MiddleCenter;
            XXL.MouseEnter += (sndr, evnt) =>
            {
                Panel.BackColor = Color.FromArgb(200, 200, 200);
            };
            XXL.MouseLeave += (sndr, evnt) =>
            {
                Panel.BackColor = DefaultBackgroundColor;
            };

            Status.Dock = DockStyle.Top;
            Status.Items.Add("Available");
            Status.Items.Add("Unavailable");
            if (Service.Status.Equals("Available"))
            {
                Status.SelectedIndex = 0;
            }
            if (Service.Status.Equals("Unavailable"))
            {
                Status.SelectedIndex = 1;
            }
            Status.SelectedIndexChanged += (sndr, evnt) =>
            {
                try
                {
                    DateTime Now = DateTime.Now;

                    RecordActivity($"Changed service [{Service.ID}] status from [{Service.Status}] to [{Status.Text}]");

                    SqliteCommand Command = new SqliteCommand($"UPDATE AUTOLANDIA_ServiceList SET ServiceStatus='{Status.Text}' WHERE ServiceId='{Service.ID}'", SQL);

                    Command.ExecuteNonQuery();

                    OkMessageBox("Successfully changed service status!");
                    RefreshServices();
                    GlobalActivityRecordForm.RefreshActivities();
                }
                catch (Exception Exception)
                {
                    AlertMessageBox(Exception.Message);
                }
            };

            Panel.Click += (sndr, evnt) =>
            {
                new EditServiceDialog(this, Service.ID).ShowDialog();
            };
            ID.Click += (sndr, evnt) =>
            {
                new EditServiceDialog(this, Service.ID).ShowDialog();
            };
            Name.Click += (sndr, evnt) =>
            {
                new EditServiceDialog(this, Service.ID).ShowDialog();
            };
            S.Click += (sndr, evnt) =>
            {
                new EditServiceDialog(this, Service.ID).ShowDialog();
            };
            M.Click += (sndr, evnt) =>
            {
                new EditServiceDialog(this, Service.ID).ShowDialog();
            };
            L.Click += (sndr, evnt) =>
            {
                new EditServiceDialog(this, Service.ID).ShowDialog();
            };
            XL.Click += (sndr, evnt) =>
            {
                new EditServiceDialog(this, Service.ID).ShowDialog();
            };
            XXL.Click += (sndr, evnt) =>
            {
                new EditServiceDialog(this, Service.ID).ShowDialog();
            };
            
            ServiceList.RowStyles.Add(Row);
            Panel.Controls.Add(ID, 0, 0);
            Panel.Controls.Add(Name, 1, 0);
            Panel.Controls.Add(S, 2, 0);
            Panel.Controls.Add(M, 3, 0);
            Panel.Controls.Add(L, 4, 0);
            Panel.Controls.Add(XL, 5, 0);
            Panel.Controls.Add(XXL, 6, 0);
            Panel.Controls.Add(Status, 7, 0);
            ServiceList.Controls.Add(Panel);

            tableLayoutPanel2.Width = ServiceList.Width;
        }
    }
}
