using MaterialSkin.Controls;
using System;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class PaymentDialog : MaterialForm
    {
        NewTransactionDialog NewTransactionDialog;
        AddCustomerVehicleDialog AddCustomerVehicleDialog;
        NewCustomerDialog NewCustomerDialog;
        BillDetailDialog BillDetailDialog;
        string BillingId;
        BillingItem Bill;

        public PaymentDialog(string BillingId)
        {
            InitializeComponent();

            this.BillingId = BillingId;

            Bill = GetBillFromID(BillingId.ToString());
        }

        public PaymentDialog(BillDetailDialog BillDetailDialog, string BillingId)
        {
            InitializeComponent();

            this.BillDetailDialog = BillDetailDialog;
            this.BillingId = BillingId;

            Bill = GetBillFromID(BillingId.ToString());
        }

        public PaymentDialog(NewTransactionDialog NewTransactionDialog, AddCustomerVehicleDialog AddCustomerVehicleDialog, NewCustomerDialog NewCustomerDialog, string BillingId)
        {
            InitializeComponent();

            this.NewTransactionDialog = NewTransactionDialog;
            this.AddCustomerVehicleDialog = AddCustomerVehicleDialog;
            this.NewCustomerDialog = NewCustomerDialog;
            this.BillingId = BillingId;

            Bill = GetBillFromID(BillingId.ToString());
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(TB_Amount.Text) > 0)
                {
                    if (double.Parse(TB_Amount.Text) >= Bill.Balance)
                    {
                        double PaidAmount = double.Parse(TB_Amount.Text);
                        double NewBalance = (Bill.Balance - Bill.Paid) - double.Parse(TB_Amount.Text);
                        string Status = NewBalance <= 0 ? "Paid" : "Pending";
                        UpdatePaymentInBillingList(
                            BillingId,
                            PaidAmount + Bill.Paid,
                            Status,
                            $"{DateTime.Now.ToString("yyyy")}/{DateTime.Now.ToString("MM")}/{DateTime.Now.ToString("dd")}" + $" {DateTime.Now.ToString("HH")}:{DateTime.Now.ToString("mm")}:{DateTime.Now.ToString("ss")} {DateTime.Now.ToString("tt")}"
                        ).ExecuteNonQuery();

                        RecordActivity($"Updated bill details with reference number: [{BillingId}]");

                        GlobalBillingForm.RefreshBillings();
                        GlobalActivityRecordForm.RefreshActivities();

                        OkMessageBox($"Successfully updated bill details!{(NewBalance <= 0 ? " Please give customer change immediately if present." : "")}");

                        new ReceiptDialog(BillingId, PaidAmount).ShowDialog();

                        Close();
                    }
                    else
                    {
                        AlertMessageBox("Please enter an amount equal to or higher than the current balance.");
                    }
                }
                else
                {
                    AlertMessageBox("Please enter a valid amount.");
                }
            }
            catch (Exception Exception)
            {
                AlertMessageBox(Exception.Message);
            }
        }

        private void PaymentDialog_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (AddCustomerVehicleDialog != null)
            {
                AddCustomerVehicleDialog.Close();
            }
            if (NewCustomerDialog != null)
            {
                NewCustomerDialog.Close();
            }
            if (NewTransactionDialog != null)
            {
                NewTransactionDialog.Close();
            }
            if (BillDetailDialog != null)
            {
                BillDetailDialog.Close();
            }
        }

        private void PaymentDialog_Load(object sender, EventArgs e)
        {
            BillID.Text = $"Payment for bill number {BillingId}";
            TB_Balance.Text = $"₱{(Bill.Balance - Bill.Paid).ToString("N2")}";
            TB_Change.Text = $"₱{(0).ToString("N2")}";
        }

        private void TB_Amount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (double.Parse(TB_Amount.Text) > 0)
                {
                    TB_Balance.Text = (Bill.Balance - Bill.Paid) - double.Parse(TB_Amount.Text) > 0 ? $"₱{((Bill.Balance - Bill.Paid) - double.Parse(TB_Amount.Text)).ToString("N2")}" : $"₱{(0).ToString("N2")}";
                    TB_Change.Text = double.Parse(TB_Amount.Text) - (Bill.Balance - Bill.Paid) > 0 ? $"₱{(double.Parse(TB_Amount.Text) - (Bill.Balance - Bill.Paid)).ToString("N2")}" : $"₱{(0).ToString("N2")}";
                }
                else
                {
                    TB_Balance.Text = $"₱{Bill.Balance.ToString("N2")}";
                    TB_Change.Text = $"₱{(0).ToString("N2")}";
                }
            }
            catch
            {
                TB_Balance.Text = $"₱{Bill.Balance.ToString("N2")}";
                TB_Change.Text = $"₱{(0).ToString("N2")}";
            }
        }
    }
}
