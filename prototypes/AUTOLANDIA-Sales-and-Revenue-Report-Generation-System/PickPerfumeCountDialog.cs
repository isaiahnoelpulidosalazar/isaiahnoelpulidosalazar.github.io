using CSSimpleFunctions;
using MaterialSkin.Controls;
using System;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class PickPerfumeCountDialog : MaterialForm
    {
        NewTransactionDialog NewTransactionDialog;
        TransactionDetailDialog EditTransactionDialog;

        public PickPerfumeCountDialog(NewTransactionDialog NewTransactionDialog)
        {
            InitializeComponent();

            this.NewTransactionDialog = NewTransactionDialog;
        }

        public PickPerfumeCountDialog(TransactionDetailDialog EditTransactionDialog)
        {
            InitializeComponent();

            this.EditTransactionDialog = EditTransactionDialog;
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            if (Check.IsAllNumbers(TB_PC.Text))
            {
                try
                {
                    if (int.Parse(TB_PC.Text) > 0)
                    {
                        if (EditTransactionDialog != null)
                        {
                            EditTransactionDialog.PickPerfumeCount(int.Parse(TB_PC.Text));
                        }
                        if (NewTransactionDialog != null)
                        {
                            NewTransactionDialog.PickPerfumeCount(int.Parse(TB_PC.Text));
                        }
                        Close();
                    }
                    else
                    {
                        AlertMessageBox("Please enter a valid amount.");
                    }
                }
                catch
                {
                    AlertMessageBox("Please enter a valid amount.");
                }
            }
            else
            {
                AlertMessageBox("Please enter a valid amount.");
            }
        }

        private void PickPerfumeCountDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Check.IsAllNumbers(TB_PC.Text))
            {
                try
                {
                    if (int.Parse(TB_PC.Text) > 0)
                    {
                        if (EditTransactionDialog != null)
                        {
                            EditTransactionDialog.PickPerfumeCount(int.Parse(TB_PC.Text));
                        }
                        if (NewTransactionDialog != null)
                        {
                            NewTransactionDialog.PickPerfumeCount(int.Parse(TB_PC.Text));
                        }
                    }
                    else
                    {
                        AlertMessageBox("Please enter a valid amount.");
                        e.Cancel = true;
                    }
                }
                catch
                {
                    AlertMessageBox("Please enter a valid amount.");
                    e.Cancel = true;
                }
            }
            else
            {
                AlertMessageBox("Please enter a valid amount.");
                e.Cancel = true;
            }
        }
    }
}
