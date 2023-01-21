using BTM.Data;
using BTM.Data.DBAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BTM.Pages.Kunden
{
    public class EditDeviceModel : PageModel
    {
        [BindProperty]
        public Devices CurrentDevice { get; set; }

        private readonly IDevices _dev;
        public EditDeviceModel(IDevices dev)
        {
        _dev= dev;
        }

        public void OnGet(int DeviceID)
        {
            CurrentDevice=_dev.GetDevicesById(DeviceID);
        }
        public IActionResult OnPostSave() 
        {
            //Save
            _dev.UpdateDevice(CurrentDevice);
            return RedirectToPage("./Details", new {ID=CurrentDevice.KundenID});
        }
        public IActionResult OnPostBack()
        {
            return RedirectToPage("./Details", new { ID = CurrentDevice.KundenID });
        }
    }
}
