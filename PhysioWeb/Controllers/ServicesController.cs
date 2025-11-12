using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;


namespace PhysioWeb.Controllers
{
    public class ServicesController : Controller
    {
        #region OC
        public async Task<ActionResult> OrderConfirm() {

            return View();
        }

        #endregion

        #region Invoice
        public async Task<ActionResult> Invoice() {
            return View();
        }
        #endregion

        #region ProformaInvoice
        public async Task<ActionResult> ProformaInvoice() {
            return View();
        }
        #endregion
        #region ProformaInvoicePrint
        public async Task<ActionResult> ProformaInvoicePrint()
        {
            return View();
        }
        #endregion


        #region Lead profile
        public async Task<ActionResult> LeadProfile() {
            return View();
        }
        #endregion
    }
}
