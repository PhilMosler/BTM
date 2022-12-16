using BTM.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BTM.Pages.Kunden
{
    public class IndexModel : PageModel
    {
        private readonly ICostumer _costumer;
        private readonly ILogger<IndexModel> _logger;
        public IndexModel(ILogger<IndexModel> logger, ICostumer costumer)
        {
            _logger = logger;
            _costumer = costumer;
        }
        [BindProperty]
        public List<Kunde> AllCostumers { get; set; }
        [BindProperty]
        public Kunde Costumer { get; set; }
        public void OnGet()
        {
            AllCostumers = _costumer.GetAllCostumers();
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
        public IActionResult OnPostFilter()
        {
            return RedirectToPage();
        }
    }
}
