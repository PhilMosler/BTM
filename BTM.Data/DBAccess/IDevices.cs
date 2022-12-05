using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTM.Data.DBAccess
{
    public interface IDevices
    {
        List<Devices> GetDevicesByCustomerID(int id);

    }
}
