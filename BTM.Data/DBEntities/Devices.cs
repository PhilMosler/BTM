using BTM.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTM.Data
{
    public class Devices
    {
        [Key]
        public int ID { get; set; }
        public int KundenID { get; set; }
        public int DeviceNumber { get; set; }
        public string DeviceName { get; set; }
        public Verträge VertragsID { get; set; }
        public string Standort { get; set; }
        [NotMapped]
        public List<Counters> Counters { get; set; }
        [NotMapped]
        public List<DateTime>? LastCounterTimestamps { get; set; }
    }
}
