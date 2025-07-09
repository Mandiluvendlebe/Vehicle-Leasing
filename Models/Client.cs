namespace VehicleLeasingApplication.Models
{
    public class Client
    {
        public int ClientID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }

        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }

}
