namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public class ServiceItem
    {
        string id, name, status;
        double s, m, l, xl, xxl;

        public ServiceItem(string id, string name, double s, double m, double l, double xl, double xxl, string status)
        {
            this.id = id;
            this.name = name;
            this.s = s;
            this.m = m;
            this.l = l;
            this.xl = xl;
            this.xxl = xxl;
            this.status = status;
        }

        public string ID
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public double S
        {
            get { return s; }
            set { s = value; }
        }
        
        public double M
        {
            get { return m; }
            set { m = value; }
        }

        public double L
        {
            get { return l; }
            set { l = value; }
        }

        public double XL
        {
            get { return xl; }
            set { xl = value; }
        }

        public double XXL
        {
            get { return xxl; }
            set { xxl = value; }
        }
    }
}
