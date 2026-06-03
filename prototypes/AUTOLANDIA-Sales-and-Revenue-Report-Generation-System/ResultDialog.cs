using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class ResultDialog : MaterialForm
    {
        NewTransactionDialog NewTransactionDialog;
        object[] args;
        bool Back = false;

        public ResultDialog(NewTransactionDialog NewTransactionDialog, List<ServiceItem> Services, string Package, string Extras, object[] args)
        {
            InitializeComponent();

            this.NewTransactionDialog = NewTransactionDialog;

            string ServiceNames = "[Services] ";
            foreach (var service in Services)
            {
                ServiceNames += service.Name + ", ";
            }
            ServiceNames = ServiceNames.TrimEnd(',', ' ');

            string Extra = "[Add-ons] ";
            if (Extras.Contains('C'))
            {
                Extra += $"Car w/ Carrier, ";
            }
            if (Extras.Contains("P"))
            {
                Extra += $"{Extras.Split('P')[0]} Perfume(s), ";
            }
            Extra = Extra.TrimEnd(',', ' ');

            L_Service.Text = $"{(ServiceNames.Equals("[Services]") ? string.Empty : ServiceNames)} {Package}";
            L_Service.Text = L_Service.Text.EndsWith(" [ - ]") ? L_Service.Text.Split(new string[] { " [ - ]" }, StringSplitOptions.None)[0] : L_Service.Text;
            L_Extras.Text = Extra.Equals("[Add-ons] ".TrimEnd(',', ' ')) ? "None" : Extra;
            L_Total.Text = (int)args[8] > 0 ? $"₱{((double)args[7]).ToString("N2")} ({args[8]}% discount - ₱{((double)args[6]).ToString("N2")})" : $"₱{((double)args[7]).ToString("N2")}";
            this.args = args;
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ResultDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Back)
            {
                NewTransactionDialog.FromResultDialog((string)args[0], (string)args[1], (string)args[2], (string)args[3], (VehicleItem)args[4], (DateTime)args[5], (double)args[6]);
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            Back = true;
            Close();
        }
    }
}
