using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;

namespace BTM.Data
{
    public class Kunde
    {       
        
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string? Standort { get; set; }
        [NotMapped]
        public List<Devices>?Devices { get; set; }
        [NotMapped]
        public List<Telefon>? Telefons { get; set; }
        
    }
}
