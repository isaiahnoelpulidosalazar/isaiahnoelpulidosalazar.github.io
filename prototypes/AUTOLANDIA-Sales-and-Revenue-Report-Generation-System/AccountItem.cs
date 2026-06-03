namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public class AccountItem
    {
        string username, password;
        bool isAdmin;

        public AccountItem(string username, string password, bool isAdmin)
        {
            this.username = username;
            this.password = password;
            this.isAdmin = isAdmin;
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

        public bool IsAdmin
        {
            get { return isAdmin; }
            set { isAdmin = value; }
        }
    }
}
