namespace VehicleLeasingApplication.Models
{
    public class Branch
    {
        public int BranchID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }

}
