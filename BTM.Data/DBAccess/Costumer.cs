using BTM.Data.DBAccess;
using Microsoft.EntityFrameworkCore;
using BTM;
using BTM.Data.Enums;

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

        public List<Kunde> GetAllCostumers(string Search)
        {
            try
            {
                if (int.TryParse(Search, out int num)) ;


                var All = _db.Customers.Where(x =>
              x.Name.Contains(Search)
            || string.IsNullOrWhiteSpace(Search)
            || x.Email.Contains(Search)
             
            
            );
                if (All != null && All.Count() > 0)
                {
                    foreach (var item in All)
                    {
                        var phone = _db.Telephone.Where(x => x.KundenID == item.ID).ToList();
                        var device = _dev.GetDevicesByCustomerID(item.ID);
                        if (phone.Any())
                        {
                            item.Telefons = phone;
                        }
                        if (device.Any())
                        {
                            item.Devices = device;
                        }
                    }
                }
                return All.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine("Get All Customer failed" + e.Message + e.StackTrace);
                return new List<Kunde>();
            }
        }

        public List<Tuple<Devices, Counters>> GetAllLastCountersOfKunde(int ID)
        {
            List<Tuple<Devices, Counters>> tuples = new List<Tuple<Devices, Counters>>();
            if (ID > 0)
            {
                var Divices = _dev.GetDevicesByCustomerID(ID);
                foreach (var item in Divices)
                {
                    var lastount = GetLastCounterOfDevice(item.ID);
                    tuples.Add(Tuple.Create(item, lastount));
                }
            }
            return tuples;
        }

        public Kunde GetCostumer(int id)
        {
            if (id > 0)
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

        public Counters GetLastCounterOfDevice(int ID)
        {
            var listOFDeviceCounters = _db.Counters.Where(x => x.DeviceID == ID).OrderByDescending(x => x.DateTime).ToList();
            var lastCounter = new Counters() { CounterSum = 0, BlackWhiteCounter = 0, ColorCounter = 0 };
            if (listOFDeviceCounters.Count > 0)
                lastCounter = listOFDeviceCounters.First();
            return lastCounter;
        }

        public void HideDevice(int DeviceID)
        {
            if (DeviceID > 0)
            {
                var selcted = _db.Devices.SingleOrDefault(x => x.ID == DeviceID);
                if (selcted.ID > 0)
                {
                    selcted.IsVisible = false;
                    _db.Devices.Update(selcted);
                    _db.SaveChanges();
                }
            }
        }

        public void RemoveCounter(int counterID)
        {
            if (counterID > 0)
            {
                var count = _db.Counters.SingleOrDefault(x => x.ID == counterID);
                if (count != null && count.ID > 0)
                {
                    _db.Counters.Remove(count);
                    _db.SaveChanges();
                }
            }
        }

        public bool RemovePhone(int PhoneId)
        {
            if (PhoneId > 0)
            {
                if (_db.Telephone.Any(x => x.ID == PhoneId))
                {
                    _db.Telephone.Remove(_db.Telephone.Find(PhoneId));
                    _db.SaveChanges();
                    return true;
                }
            }
            return false;
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
