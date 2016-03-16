namespace KonigLabs.LevisJeans.Models.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Phrase { get; set; }

        public bool Checked { get; set; }

        public virtual Test Answers { get; set; }
    }
}