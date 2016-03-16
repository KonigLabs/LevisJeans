using System.Collections.Generic;

namespace KonigLabs.LevisJeans.Models
{
    public class ChoosePhraseVm
    {
        public int CustomerId { get; set; }
        public string Phrase { get; set; }

        public string[] Phrases { get; set; } 
    }
}