using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace VehicleLeasingApplication.Models
{
    public class Vehicle
    {
        public int VehicleID { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        [Required]
        public int SupplierID { get; set; }

        [Required]
        public int BranchID { get; set; }

        [Required]
        public int ClientID { get; set; }

        [Required]
        public int DriverID { get; set; }

        [ValidateNever]
        public virtual Supplier Supplier { get; set; }
        [ValidateNever]
        public virtual Branch Branch { get; set; }
        [ValidateNever]
        public virtual Client Client { get; set; }
        [ValidateNever]
        public virtual Driver Driver { get; set; }
    }

}
