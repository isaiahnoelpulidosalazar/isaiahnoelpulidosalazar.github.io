namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public class VehicleModelItem
    {
        string brand, model;

        public VehicleModelItem(string brand, string model)
        {
            this.brand = brand;
            this.model = model;
        }

        public string Brand
        {
            get { return brand; }
            set { brand = value; }
        }

        public string Model
        {
            get { return model; }
            set { model = value; }
        }
    }
}
