namespace hangfire_jobs_database.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public List<Address> Addresses { get; set; }

    }
}
