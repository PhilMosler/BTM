using System.ComponentModel.DataAnnotations;

namespace BTM.Data
{
    public class Telefon
    {
        [Key]
        public int ID { get; set; }
        public Kunde KundenID { get; set; }
        public string Name { get; set; }
        public string TelefonNummer { get; set; }
    }
}
