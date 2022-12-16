using BTM.Data.DBAccess;
using Microsoft.EntityFrameworkCore;
using BTM;
namespace BTM.Data
{
    public class Costumer : ICostumer
    {
        private readonly BTMDbContext _db;
        private readonly IDevices _dev;
        public Costumer(BTMDbContext dbContext, IDevices dev)
        {
            _db = dbContext;
            _dev = dev;
        }

        public Counters AddCounters(Counters counter)
        {
            _db.Counters.Add(counter);
            _db.SaveChanges();
            return counter;
        }

        public Devices AddDevice(Devices device)
        {
            _db.Devices.Add(device);
            _db.SaveChanges();
            return device;
        }

        public Kunde AddNewCostumer(Kunde kunde)
        {
            if (kunde.Name != null && kunde.Name != String.Empty)
            {
                _db.Customers.Add(kunde);
                _db.SaveChanges();
                return kunde;
            }
            return null;
        }

        public void AddTelephone(Telefon telefon)
        {
            _db.Telephone.Add(telefon);
            _db.SaveChanges();
        }

        public List<Kunde> GetAllCostumers()
        {
            try
            {
                var All = _db.Customers.ToList();
                if (All != null && All.Count() > 0)
                {
                    foreach (var item in All)
                    {
                        item.Telefons = _db.Telephone.Where(x => x.KundenID == item.ID).ToList();
                        item.Devices = _db.Devices.Where(x => x.KundenID == item.ID).ToList();
                    }
                }
                return All.ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public Kunde GetCostumer(int id)
        {
            if (id != null && id > 0)
            {
                var kunde = _db.Customers.FirstOrDefault(x => x.ID == id);
                if (kunde != null && kunde.ID > 0)
                {
                    kunde.Devices = _dev.GetDevicesByCustomerID(id);
                    kunde.Telefons = _db.Telephone.Where(x => x.KundenID == id).ToList();
                    return kunde;
                }
            }
            return null;
        }

        public Counters GetLastCounterOfDevice(Counters counters)
        {
            var listOFDeviceCounters = _db.Counters.Where(x => x.DeviceID == counters.DeviceID).OrderByDescending(x => x.DateTime).ToList();
            var lastCounter = new Counters() {CounterSum=0,BlackWhiteCounter=0,ColorCounter=0};
            if (listOFDeviceCounters.Count > 0)
                lastCounter = listOFDeviceCounters.First();
            return lastCounter;
        }

        public Kunde UpdateCostumer(Kunde kunde)
        {
            if (kunde.Standort == null)
                kunde.Standort = String.Empty;
            _db.Customers.Update(kunde);
            _db.SaveChanges();
            return kunde;
        }
    }
}
