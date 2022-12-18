using BTM.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace BTM.Data
{
    public class Counters
    {
        
        public Counters()
        {

        }
        
        [Key]
        public int ID { get; set; }
        public int DeviceID { get; set; }
        public int BlackWhiteCounter { get; set; }
        public int ColorCounter { get; set; }
        public int CounterSum { get; set; }

        public DateTime DateTime { get; set; }
        public Quartal Quartal { get; set; }

       
    }
}
