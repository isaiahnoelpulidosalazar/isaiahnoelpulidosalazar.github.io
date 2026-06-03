namespace AUTOLANDIA_Sales_and_Revenue_Report_Generation_System
{
    public class VehicleItem
    {
        string id, brand, model, size, plateNumber, customerID;

        public VehicleItem(string id, string brand, string model, string size, string plateNumber, string customerID)
        {
            this.id = id;
            this.brand = brand;
            this.model = model;
            this.size = size;
            this.plateNumber = plateNumber;
            this.customerID = customerID;
        }

        public string ID
        {
            get { return id; }
            set { id = value; }
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

        public string Size
        {
            get { return size; }
            set { size = value; }
        }

        public string PlateNumber
        {
            get { return plateNumber; }
            set { plateNumber = value; }
        }

        public string CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }
    }
}
