﻿using BTM.Data.Enums;
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

                    foreach (var device in devices)
                    {
                        device.Counters = _db.Counters.Where(x => x.DeviceID == device.ID).OrderByDescending(x => x.DateTime).ToList();
                        if (device.Counters.Count > 1)
                        {
                            device.ColEval = device.Counters[0].ColorCounter - device.Counters[1].ColorCounter - device.FreePrintsColor;
                            device.BlackWhiteEval = device.Counters[0].BlackWhiteCounter - device.Counters[1].BlackWhiteCounter - device.FreePrintsBlackWhite;
                            if (device.BlackWhiteEval < 0)
                                device.BlackWhiteEval = 0;
                            if (device.ColEval < 0)
                                device.ColEval = 0;
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
    }
}
