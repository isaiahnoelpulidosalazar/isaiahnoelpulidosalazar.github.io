namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public class UserItem
    {
        string username, password;

        public UserItem(string username, string password)
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
