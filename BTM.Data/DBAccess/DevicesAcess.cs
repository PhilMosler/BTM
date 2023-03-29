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
        public List<Devices> GetDevicesByCustomerID(int id,Filter filter)
        {
            try
            {
                if (id != null && id > 0)
                {
                    var devices = _db.Devices.Where(x => x.KundenID == id && x.IsVisible == true).ToList();

                    foreach (var device in devices.Where(x => x.IsVisible))
                    {
                        device.Counters = _db.Counters.Where(x => x.DeviceID == device.ID).OrderByDescending(x => x.DateTime).ToList();
                        if (device.Counters.Count >= 2)
                        {
                            device.ColEval = device.Counters[0].ColorCounter - device.Counters[1].ColorCounter - (3 * device.FreePrintsColor);
                            device.BlackWhiteEval = device.Counters[0].BlackWhiteCounter - device.Counters[1].BlackWhiteCounter - (3 * device.FreePrintsBlackWhite);
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
                            device.isUpToDate = CheckForQuartal(device,filter);
                            if (!device.isUpToDate) { _db.Customers.Find(id).IsUpToDate = false; }
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

        public Devices GetDevicesById(int id)
        {
            return _db.Devices.FirstOrDefault(x => x.ID == id);
        }

        public Devices UpdateDevice(Devices device)
        {
            if (device != null)
            {
                _db.Devices.Update(device);
                _db.SaveChanges();
            }
            return device;
        }
        #region Helper
        private (int, int) GetMonthOfQuartal()
        {
            List<List<int>> Months = new List<List<int>>
            {
                new List<int>() { 1, 2, 3 },
                new List<int>() { 4, 5, 6 },
                new List<int>() { 7, 8, 9 },
                new List<int>() { 10, 11, 12 }
            };
            var quartal = Months.FindIndex(x => x.Contains(DateTime.Today.Month));        // add 1 => 0 index
            var montOfQuartal = Months.ElementAt(quartal).IndexOf(DateTime.Today.Month);  // add 1 => 0 index

            return (quartal, montOfQuartal);
        }
        public Quartal GetCurrentQuartal()
        {
            return (Quartal)((GetMonthOfQuartal().Item1 + 1) * 10);
        }
        private bool CheckForQuartal(Devices device,Filter filter)
        {

            var QuartalInfo = GetMonthOfQuartal();
            QuartalInfo.Item2 = 1;
            var lastCount = device.Counters.OrderByDescending(x => x.DateTime).First();
            var currentQuartal = Quartal.Default;
            switch (QuartalInfo.Item1)
            {
                case 0:
                    currentQuartal = Quartal.Quartal1;
                    break;
                case 1:
                    currentQuartal = Quartal.Quartal2;
                    break;
                case 2:
                    currentQuartal = Quartal.Quartal3;
                    break;
                case 3:
                    currentQuartal = Quartal.Quartal4;
                    break;
                default:
                    currentQuartal = Quartal.Default;
                    break;
            }


            if (currentQuartal != Quartal.Default  )
            {
                if (lastCount.QuartalYear == filter.SearchYear && lastCount.Quartal == filter.SearchQuartal)
                    return true;
                
            }
            return false;
        }
        #endregion
    }
}
