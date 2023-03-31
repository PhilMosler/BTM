using BTM.Data;
using BTM.Data.DBAccess;
using BTM.Data.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BTM.Pages.Kunden
{
    public class EvaluationModel : PageModel
    {
        private readonly ICostumer _costumer;
        private readonly IDevices _devices;
        public EvaluationModel(ICostumer costumer,IDevices devices)
        {
            _costumer = costumer;
            _devices = devices;
        }
        [BindProperty]
        public List<Devices> Devices { get; set; }
        [BindProperty]
        public Kunde Costumer { get; set; }
        public void OnGet(int Id)
        {
            Costumer = _costumer.GetCostumer(Id);
            Devices = _devices.GetDevicesByCustomerID(Id,null);
        }
        public IActionResult OnPostBack(int ID)
        {
            return RedirectToPage("./Details", new {ID});
        }
    }
}
