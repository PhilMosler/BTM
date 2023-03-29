

using BTM.Data.Enums;

namespace BTM.Data
{
    public interface ICostumer
    {
        List<Kunde> GetAllCostumers(string search,Filter filter);
        Kunde GetCostumer(int id);
        Kunde AddNewCostumer(Kunde kunde);
        Kunde UpdateCostumer(Kunde kunde);
        void AddTelephone(Telefon telefon);
        bool RemovePhone(int PhoneId);
        Devices AddDevice(Devices device);
        Counters AddCounters(Counters counter);
        Counters GetLastCounterOfDevice(int ID);
        void RemoveCounter(int counterID);
        List<Tuple<Devices, Counters>> GetAllLastCountersOfKunde(int ID);
        void HideDevice(int DeviceID);

    }
    public class Filter
    {
        public int SearchYear { get; set; }
        public Quartal SearchQuartal { get; set; }
    }
}
    