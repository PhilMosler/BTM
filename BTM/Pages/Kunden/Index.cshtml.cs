using BTM.Data;
using BTM.Data.DBAccess;
using BTM.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BTM.Pages.Kunden
{
    public class IndexModel : PageModel
    {
        private ICostumer _costumer { get; set; }
        private IDevices _devices { get; set; }

        private readonly ILogger<IndexModel> _logger;
        public IndexModel(ILogger<IndexModel> logger, ICostumer costumer,IDevices devices)
        {
            _logger = logger;
            _costumer = costumer;
            _devices = devices;
        }
        [BindProperty]
        public List<Kunde> AllCostumers { get; set; }
        [BindProperty(SupportsGet = true)]
        public Quartal SelectedQuartal { get; set; }
        [BindProperty(SupportsGet = true)]
        public int SelectedYear { get; set; }
        [BindProperty]
        public Kunde Costumer { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }
        public void OnGet()
        {
            var filter = new Filter() { SearchQuartal= SelectedQuartal,SearchYear=SelectedYear };
            AllCostumers = _costumer.GetAllCostumers(Search,filter);
            if (AllCostumers == null)
                AllCostumers = new List<Kunde>();

            Costumer = new Kunde { Email = string.Empty, Standort = string.Empty };
        }
        public IActionResult OnPostCreate()
        {
            var kunde = _costumer.AddNewCostumer(this.Costumer);
            if (kunde != null)
            {
                Costumer=kunde;
            }
            return RedirectToPage("./Details",new {Id=kunde.ID });
        }
        public IActionResult OnPostDetail(int ID)
        {
            return RedirectToPage($"./Details",new {Id=ID });
        }
        public IActionResult OnPostAddFilter()
        {
            return RedirectToPage();
        }

    }
}
