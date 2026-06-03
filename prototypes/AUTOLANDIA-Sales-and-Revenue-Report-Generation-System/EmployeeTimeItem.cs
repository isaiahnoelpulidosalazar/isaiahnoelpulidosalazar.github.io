namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public class EmployeeTimeItem
    {
        string id, timeIn, timeOut, dateCreated;

        public EmployeeTimeItem(string id, string timeIn, string timeOut, string dateCreated)
        {
            this.id = id;
            this.timeIn = timeIn;
            this.timeOut = timeOut;
            this.dateCreated = dateCreated;
        }

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public string TimeIn
        {
            get { return timeIn; }
            set { timeIn = value; }
        }

        public string TimeOut
        {
            get { return timeOut; }
            set { timeOut = value; }
        }

        public string DateCreated
        {
            get { return dateCreated; }
            set { dateCreated = value; }
        }
    }
}
