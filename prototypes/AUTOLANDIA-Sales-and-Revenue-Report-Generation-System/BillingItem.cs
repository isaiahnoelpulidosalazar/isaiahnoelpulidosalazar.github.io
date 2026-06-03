namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public class BillingItem
    {
        string id, status, lastUpdated, dateCreated;
        double transactionBalance, discount, paid;

        public BillingItem(string id, double transactionBalance, double discount, double paid, string status, string lastUpdated, string dateCreated)
        {
            this.id = id;
            this.transactionBalance = transactionBalance;
            this.discount = discount;
            this.paid = paid;
            this.status = status;
            this.lastUpdated = lastUpdated;
            this.dateCreated = dateCreated;
        }

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public double Balance
        {
            get { return transactionBalance; }
            set { transactionBalance = value; }
        }

        public double Discount
        {
            get { return discount; }
            set { discount = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public double Paid
        {
            get { return paid; }
            set { paid = value; }
        }

        public string LastUpdated
        {
            get { return lastUpdated; }
            set { lastUpdated = value; }
        }

        public string DateCreated
        {
            get { return dateCreated; }
            set { dateCreated = value; }
        }
    }
}
