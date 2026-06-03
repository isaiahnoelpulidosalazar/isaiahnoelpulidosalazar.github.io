using MaterialSkin.Controls;
using Microsoft.Data.Sqlite;
using System;
using static AUTOLANDIA_Sales_and_Revenue_Report_Generation_System.GlobalValues;

namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public partial class EditPackageDialog : MaterialForm
    {
        ServicesAndPackagesForm ServicesAndPackagesForm;
        string PackageID;

        public EditPackageDialog(ServicesAndPackagesForm ServicesAndPackagesForm, string PackageID)
        {
            InitializeComponent();

            this.ServicesAndPackagesForm = ServicesAndPackagesForm;
            this.PackageID = PackageID;

            foreach (PackageItem Package in GlobalPackageList)
            {
                if (Package.ID.Equals(PackageID))
                {
                    TB_ID.Text = Package.ID;
                    TB_Name.Text = Package.Name;
                    TB_Services.Text = Package.Details;
                    TB_PriceS.Text = Package.S.ToString();
                    TB_PriceM.Text = Package.M.ToString();
                    TB_PriceL.Text = Package.L.ToString();
                    TB_PriceXL.Text = Package.XL.ToString();
                    TB_PriceXXL.Text = Package.XXL.ToString();
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

                RecordActivity($"Updated package [{PackageID}] details");

                SqliteCommand Command = new SqliteCommand($"UPDATE AUTOLANDIA_PackageList SET PackagePriceSizeS={PriceS}, PackagePriceSizeM={PriceM}, PackagePriceSizeL={PriceL}, PackagePriceSizeXL={PriceXL}, PackagePriceSizeXXL={PriceXXL} WHERE PackageId='{PackageID}'", SQL);

                Command.ExecuteNonQuery();

                OkMessageBox("Successfully updated package details!");
                ServicesAndPackagesForm.RefreshPackages();
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
