namespace hangfire_jobs_database.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Cep { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string State { get; set; }
        public string Neightborhood { get; set; }
        public string Country { get; set; }
        public User User { get; set; }
    }
}
