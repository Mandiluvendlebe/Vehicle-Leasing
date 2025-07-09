namespace VehicleLeasingApplication.Models
{
    public class Driver
    {
        public int DriverID { get; set; }
        public string FullName { get; set; }
        public string LicenseNumber { get; set; }
        public string Contact { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }

}
