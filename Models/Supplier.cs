using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VehicleLeasingApplication.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Location { get; set; }

        [ValidateNever]
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }

}
