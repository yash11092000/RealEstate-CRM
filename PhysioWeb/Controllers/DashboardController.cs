using Microsoft.AspNetCore.Mvc;

namespace PhysioWeb.Controllers
{
    public class DashboardController : Controller
    {
        public async Task<IActionResult> LeadDashboard()
        {
            return View();
        }
    }
}
