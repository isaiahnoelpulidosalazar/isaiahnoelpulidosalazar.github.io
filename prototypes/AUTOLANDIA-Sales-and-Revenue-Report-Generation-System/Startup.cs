using AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.Properties;
using CSSimpleFunctions;
using MaterialSkin.Controls;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class Startup : MaterialForm
    {
        Timer Timer = new Timer();
        bool TabMove = false;
        Point PanelLocation, CursorPosition;

        public Startup()
        {
            InitializeComponent();

            SET_SKIN(this);

            Banner.Image = Resources.autolandia_logo;
            Banner.BackColor = Color.Black;

            Timer.Interval = 1000;
            Timer.Tick += (s, e) =>
            {
                try
                {
                    Timer.Stop();

                    if (!File.Exists("easy_sql.py"))
                    {
                        SimpleFileHandler.ProjectToLocation(Assembly.GetExecutingAssembly(), "easy_sql.py");
                    }
                    if (!File.Exists("autolandia.py"))
                    {
                        SimpleFileHandler.ProjectToLocation(Assembly.GetExecutingAssembly(), "autolandia.py");
                    }

                    PyCS pyCS = new PyCS();
                    pyCS.RunFile("autolandia.py");
                    if (!File.Exists("ft.autlndia"))
                    {
                        SimpleFileHandler.Write("ft.autlndia", "0");
                    }

                    SQL.Open();

                    RecreateGlobalServiceList();
                    ProgressBar.Increment(10);

                    RecreateGlobalAdminList();
                    ProgressBar.Increment(5);

                    RecreateGlobalUserList();
                    ProgressBar.Increment(5);

                    RecreateGlobalPackageList();
                    ProgressBar.Increment(10);

                    RecreateGlobalVehicleModelList();
                    ProgressBar.Increment(10);

                    RecreateGlobalCustomerList();
                    ProgressBar.Increment(10);

                    RecreateGlobalVehicleList();
                    ProgressBar.Increment(10);

                    RecreateGlobalEmployeeList();
                    ProgressBar.Increment(10);
                    
                    RecreateGlobalEmployeeTimeList();
                    ProgressBar.Increment(10);

                    RecreateGlobalTransactionList();
                    ProgressBar.Increment(5);

                    RecreateGlobalBillingList();
                    ProgressBar.Increment(5);

                    RecreateGlobalActivityList();
                    ProgressBar.Increment(10);

                    RecreateGlobalAccountList();

                    new LoginForm(this).Show();
                }
                catch (Exception Exception)
                {
                    Timer.Stop();
                    AlertMessageBox(Exception.Message);
                    Application.Exit();
                }
            };
            Timer.Start();
        }

        private void Startup_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void Banner_MouseDown(object sender, MouseEventArgs e)
        {
            TabMove = true;
            PanelLocation = Location;
            CursorPosition = Cursor.Position;
        }

        private void Banner_MouseMove(object sender, MouseEventArgs e)
        {
            if (TabMove)
            {
                Location = new Point(MousePosition.X - (CursorPosition.X - PanelLocation.X), MousePosition.Y - (CursorPosition.Y - PanelLocation.Y));
            }
        }

        private void Banner_MouseUp(object sender, MouseEventArgs e)
        {
            TabMove = false;
        }
    }
}
