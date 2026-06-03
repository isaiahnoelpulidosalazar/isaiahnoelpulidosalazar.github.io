using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class SelectEmployeeDialog : MaterialForm
    {
        List<EmployeeItem> Temp = new List<EmployeeItem>();
        List<string> TempTime = new List<string>();
        NewTransactionDialog NewTransactionDialog;
        TransactionDetailDialog EditTransactionDialog;
        string EmployeeList;

        public SelectEmployeeDialog(NewTransactionDialog NewTransactionDialog)
        {
            InitializeComponent();

            this.NewTransactionDialog = NewTransactionDialog;

            foreach (EmployeeItem Employee in GlobalEmployeeList)
            {
                Temp.Add(Employee);
            }
            foreach (EmployeeTimeItem EmployeeTime in GlobalEmployeeTimeList)
            {
                if (DateTime.Parse(EmployeeTime.TimeIn).Date.Equals(DateTime.Now.Date))
                {
                    TempTime.Add(EmployeeTime.ID);
                }
            }
            Temp.Reverse();

            if (GlobalEmployeeTimeList.Count < 1)
            {
                Temp.Clear();
            }
            for (int counter = 0; counter < Temp.Count; counter++)
            {
                string EmployeeDetail = $"{Temp[counter].ID}: {Temp[counter].LastName}, {Temp[counter].FirstName} {Temp[counter].MiddleName}";
                foreach (TransactionItem Order in GlobalTransactionList)
                {
                    string[] Split = Order.EmployeeIDList.Substring(1, Order.EmployeeIDList.Length - 2).Split(',');
                    if (Split.Contains(Temp[counter].ID))
                    {
                        if (!Order.Status.Equals("Complete") && !Order.Status.Equals("Cancelled"))
                        {
                            Temp.Remove(Temp[counter]);
                            counter = 0;
                            break;
                        }
                    }
                }
                if (Temp.Count > 0)
                {
                    foreach (EmployeeTimeItem EmployeeTime in GlobalEmployeeTimeList)
                    {
                        if (EmployeeTime.ID.Equals(Temp[counter].ID))
                        {
                            if (!string.IsNullOrEmpty(EmployeeTime.TimeOut))
                            {
                                Temp.Remove(Temp[counter]);
                                counter = 0;
                                break;
                            }
                        }
                    }
                }
                if (Temp.Count > 0)
                {
                    if (!TempTime.Contains(Temp[counter].ID))
                    {
                        Temp.Remove(Temp[counter]);
                        counter--;
                    }
                }
            }
            foreach (EmployeeItem Employee in Temp)
            {
                string EmployeeDetail = $"{Employee.ID}: {Employee.LastName}, {Employee.FirstName} {Employee.MiddleName}";
                if (!InCheckedListBox(EmployeeDetail))
                {
                    EmployeeListCheckBox.Items.Add(EmployeeDetail);
                }
            }

            if (NewTransactionDialog.EmployeeCheckedboxes != null)
            {
                NewTransactionDialog.EmployeeCheckedboxes.Reverse();
                for (int a = 0; a < EmployeeListCheckBox.Items.Count; a++)
                {
                    if (NewTransactionDialog.EmployeeCheckedboxes[a].Checked)
                    {
                        EmployeeListCheckBox.Items[a].Checked = true;
                    }
                }
            }
        }

        public SelectEmployeeDialog(TransactionDetailDialog EditTransactionDialog, string EmployeeList)
        {
            InitializeComponent();

            this.EditTransactionDialog = EditTransactionDialog;
            this.EmployeeList = EmployeeList;

            //Temp.Sort(new EmployeeNameComparer());
            Temp.Reverse();
            int counter = 0;

            while (counter < Temp.Count)
            {
                string EmployeeDetail = $"{Temp[counter].ID}: {Temp[counter].LastName}, {Temp[counter].FirstName} {Temp[counter].MiddleName}";
                foreach (TransactionItem Order in GlobalTransactionList)
                {
                    string[] Split = Order.EmployeeIDList.Substring(1, Order.EmployeeIDList.Length - 2).Split(',');
                    if (Split.Contains(Temp[counter].ID))
                    {
                        if (!Order.Status.Equals("Complete") && !Order.Status.Equals("Cancelled"))
                        {
                            Temp.Remove(Temp[counter]);
                            counter = 0;
                            break;
                        }
                    }
                }
                counter++;
            }
            foreach (EmployeeItem Employee in Temp)
            {
                string EmployeeDetail = $"{Employee.ID}: {Employee.LastName}, {Employee.FirstName} {Employee.MiddleName}";
                if (!InCheckedListBox(EmployeeDetail))
                {
                    EmployeeListCheckBox.Items.Add(EmployeeDetail);
                }
            }

            if (EditTransactionDialog.EmployeeCheckedboxes != null)
            {
                EditTransactionDialog.EmployeeCheckedboxes.Reverse();
                for (int a = 0; a < EmployeeListCheckBox.Items.Count; a++)
                {
                    if (EditTransactionDialog.EmployeeCheckedboxes[a].Checked)
                    {
                        EmployeeListCheckBox.Items[a].Checked = true;
                    }
                }
            }
            else
            {
                string[] EmployeeListSplit = EmployeeList.Split(',');
                foreach (string Employee in EmployeeListSplit)
                {
                    for (int a = 0; a < EmployeeListCheckBox.Items.Count; a++)
                    {
                        if (EmployeeListCheckBox.Items[a].Text.Equals(Employee.Trim()))
                        {
                            EmployeeListCheckBox.Items[a].Checked = true;
                        }
                    }
                }
            }
        }

        bool InCheckedListBox(string text)
        {
            foreach (MaterialCheckbox Item in EmployeeListCheckBox.Items)
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
                NewTransactionDialog.SetEmployees(EmployeeListCheckBox.Items);
            }
            if (EditTransactionDialog != null)
            {
                EditTransactionDialog.SetEmployees(EmployeeListCheckBox.Items);
            }
            Close();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SelectEmployeeDialog_Load(object sender, EventArgs e)
        {
            Timer Timer = new Timer();
            Timer.Interval = 1;
            Timer.Tick += (s, evt) =>
            {
                EmployeeListCheckBox.AutoScrollPosition = new Point(0, 0);
                Timer.Stop();
            };
            Timer.Start();
        }

        private void SelectEmployeeDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (NewTransactionDialog != null)
            {
                if (NewTransactionDialog.EmployeeCheckedboxes != null)
                {
                    NewTransactionDialog.EmployeeCheckedboxes.Reverse();
                }
            }
            if (EditTransactionDialog != null)
            {
                if (EditTransactionDialog.EmployeeCheckedboxes != null)
                {
                    EditTransactionDialog.EmployeeCheckedboxes.Reverse();
                }
            }
        }

        private void SearchBarEmployee_TextChanged(object sender, EventArgs e)
        {
            EmployeeListCheckBox.Items.Clear();
            EmployeeListCheckBox.Controls.Clear();

            if (NewTransactionDialog != null)
            {
                Temp.Sort(new EmployeeNameComparer());
                Temp.Reverse();

                foreach (EmployeeItem Employee in Temp)
                {
                    string EmployeeDetail = $"{Employee.ID}: {Employee.LastName}, {Employee.FirstName} {Employee.MiddleName}";
                    if (!InCheckedListBox(EmployeeDetail))
                    {
                        if ($"{Employee.LastName}, {Employee.FirstName} {Employee.MiddleName}".ToUpper().Contains(SearchBarEmployee.Text.ToUpper()))
                        {
                            EmployeeListCheckBox.Items.Add(EmployeeDetail);
                        }
                        else if (string.IsNullOrEmpty(SearchBarEmployee.Text.ToUpper()))
                        {
                            EmployeeListCheckBox.Items.Add(EmployeeDetail);
                        }
                    }
                }

                if (NewTransactionDialog.EmployeeCheckedboxes != null)
                {
                    NewTransactionDialog.EmployeeCheckedboxes.Reverse();
                    for (int a = 0; a < EmployeeListCheckBox.Items.Count; a++)
                    {
                        if (NewTransactionDialog.EmployeeCheckedboxes[a].Checked)
                        {
                            EmployeeListCheckBox.Items[a].Checked = true;
                        }
                    }
                }
            }
            if (EditTransactionDialog != null)
            {
                Temp.Sort(new EmployeeNameComparer());
                Temp.Reverse();

                foreach (EmployeeItem Employee in Temp)
                {
                    string EmployeeDetail = $"{Employee.ID}: {Employee.LastName}, {Employee.FirstName} {Employee.MiddleName}";
                    if (!InCheckedListBox(EmployeeDetail))
                    {
                        if ($"{Employee.LastName}, {Employee.FirstName} {Employee.MiddleName}".ToUpper().Contains(SearchBarEmployee.Text.ToUpper()))
                        {
                            EmployeeListCheckBox.Items.Add(EmployeeDetail);
                        }
                        else if (string.IsNullOrEmpty(SearchBarEmployee.Text.ToUpper()))
                        {
                            EmployeeListCheckBox.Items.Add(EmployeeDetail);
                        }
                    }
                }

                if (EditTransactionDialog.EmployeeCheckedboxes != null)
                {
                    EditTransactionDialog.EmployeeCheckedboxes.Reverse();
                    for (int a = 0; a < EmployeeListCheckBox.Items.Count; a++)
                    {
                        if (EditTransactionDialog.EmployeeCheckedboxes[a].Checked)
                        {
                            EmployeeListCheckBox.Items[a].Checked = true;
                        }
                    }
                }
                else
                {
                    string[] EmployeeListSplit = EmployeeList.Split(',');
                    foreach (string Employee in EmployeeListSplit)
                    {
                        for (int a = 0; a < EmployeeListCheckBox.Items.Count; a++)
                        {
                            if (EmployeeListCheckBox.Items[a].Text.Equals(Employee.Trim()))
                            {
                                EmployeeListCheckBox.Items[a].Checked = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
