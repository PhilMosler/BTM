using BTM.Data.Enums;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTM.Data.DBAccess
{
    public class DevicesAcess : IDevices
    {
        private readonly BTMDbContext _db;
        public DevicesAcess(BTMDbContext db)
        {
            _db = db;
        }
        public List<Devices> GetDevicesByCustomerID(int id)
        {
            try
            {
                if (id != null && id > 0)
                {
                    var devices = _db.Devices.Where(x => x.KundenID == id).ToList();
                  
                    foreach (var device in devices)
                    {
                        device.Counters= _db.Counters.Where(x => x.DeviceID == device.ID).OrderByDescending(x=>x.DateTime).ToList();
                        
                    }
                    return devices;
                }
                return new List<Devices>();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while getting Devices by Customer   =>" + e.Message + " stacktrace: " + e.StackTrace);
                return new  List<Devices>();
            }
        }
    }
}
