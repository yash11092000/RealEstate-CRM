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
        public async Task<ActionResult> LeadList() {
            var form = Request.Form;

            // ✅ Map DataTables default parameters
            var dataTablePara = new DataTablePara
            {
                iDisplayStart = Convert.ToInt32(form["start"]),
                iDisplayLength = Convert.ToInt32(form["length"]),
                iSortCol_0 = Convert.ToInt32(form["order[0][column]"]),
                sSortDir_0 = form["order[0][dir]"],
                sSearch = form["search[value]"]
            };

            // ✅ Map column filters dynamically (for first 10 columns)
            for (int i = 0; i < 30; i++)
            {
                string key = $"columns[{i}][search][value]";
                if (Request.Form.ContainsKey(key))
                {
                    typeof(DataTablePara)
                        .GetProperty($"sSearch_{i}")
                        ?.SetValue(dataTablePara, Request.Form[key].ToString());
                }
            }
            dataTablePara.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            dataTablePara.AgencyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;
            var result = await _IDashboardRepository.LeadList(dataTablePara);
            var requestForm = Request.Form;
            return Json(new
            {
                draw = requestForm["draw"],                     // Echo back the draw count
                recordsTotal = result.iTotalRecords,            // Total records in DB
                recordsFiltered = result.iTotalDisplayRecords,  // Total records after filtering
                data = result.aaData                            // Actual paged data
            });
        }
        public async Task<ActionResult> SalesDashboard()
        {
            return View();
        }

        public async Task<ActionResult> AgentDashboard()
        {
            return View();
        }

        #region LeadAssignmentDashboard
        [HttpGet]
        public async Task<ActionResult> LeadAssignmentDashboard()
        {
            string UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            NewLead DropDowns = await _IDashboardRepository.GetDropDowndata(UserID);


            return View(DropDowns);
            //return View();
        }

        public async Task<ActionResult> AssignedUnassignLeadsList(int IsFromList)
        {
            var form = Request.Form;

            // ✅ Map DataTables default parameters
            var dataTablePara = new DataTablePara
            {
                iDisplayStart = Convert.ToInt32(form["start"]),
                iDisplayLength = Convert.ToInt32(form["length"]),
                iSortCol_0 = Convert.ToInt32(form["order[0][column]"]),
                sSortDir_0 = form["order[0][dir]"],
                sSearch = form["search[value]"]
            };

            // ✅ Map column filters dynamically (for first 10 columns)
            for (int i = 0; i < 30; i++)
            {
                string key = $"columns[{i}][search][value]";
                if (Request.Form.ContainsKey(key))
                {
                    typeof(DataTablePara)
                        .GetProperty($"sSearch_{i}")
                        ?.SetValue(dataTablePara, Request.Form[key].ToString());
                }
            }
            dataTablePara.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            dataTablePara.AgencyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;
            var result = await _IDashboardRepository.AssignedUnassignLeadsList(dataTablePara);
            var requestForm = Request.Form;
            return Json(new
            {
                draw = requestForm["draw"],                     // Echo back the draw count
                recordsTotal = result.iTotalRecords,            // Total records in DB
                recordsFiltered = result.iTotalDisplayRecords,  // Total records after filtering
                data = result.aaData                            // Actual paged data
            });
        }
        #endregion
    }
}
