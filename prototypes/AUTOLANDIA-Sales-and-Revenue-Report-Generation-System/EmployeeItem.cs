namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public class EmployeeItem
    {
        string id, fname, mobileNumber, address, lname, mname, documents;

        public EmployeeItem(string id, string lname, string fname, string mname, string documents, string mobileNumber, string address)
        {
            this.id = id;
            this.lname = lname;
            this.fname = fname;
            this.mname = mname;
            this.documents = documents;
            this.mobileNumber = mobileNumber;
            this.address = address;
        }

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public string FirstName
        {
            get { return fname; }
            set { fname = value; }
        }

        public string MobileNumber
        {
            get { return mobileNumber; }
            set { mobileNumber = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        public string LastName
        {
            get { return lname; }
            set { lname = value; }
        }

        public string MiddleName
        {
            get { return mname; }
            set { mname = value; }
        }

        public string Documents
        {
            get { return documents; }
            set { documents = value; }
        }
    }
}
