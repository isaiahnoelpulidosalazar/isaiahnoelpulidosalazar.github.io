using MaterialSkin.Controls;
using Microsoft.Data.Sqlite;
using System;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class EditServiceDialog : MaterialForm
    {
        ServicesAndPackagesForm ServicesAndPackagesForm;
        string ServiceID;

        public EditServiceDialog(ServicesAndPackagesForm ServicesAndPackagesForm, string ServiceID)
        {
            InitializeComponent();

            this.ServicesAndPackagesForm = ServicesAndPackagesForm;
            this.ServiceID = ServiceID;

            foreach (ServiceItem Service in GlobalServiceList)
            {
                if (Service.ID.Equals(ServiceID))
                {
                    TB_ID.Text = Service.ID;
                    TB_Name.Text = Service.Name;
                    TB_PriceS.Text = Service.S.ToString();
                    TB_PriceM.Text = Service.M.ToString();
                    TB_PriceL.Text = Service.L.ToString();
                    TB_PriceXL.Text = Service.XL.ToString();
                    TB_PriceXXL.Text = Service.XXL.ToString();
                }
            }
        }

        private void DoneButton_Click(object sender, EventArgs e)
        {
            try
            {
                string PriceS = TB_PriceS.Text.ToUpper();
                string PriceM = TB_PriceM.Text.ToUpper();
                string PriceL = TB_PriceL.Text.ToUpper();
                string PriceXL = TB_PriceXL.Text.ToUpper();
                string PriceXXL = TB_PriceXXL.Text.ToUpper();

                DoneButton.Enabled = false;
                CancelButton.Enabled = false;

                RecordActivity($"Updated service [{ServiceID}] details");

                SqliteCommand Command = new SqliteCommand($"UPDATE AUTOLANDIA_ServiceList SET ServicePriceSizeS={PriceS}, ServicePriceSizeM={PriceM}, ServicePriceSizeL={PriceL}, ServicePriceSizeXL={PriceXL}, ServicePriceSizeXXL={PriceXXL} WHERE ServiceId='{ServiceID}'", SQL);

                Command.ExecuteNonQuery();

                OkMessageBox("Successfully updated service details!");
                ServicesAndPackagesForm.RefreshServices();
                GlobalActivityRecordForm.RefreshActivities();
                Close();
            }
            catch (Exception Exception)
            {
                AlertMessageBox(Exception.Message);
                DoneButton.Enabled = true;
                CancelButton.Enabled = true;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
