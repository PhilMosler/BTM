using BTM.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BTM.Pages.Kunden
{
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public Kunde CurrentCostumer { get; set; }
        [BindProperty]
        public Devices Devices { get; set; }
        [BindProperty]
        public Telefon Telefon { get; set; }
        [BindProperty]
        public Counters Counters { get; set; }
        public List<Tuple<Devices, Counters>> LastCounter { get; set; }
        [BindProperty]
        public string ColorMessage { get; set; }
        [BindProperty]
        public string BlackWhiteMessage { get; set; }
        [BindProperty]
        public string GesamtMessage { get; set; }
        [BindProperty]
        public bool Color { get; set; }
        [BindProperty]
        public bool Black { get; set; }
        [BindProperty]
        public bool Gesamt { get; set; }



        private readonly ICostumer _db;
        public DetailsModel(ICostumer db)
        {
            _db = db;           
        }
        public void OnGet(int id)
        {
            Counters = new Counters();
            CurrentCostumer = _db.GetCostumer(id);
            LastCounter = _db.GetAllLastCountersOfKunde(id);
            
        }
        public IActionResult OnPost(int ID)
        {
            _db.UpdateCostumer(CurrentCostumer);
            return this.RedirectToPage();
        }
        public IActionResult OnPostAddPhone(int ID)
        {
            var phone = Telefon;
            phone.KundenID = ID;
            _db.AddTelephone(phone);
            return this.RedirectToPage();
        }
        public IActionResult OnPostAddDevice(int ID)
        {
            var dev = this.Devices;
            dev.KundenID = ID;            
            _db.AddDevice(dev);            
            return RedirectToPage();
        }
        public IActionResult OnPostAddCounter(int ID)
        {
            Color = true;
            Black = true;
            Gesamt = true;
            var counter = this.Counters;
            counter.DateTime = DateTime.Now;
            var lastCounter = _db.GetLastCounterOfDevice(counter.DeviceID);
            if (lastCounter.ColorCounter > counter.ColorCounter)
            {
               ViewData["Color"] = "Error";
            }
            if (lastCounter.CounterSum > counter.CounterSum)
            {
                Gesamt = false;
            }
            if (lastCounter.BlackWhiteCounter > counter.BlackWhiteCounter)
            {
                Black = false;
            }
            if (Black && Color)
            {
                _db.AddCounters(counter);
            }
            return RedirectToPage();

        }
        public IActionResult OnPostRemoveCounter(int CounterID)
        {
            _db.RemoveCounter(CounterID);
            return RedirectToPage();
        }
        public IActionResult OnPostCalculateCounters(int ID)
        {
            return RedirectToPage("./Evaluation", new { ID });
        }       

        public IActionResult OnPostBack()
        {

            return RedirectToPage("./Index");
        }
        public IActionResult OnPostDeleteDevice(int ID,int DeviceID)
        {
            _db.HideDevice(DeviceID);
            return RedirectToPage();
        }
        private void CreateCostumerEvaluation(Kunde results)
        {
            

        }

    }
}
