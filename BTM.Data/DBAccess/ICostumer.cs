﻿

namespace BTM.Data
{
    public interface ICostumer
    {
        List<Kunde> GetAllCostumers(string search);
        Kunde GetCostumer(int id);
        Kunde AddNewCostumer(Kunde kunde);
        Kunde UpdateCostumer(Kunde kunde);
        void AddTelephone(Telefon telefon);
        Devices AddDevice(Devices device);
        Counters AddCounters(Counters counter);
        Counters GetLastCounterOfDevice(int ID);
        void RemoveCounter(int counterID);
        List<Tuple<Devices, Counters>> GetAllLastCountersOfKunde(int ID);

    }
}
    