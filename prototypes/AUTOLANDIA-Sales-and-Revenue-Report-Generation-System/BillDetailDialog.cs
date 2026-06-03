using MaterialSkin.Controls;
using System;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class BillDetailDialog : MaterialForm
    {
        BillingForm BillingForm;
        string BillingID;
        BillingItem Bill;

        public BillDetailDialog(BillingForm BillingForm, string BillingID)
        {
            InitializeComponent();

            this.BillingForm = BillingForm;
            this.BillingID = BillingID;
            Bill = GetBillFromID(BillingID);
            TransactionItem Transaction = GetTransactionFromID(BillingID);

            foreach (BillingItem Billing in GlobalBillingList)
            {
                if (Billing.ID.Equals(BillingID))
                {
                    TB_ID.Text = Billing.ID;
                    //TB_Price.Text = Billing.Balance - Billing.Paid > 0? $"₱{(Billing.Balance - Billing.Paid).ToString("0.00")}" : "₱0.00";
                    TB_Price.Text = $"₱{Billing.Balance.ToString("N2")}";
                    if (Transaction.Status.Equals("Complete"))
                    {
                        if (Billing.Balance - Billing.Paid <= 0)
                        {
                            PayButton.Text = "Paid";
                            PayButton.Enabled = false;
                            ReceiptButton.Visible = true;
                        }
                    }
                    else
                    {
                        PayButton.Text = "Can only pay if transaction status is complete";
                        PayButton.Enabled = false;
                    }
                    L_Discount.Text = $"{Billing.Discount}%";
                    TB_LastUpdated.Text = Billing.LastUpdated;
                    TB_DateCreated.Text = Billing.DateCreated;
                }
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void PayButton_Click(object sender, EventArgs e)
        {
            new PaymentDialog(this, BillingID).ShowDialog();
        }

        private void ReceiptButton_Click(object sender, EventArgs e)
        {
            new ReceiptDialog(BillingID, Bill.Paid).ShowDialog();
        }
    }
}
