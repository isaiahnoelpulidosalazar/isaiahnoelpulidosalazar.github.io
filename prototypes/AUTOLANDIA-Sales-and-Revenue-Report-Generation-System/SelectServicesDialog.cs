using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class SelectServicesDialog : MaterialForm
    {
        NewTransactionDialog NewTransactionDialog;
        TransactionDetailDialog EditTransactionDialog;
        TableLayoutControlCollection ServiceListControls;

        public SelectServicesDialog(NewTransactionDialog NewTransactionDialog, string Brand, string Model)
        {
            InitializeComponent();
            this.NewTransactionDialog = NewTransactionDialog;

            List<ServiceItem> Temp = new List<ServiceItem>(GlobalServiceList);

            if (Brand.Equals("GENERAL"))
            {
                Temp.RemoveRange(0, Temp.Count - 5);
                if (Model.Equals("EBIKE") || Model.Equals("MOTOR") || Model.Equals("TRUCK"))
                {
                    Temp.RemoveAt(2);
                    Temp.RemoveAt(1);
                }
                if (Model.Equals("TRICYCLE"))
                {
                    Temp.RemoveRange(2, 3);
                    Temp.RemoveAt(0);
                }
                if (Model.Equals("PUV"))
                {
                    Temp.RemoveRange(3, 2);
                    Temp.RemoveRange(0, 2);
                }
            }

            Temp.Reverse();

            foreach (ServiceItem Service in Temp)
            {
                if (!InCheckedListBox(Service.Name) && Service.Status.Equals("Available"))
                {
                    ServiceListCheckBox.Items.Add(Service.Name);
                }
            }

            if (NewTransactionDialog.ServiceCheckedboxes != null)
            {
                NewTransactionDialog.ServiceCheckedboxes.Reverse();
                for (int a = 0; a < ServiceListCheckBox.Items.Count; a++)
                {
                    if (NewTransactionDialog.ServiceCheckedboxes[a].Checked)
                    {
                        ServiceListCheckBox.Items[a].Checked = true;
                    }
                }
            }
        }

        public SelectServicesDialog(TransactionDetailDialog EditTransactionDialog, TableLayoutControlCollection ServiceListControls, string Brand, string Model)
        {
            InitializeComponent();
            this.EditTransactionDialog = EditTransactionDialog;
            this.ServiceListControls = ServiceListControls;

            List<ServiceItem> Temp = new List<ServiceItem>(GlobalServiceList);

            if (Brand.Equals("GENERAL"))
            {
                Temp.RemoveRange(0, Temp.Count - 5);
                if (Model.Equals("EBIKE") || Model.Equals("MOTOR") || Model.Equals("TRUCK"))
                {
                    Temp.RemoveAt(2);
                    Temp.RemoveAt(1);
                }
                if (Model.Equals("TRICYCLE"))
                {
                    Temp.RemoveRange(2, 3);
                    Temp.RemoveAt(0);
                }
                if (Model.Equals("PUV"))
                {
                    Temp.RemoveRange(3, 2);
                    Temp.RemoveRange(0, 2);
                }
            }

            Temp.Reverse();

            foreach (ServiceItem Service in Temp)
            {
                if (!InCheckedListBox(Service.Name) && Service.Status.Equals("Available"))
                {
                    ServiceListCheckBox.Items.Add(Service.Name);
                }
            }

            if (EditTransactionDialog.ServiceCheckedboxes != null)
            {
                EditTransactionDialog.ServiceCheckedboxes.Reverse();
                for (int a = 0; a < ServiceListCheckBox.Items.Count; a++)
                {
                    if (EditTransactionDialog.ServiceCheckedboxes[a].Checked)
                    {
                        ServiceListCheckBox.Items[a].Checked = true;
                    }
                }
            }
            else
            {
                foreach (Control Service in ServiceListControls)
                {
                    for (int a = 0; a < ServiceListCheckBox.Items.Count; a++)
                    {
                        if (ServiceListCheckBox.Items[a].Text.Equals(Service.Controls[0].Text.Trim()))
                        {
                            ServiceListCheckBox.Items[a].Checked = true;
                        }
                    }
                }
            }
        }

        bool InCheckedListBox(string text)
        {
            foreach (MaterialCheckbox Item in ServiceListCheckBox.Items)
            {
                if (Item.Text == text)
                {
                    return true;
                }
            }
            return false;
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            if (NewTransactionDialog != null)
            {
                NewTransactionDialog.SetServices(ServiceListCheckBox.Items);
            }
            if (EditTransactionDialog != null)
            {
                EditTransactionDialog.SetServices(ServiceListCheckBox.Items);
            }
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SelectServicesDialog_Load(object sender, EventArgs e)
        {
            Timer Timer = new Timer();
            Timer.Interval = 1;
            Timer.Tick += (s, evt) =>
            {
                ServiceListCheckBox.AutoScrollPosition = new Point(0, 0);
                Timer.Stop();
            };
            Timer.Start();
        }

        private void SelectServicesDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (NewTransactionDialog != null)
            {
                if (NewTransactionDialog.ServiceCheckedboxes != null)
                {
                    NewTransactionDialog.ServiceCheckedboxes.Reverse();
                }
            }
            if (EditTransactionDialog != null)
            {
                if (EditTransactionDialog.ServiceCheckedboxes != null)
                {
                    EditTransactionDialog.ServiceCheckedboxes.Reverse();
                }
            }
        }

        private void SearchBarService_TextChanged(object sender, EventArgs e)
        {
            ServiceListCheckBox.Items.Clear();
            ServiceListCheckBox.Controls.Clear();

            if (NewTransactionDialog != null)
            {
                List<ServiceItem> Temp = new List<ServiceItem>(GlobalServiceList);
                Temp.Reverse();

                foreach (ServiceItem Service in Temp)
                {
                    if (!InCheckedListBox(Service.Name))
                    {
                        if (Service.Name.ToUpper().Contains(SearchBarService.Text.ToUpper()))
                        {
                            ServiceListCheckBox.Items.Add(Service.Name);
                        }
                        else if (string.IsNullOrEmpty(SearchBarService.Text.ToUpper()))
                        {
                            ServiceListCheckBox.Items.Add(Service.Name);
                        }
                    }
                }

                if (NewTransactionDialog.ServiceCheckedboxes != null)
                {
                    NewTransactionDialog.ServiceCheckedboxes.Reverse();
                    for (int a = 0; a < ServiceListCheckBox.Items.Count; a++)
                    {
                        if (NewTransactionDialog.ServiceCheckedboxes[a].Checked)
                        {
                            ServiceListCheckBox.Items[a].Checked = true;
                        }
                    }
                }
            }
            if (EditTransactionDialog != null)
            {
                List<ServiceItem> Temp = new List<ServiceItem>(GlobalServiceList);
                Temp.Reverse();

                foreach (ServiceItem Service in Temp)
                {
                    if (!InCheckedListBox(Service.Name))
                    {
                        if (Service.Name.ToUpper().Contains(SearchBarService.Text.ToUpper()))
                        {
                            ServiceListCheckBox.Items.Add(Service.Name);
                        }
                        else if (string.IsNullOrEmpty(SearchBarService.Text.ToUpper()))
                        {
                            ServiceListCheckBox.Items.Add(Service.Name);
                        }
                    }
                }

                if (EditTransactionDialog.ServiceCheckedboxes != null)
                {
                    EditTransactionDialog.ServiceCheckedboxes.Reverse();
                    for (int a = 0; a < ServiceListCheckBox.Items.Count; a++)
                    {
                        if (EditTransactionDialog.ServiceCheckedboxes[a].Checked)
                        {
                            ServiceListCheckBox.Items[a].Checked = true;
                        }
                    }
                }
                else
                {
                    foreach (Control Service in ServiceListControls)
                    {
                        for (int a = 0; a < ServiceListCheckBox.Items.Count; a++)
                        {
                            if (ServiceListCheckBox.Items[a].Text.Equals(Service.Controls[0].Text.Trim()))
                            {
                                ServiceListCheckBox.Items[a].Checked = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
