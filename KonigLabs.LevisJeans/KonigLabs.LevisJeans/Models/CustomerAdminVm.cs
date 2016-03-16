using System.Xml.Serialization;

namespace KonigLabs.LevisJeans.Models
{
    [XmlRoot(ElementName = "Customer")]
    public class CustomerAdminVm
    {
        [XmlElement(ElementName = "Number")]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Phrase { get; set; }

        public bool Checked { get; set; }

        public bool Answer1 { get; set; }
        public bool Answer2 { get; set; }
        public bool Answer3 { get; set; }
        public bool Answer4 { get; set; }
    }
}