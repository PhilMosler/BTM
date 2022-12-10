using BTM.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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



        private readonly ICostumer _db;
        public DetailsModel(ICostumer db)
        {
            _db = db;
        }
        public void OnGet(int id)
        {
            Counters = new Counters();
            CurrentCostumer = _db.GetCostumer(id);
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
            return RedirectToPage() ;
        }
        public IActionResult OnPostAddCounter(int ID)
        {
            var counter = this.Counters;
            counter.DateTime = DateTime.Now;            
            _db.AddCounters(counter);
            return RedirectToPage();
        }
        

    }
}
