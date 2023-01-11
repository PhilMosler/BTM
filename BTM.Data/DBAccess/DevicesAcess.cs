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
                    var devices = _db.Devices.Where(x => x.KundenID == id && x.IsVisible == true).ToList();

                    foreach (var device in devices.Where(x=>x.IsVisible))
                    {
                        device.Counters = _db.Counters.Where(x => x.DeviceID == device.ID).OrderByDescending(x => x.DateTime).ToList();
                        if (device.Counters.Count >= 2)
                        {
                            device.ColEval = device.Counters[0].ColorCounter - device.Counters[1].ColorCounter - (3*device.FreePrintsColor);
                            device.BlackWhiteEval = device.Counters[0].BlackWhiteCounter - device.Counters[1].BlackWhiteCounter - (3*device.FreePrintsBlackWhite);
                            if (device.BlackWhiteEval < 0)
                                device.BlackWhiteEval = 0;
                            if (device.ColEval < 0)
                                device.ColEval = 0;
                        }
                        else
                        {
                            device.ColEval = 0;
                            device.BlackWhiteEval = 0;
                        }
                        if (device.Counters.Any())
                        {
                            device.isUpToDate=CheckForQuartal(device);
                            if(!device.isUpToDate) { _db.Customers.Find(id).IsUpToDate = false; }
                        }
                        else
                        {
                            device.isUpToDate = false;
                        }


                    }
                    return devices;
                }
                return new List<Devices>();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while getting Devices by Customer   =>" + e.Message + " stacktrace: " + e.StackTrace);
                return new List<Devices>();
            }
        }
        #region Helper
        private bool CheckForQuartal(Devices device)
        {
            var lastCount = device.Counters.OrderByDescending(x => x.DateTime).First();
            var currentQuartal = Quartal.Quartal1;
            var today = DateTime.Today;
            if (today.Month >= 1 && today.Month <= 3)
                currentQuartal = Quartal.Quartal1;
            else if (today.Month >= 4 && today.Month <= 6)
                currentQuartal = Quartal.Quartal2;
            else if (today.Month >= 7 && today.Month <= 9)
                currentQuartal = Quartal.Quartal3;
            else if (today.Month >= 10 && today.Month <= 12)
                currentQuartal = Quartal.Quartal4;
            if (lastCount.QuartalYear == DateTime.Today.Year && lastCount.Quartal == currentQuartal)
            {
                return true;
            }

            return false;
        }
        #endregion
    }
}
