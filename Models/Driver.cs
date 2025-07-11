using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VehicleLeasingApplication.Models
{
    public class Driver
    {
        public int DriverID { get; set; }
        public string FullName { get; set; }
        public string LicenseNumber { get; set; }
        public string Contact { get; set; }

        [ValidateNever]
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }

}
