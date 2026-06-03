namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public class AdminItem
    {
        string username, password;

        public AdminItem(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
