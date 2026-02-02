using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PhysioWeb.Models;
using PhysioWeb.Repository;

namespace PhysioWeb.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _IDashboardRepository;
        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _IDashboardRepository = dashboardRepository;
        }
        public async Task<IActionResult> LeadDashboard()
        {
            string UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            string AgencyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;
            LeadDashboard data = await _IDashboardRepository.GetLeadDashboardData(UserID, AgencyId);
            return View(data);
        }

        public async Task<ActionResult> SalesDashboard()
        {
            return View();
        }

        public async Task<ActionResult> AgentDashboard()
        {
            return View();
        }
    }
}
