using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VehicleLeasingApplication.Models
{
    public class Branch
    {
        public int BranchID { get; set; }
        public required string Name { get; set; }
        public required string Location { get; set; }

        [ValidateNever]
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }

}
