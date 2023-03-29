using BTM.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTM.Data.DBAccess
{
    public interface IDevices
    {
        List<Devices> GetDevicesByCustomerID(int id,Filter filter);
        Devices GetDevicesById(int id);
        Devices UpdateDevice(Devices device);
        Quartal GetCurrentQuartal();
    }
}
