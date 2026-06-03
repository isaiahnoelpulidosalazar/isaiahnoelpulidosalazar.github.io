using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public class GlobalValues
    {
        public static SqliteConnection SQL = new SqliteConnection("Data Source=AUTOLANDIA.db;");

        public static List<ServiceItem> GlobalServiceList = new List<ServiceItem>();
        public static List<PackageItem> GlobalPackageList = new List<PackageItem>();
        public static List<TransactionItem> GlobalTransactionList = new List<TransactionItem>();
        public static List<BillingItem> GlobalBillingList = new List<BillingItem>();
        public static List<VehicleItem> GlobalVehicleList = new List<VehicleItem>();
        public static List<VehicleModelItem> GlobalVehicleModelList = new List<VehicleModelItem>();
        public static List<CustomerItem> GlobalCustomerList = new List<CustomerItem>();
        public static List<EmployeeItem> GlobalEmployeeList = new List<EmployeeItem>();
        public static List<EmployeeTimeItem> GlobalEmployeeTimeList = new List<EmployeeTimeItem>();
        public static List<string> GlobalActivityList = new List<string>();
        public static List<AdminItem> GlobalAdminList = new List<AdminItem>();
        public static List<UserItem> GlobalUserList = new List<UserItem>();
        public static List<AccountItem> GlobalAccountList = new List<AccountItem>();

        public static HomeForm GlobalHomeForm;
        public static PeopleForm GlobalPeopleForm;
        public static ServicesAndPackagesForm GlobalServicesAndPackagesForm;
        public static TransactionsForm GlobalTransactionsForm;
        public static BillingForm GlobalBillingForm;
        public static ReportsForm GlobalReportsForm;
        public static ActivityRecordForm GlobalActivityRecordForm;

        public static AccountItem LoggedAccount;

        public static void SET_SKIN(MaterialForm MaterialForm)
        {
            MaterialSkinManager SkinManager = MaterialSkinManager.Instance;
            SkinManager.AddFormToManage(MaterialForm);
            SkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            SkinManager.ColorScheme = new ColorScheme(0, 0, Primary.Grey500, 0, TextShade.WHITE);
        }

        public class EmployeeNameComparer : IComparer<EmployeeItem>
        {
            public int Compare(EmployeeItem a, EmployeeItem b)
            {
                return $"{a.LastName}, {a.FirstName} {a.MiddleName}".CompareTo($"{b.LastName}, {b.FirstName} {b.MiddleName}");
            }
        }

        public static void RecreateAllGlobalData()
        {
            try
            {
                RecreateGlobalServiceList();
                RecreateGlobalPackageList();
                RecreateGlobalVehicleModelList();
                RecreateGlobalCustomerList();
                RecreateGlobalVehicleList();
                RecreateGlobalEmployeeList();
                RecreateGlobalEmployeeTimeList();
                RecreateGlobalTransactionList();
                RecreateGlobalBillingList();
                RecreateGlobalActivityList();
                RecreateGlobalAdminList();
                RecreateGlobalUserList();
            }
            catch (Exception Exception)
            {
                AlertMessageBox(Exception.Message);
            }
        }

        public static void RecreateGlobalUserList()
        {
            GlobalUserList.Clear();

            SqliteCommand Command = new SqliteCommand("SELECT * FROM AUTOLANDIA_UserList", SQL);

            try
            {
                using (SqliteDataReader Reader = Command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        GlobalUserList.Add(new UserItem(Reader.GetString(0), Reader.GetString(1)));
                    }
                }
            }
            catch (Exception Exception)
            {
                AlertMessageBox(Exception.Message);
            }
        }

        public static void RecreateGlobalAdminList()
        {
            GlobalAdminList.Clear();

            SqliteCommand Command = new SqliteCommand("SELECT * FROM AUTOLANDIA_AdminList", SQL);

            try
            {
                using (SqliteDataReader Reader = Command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        GlobalAdminList.Add(new AdminItem(Reader.GetString(0), Reader.GetString(1)));
                    }
                }
            }
            catch (Exception Exception)
            {
                AlertMessageBox(Exception.Message);
            }
        }

        public static void RecreateGlobalPackageList()
        {
            GlobalPackageList.Clear();

            SqliteCommand Command = new SqliteCommand("SELECT * FROM AUTOLANDIA_PackageList", SQL);

            try
            {
                using (SqliteDataReader Reader = Command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        GlobalPackageList.Add(new PackageItem(Reader.GetString(0), Reader.GetString(1), Reader.GetString(2), Reader.GetDouble(3), Reader.GetDouble(4), Reader.GetDouble(5), Reader.GetDouble(6), Reader.GetDouble(7), Reader.GetString(8)));
                    }
                }
            }
            catch (Exception exception)
            {
                MaterialMessageBox.Show(exception.Message, "Alert");
            }
        }

        public static void RecreateGlobalServiceList()
        {
            GlobalServiceList.Clear();

            SqliteCommand Command = new SqliteCommand("SELECT * FROM AUTOLANDIA_ServiceList", SQL);

            try
            {
                using (SqliteDataReader Reader = Command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        GlobalServiceList.Add(new ServiceItem(Reader.GetString(0), Reader.GetString(1), Reader.GetDouble(2), Reader.GetDouble(3), Reader.GetDouble(4), Reader.GetDouble(5), Reader.GetDouble(6), Reader.GetString(7)));
                    }
                }
            }
            catch (Exception exception)
            {
                MaterialMessageBox.Show(exception.Message, "Alert");
            }
        }

        public static void RecreateGlobalEmployeeList()
        {
            GlobalEmployeeList.Clear();

            SqliteCommand Command = new SqliteCommand("SELECT * FROM AUTOLANDIA_EmployeeList", SQL);

            try
            {
                using (SqliteDataReader Reader = Command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        GlobalEmployeeList.Add(new EmployeeItem(Reader.GetString(0), Reader.GetString(1), Reader.GetString(2), Reader.GetString(3), Reader.GetString(4), Reader.GetString(5), Reader.GetString(6)));
                    }
                }
            }
            catch (Exception exception)
            {
                MaterialMessageBox.Show(exception.Message, "Alert");
            }
        }

        public static void RecreateGlobalEmployeeTimeList()
        {
            GlobalEmployeeTimeList.Clear();

            SqliteCommand Command = new SqliteCommand("SELECT * FROM AUTOLANDIA_EmployeeTimeList", SQL);

            try
            {
                using (SqliteDataReader Reader = Command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        GlobalEmployeeTimeList.Add(new EmployeeTimeItem(Reader.GetString(0), Reader.GetString(1), Reader.GetString(2), Reader.GetString(3)));
                    }
                }
            }
            catch (Exception exception)
            {
                MaterialMessageBox.Show(exception.Message, "Alert");
            }
        }

        public static void RecreateGlobalCustomerList()
        {
            GlobalCustomerList.Clear();

            SqliteCommand Command = new SqliteCommand("SELECT * FROM AUTOLANDIA_CustomerList", SQL);

            try
            {
                using (SqliteDataReader Reader = Command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        GlobalCustomerList.Add(new CustomerItem(Reader.GetString(0), Reader.GetString(1), Reader.GetString(2), Reader.GetString(3), Reader.GetString(4), Reader.GetString(5), Reader.GetString(6)));
                    }
                }
            }
            catch (Exception exception)
            {
                MaterialMessageBox.Show(exception.Message, "Alert");
            }
        }

        public static void RecreateGlobalTransactionList()
        {
            GlobalTransactionList.Clear();

            SqliteCommand Command = new SqliteCommand("SELECT * FROM AUTOLANDIA_TransactionList", SQL);

            try
            {
                using (SqliteDataReader Reader = Command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        GlobalTransactionList.Add(new TransactionItem(Reader.GetString(0), Reader.GetString(1), Reader.GetString(2), Reader.GetString(3), Reader.GetString(4), Reader.GetString(5), Reader.GetString(6), Reader.GetString(7), Reader.GetString(8)));
                    }
                }
            }
            catch (Exception exception)
            {
                MaterialMessageBox.Show(exception.Message, "Alert");
            }
        }

        public static void RecreateGlobalBillingList()
        {
            GlobalBillingList.Clear();

            SqliteCommand Command = new SqliteCommand("SELECT * FROM AUTOLANDIA_BillingList", SQL);

            try
            {
                using (SqliteDataReader Reader = Command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        GlobalBillingList.Add(new BillingItem(Reader.GetString(0), Reader.GetDouble(1), Reader.GetDouble(2), Reader.GetDouble(3), Reader.GetString(4), Reader.GetString(5), Reader.GetString(6)));
                    }
                }
            }
            catch (Exception exception)
            {
                MaterialMessageBox.Show(exception.Message, "Alert");
            }
        }

        public static void RecreateGlobalAccountList()
        {
            GlobalAccountList.Clear();

            try
            {
                RecreateGlobalAdminList();
                RecreateGlobalUserList();

                foreach (AdminItem Admin in GlobalAdminList)
                {
                    GlobalAccountList.Add(new AccountItem(Admin.Username, Admin.Password, true));
                }
                foreach (UserItem User in GlobalUserList)
                {
                    GlobalAccountList.Add(new AccountItem(User.Username, User.Password, false));
                }
            }
            catch (Exception Exception)
            {
                AlertMessageBox(Exception.Message);
            }
        }

        public static double GetServicePrice(string ServiceName, string Size)
        {
            foreach (ServiceItem Service in GlobalServiceList)
            {
                if (Service.Name.Equals(ServiceName))
                {
                    if (Size.Equals("S"))
                    {
                        return Service.S;
                    }
                    if (Size.Equals("M"))
                    {
                        return Service.M;
                    }
                    if (Size.Equals("L"))
                    {
                        return Service.L;
                    }
                    if (Size.Equals("XL"))
                    {
                        return Service.XL;
                    }
                    if (Size.Equals("XXL"))
                    {
                        return Service.XXL;
                    }
                }
            }
            return -1;
        }

        public static SqliteCommand InsertToTransactionList(string EmployeeIdList, string ServiceIdList, string PackageId, string Extras, string VehicleId, string TransactionStatus, string DateUpdated, string DateCreated)
        {
            return new SqliteCommand($"INSERT INTO AUTOLANDIA_TransactionList(TransactionId, EmployeeIdList, ServiceIdList, PackageId, Extras, VehicleId, TransactionStatus, DateUpdated, DateCreated) VALUES ('{GlobalTransactionList.Count + 1}', '{EmployeeIdList}', '{ServiceIdList}', '{PackageId}', '{Extras}', '{VehicleId}', '{TransactionStatus}', '{DateUpdated}', '{DateCreated}')", SQL);
        }

        public static SqliteCommand InsertToBillingList(double TransactionBalance, double TransactionDiscount, double BalancePaid, string BillingStatus, string DateUpdated, string DateCreated)
        {
            return new SqliteCommand($"INSERT INTO AUTOLANDIA_BillingList(BillingId, TransactionBalance, TransactionDiscount, BalancePaid, BillingStatus, DateUpdated, DateCreated) VALUES ('{GlobalTransactionList.Count + 1}', {TransactionBalance}, {TransactionDiscount}, {BalancePaid}, '{BillingStatus}', '{DateUpdated}', '{DateCreated}')", SQL);
        }

        public static SqliteCommand UpdatePaymentInBillingList(string BillingId, double BalancePaid, string BillingStatus, string DateUpdated)
        {
            return new SqliteCommand($"UPDATE AUTOLANDIA_BillingList SET BalancePaid={BalancePaid}, BillingStatus='{BillingStatus}', DateUpdated='{DateUpdated}' WHERE BillingId='{BillingId}'", SQL);
        }

        public static SqliteCommand InsertToAccount(string Username, string Password, int Type)
        {
            return new SqliteCommand($"INSERT INTO AUTOLANDIA_{(Type == 0 ? "Admin" : "User")}List(Username, Password) VALUES ('{Username}', '{Password}')", SQL);
        }

        public static SqliteCommand DeleteAccount(string Username, int Type)
        {
            return new SqliteCommand($"DELETE FROM AUTOLANDIA_{(Type == 0 ? "Admin" : "User")}List WHERE Username='{Username}'", SQL);
        }

        public static void UpdateAccount(string Username, string Password, int Type, int OriginalType)
        {
            new SqliteCommand($"DELETE FROM AUTOLANDIA_{(OriginalType == 0 ? "Admin" : "User")}List WHERE Username='{Username}'", SQL).ExecuteNonQuery();
            new SqliteCommand($"INSERT INTO AUTOLANDIA_{(Type == 0 ? "Admin" : "User")}List(Username, Password) VALUES ('{Username}', '{Password}')", SQL).ExecuteNonQuery();
        }

        public static SqliteCommand ChangePassword(string Username, string New)
        {
            return new SqliteCommand($"UPDATE AUTOLANDIA_AdminList SET Password='{New}' WHERE Username='{Username}'", SQL);
        }

        public static DialogResult OkMessageBox(string Message)
        {
            return MaterialMessageBox.Show(Message, "Notice");
        }

        public static DialogResult NoticeMessageBox(string Message)
        {
            return MaterialMessageBox.Show(Message, "Notice", MessageBoxButtons.YesNo, FlexibleMaterialForm.ButtonsPosition.Right);
        }

        public static DialogResult AlertMessageBox(string Message)
        {
            return MaterialMessageBox.Show(Message, "Alert");
        }

        public static double GetPackagePrice(string PackageName, string Size)
        {
            foreach (PackageItem Package in GlobalPackageList)
            {
                if (Package.Name.Equals(PackageName))
                {
                    if (Size.Equals("S"))
                    {
                        return Package.S;
                    }
                    if (Size.Equals("M"))
                    {
                        return Package.M;
                    }
                    if (Size.Equals("L"))
                    {
                        return Package.L;
                    }
                    if (Size.Equals("XL"))
                    {
                        return Package.XL;
                    }
                    if (Size.Equals("XXL"))
                    {
                        return Package.XXL;
                    }
                }
            }
            return -1;
        }

        public static VehicleItem GetVehicleFromID(string VehicleID)
        {
            foreach (VehicleItem Vehicle in GlobalVehicleList)
            {
                if (Vehicle.ID.Equals(VehicleID))
                {
                    return Vehicle;
                }
            }
            return null;
        }

        public static BillingItem GetBillFromID(string BillingID)
        {
            foreach (BillingItem Bill in GlobalBillingList)
            {
                if (Bill.ID.Equals(BillingID))
                {
                    return Bill;
                }
            }
            return null;
        }

        public static EmployeeItem GetEmployeeFromID(string EmployeeID)
        {
            foreach (EmployeeItem Employee in GlobalEmployeeList)
            {
                if (Employee.ID.Equals(EmployeeID))
                {
                    return Employee;
                }
            }
            return null;
        }

        public static TransactionItem GetTransactionFromID(string TransactionID)
        {
            foreach (TransactionItem Transaction in GlobalTransactionList)
            {
                if (Transaction.ID.Equals(TransactionID))
                {
                    return Transaction;
                }
            }
            return null;
        }

        public static PackageItem GetPackageFromName(string PackageName)
        {
            foreach (PackageItem Package in GlobalPackageList)
            {
                if (Package.Name.Equals(PackageName))
                {
                    return Package;
                }
            }
            return null;
        }

        public static PackageItem GetPackageFromID(string PackageID)
        {
            foreach (PackageItem Package in GlobalPackageList)
            {
                if (Package.ID.Equals(PackageID))
                {
                    return Package;
                }
            }
            return null;
        }

        public static void RecreateGlobalVehicleList()
        {
            GlobalVehicleList.Clear();

            SqliteCommand Command = new SqliteCommand("SELECT * FROM AUTOLANDIA_VehicleList", SQL);

            try
            {
                using (SqliteDataReader Reader = Command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        GlobalVehicleList.Add(new VehicleItem(Reader.GetString(0), Reader.GetString(1), Reader.GetString(2), Reader.GetString(3), Reader.GetString(4), Reader.GetString(5)));
                    }
                }
            }
            catch (Exception exception)
            {
                MaterialMessageBox.Show(exception.Message, "Alert");
            }
        }

        public static void RecreateGlobalVehicleModelList()
        {
            GlobalVehicleModelList.Clear();

            string[] AllVehicleModels = ConstantVehicleModelList.VehicleModelList.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string VehicleModel in AllVehicleModels)
            {
                string[] Split = VehicleModel.Split(':');
                GlobalVehicleModelList.Add(new VehicleModelItem(Split[0].Trim(), Split[1].Trim()));
            }
            GlobalVehicleModelList.Add(new VehicleModelItem("GENERAL", "EBIKE"));
            GlobalVehicleModelList.Add(new VehicleModelItem("GENERAL", "MOTOR"));
            GlobalVehicleModelList.Add(new VehicleModelItem("GENERAL", "PUV"));
            GlobalVehicleModelList.Add(new VehicleModelItem("GENERAL", "TRICYCLE"));
            GlobalVehicleModelList.Add(new VehicleModelItem("GENERAL", "TRUCK"));
        }

        public static void RecreateGlobalActivityList()
        {
            GlobalActivityList.Clear();

            SqliteCommand Command = new SqliteCommand("SELECT * FROM AUTOLANDIA_ActivityList ORDER BY ActivityMessage DESC", SQL);

            try
            {
                using (SqliteDataReader Reader = Command.ExecuteReader())
                {
                    while (Reader.Read())
                    {
                        GlobalActivityList.Add(Reader.GetString(0));
                    }
                }
            }
            catch (Exception exception)
            {
                MaterialMessageBox.Show(exception.Message, "Alert");
            }
        }

        public static void RecordActivity(string Message)
        {
            DateTime Now = DateTime.Now;
            SqliteCommand Command = new SqliteCommand($"INSERT INTO AUTOLANDIA_ActivityList(ActivityMessage) VALUES ('{$"{Now.ToString("yyyy")}/{Now.ToString("MM")}/{Now.ToString("dd")}" + $" {Now.ToString("HH")}:{Now.ToString("mm")}:{Now.ToString("ss")} {Now.ToString("tt")}" + " - " + Message}')", SQL);

            try
            {
                Command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                MaterialMessageBox.Show(exception.Message, "Alert");
            }
        }
    }
}
