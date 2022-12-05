using BTM.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace BTM.Data
{
    public class Counters
    {
        [Key]
        public int ID { get; set; }
        public int DeviceID { get; set; }
        public int Counter { get; set; }
        public DateTime DateTime { get; set; }
        public Quartal Quartal { get; set; }
    }
}
