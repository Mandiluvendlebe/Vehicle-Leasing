namespace VehicleLeasingApplication.Models
{
    public class Vehicle
    {
        public int VehicleID { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string RegistrationNumber { get; set; }

        public int SupplierID { get; set; }
        public int BranchID { get; set; }
        public int ClientID { get; set; }
        public int DriverID { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Client Client { get; set; }
        public virtual Driver Driver { get; set; }
    }

}
