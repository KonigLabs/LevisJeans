namespace KonigLabs.LevisJeans.Models.Entities
{
    public class Test
    {
        public int Id { get; set; }
        public bool Answer1 { get; set; }
        public bool Answer2 { get; set; }
        public bool Answer3 { get; set; }
        public bool Answer4 { get; set; }

        public virtual Customer Customer { get; set; }
    }
}