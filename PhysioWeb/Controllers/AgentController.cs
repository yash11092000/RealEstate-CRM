using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhysioWeb.Models;
using PhysioWeb.Repository;

namespace PhysioWeb.Controllers
{
    [Authorize(Roles = "Agency")]
    public class AgentController : Controller
    {
        private readonly IAgencyRepository _agencyRepository;
        private readonly ISuperAdminRepository _superAdminRepository;
        public AgentController(IAgencyRepository agencyRepository, ISuperAdminRepository superAdminRepository)
        {
            _agencyRepository = agencyRepository;
            _superAdminRepository = superAdminRepository;
        }
        public IActionResult PropertyDesc()
        {
            return View();
        }

        #region Agency Dashboard
        public async Task<ActionResult> AgencyDashboard()
        {
            string username = User.Identity?.Name;
            string role = User.FindFirst(ClaimTypes.Role)?.Value;
            string userId = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            MenuMaster menuMaster = await _superAdminRepository.GetMenuList(role, userId);
            return View(menuMaster);
        }
        #endregion


        #region MenuMaster
        public async Task<ActionResult> MenuMaster()
        {
            SidebarMenu Sidebar = await _agencyRepository.GetParentsForSideBar();
            return View(Sidebar);
        }

        [HttpPost]
        public async Task<ActionResult> SaveMenuMaster(SidebarMenu Sidebar)
        {
            Sidebar.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
            Sidebar.AgencyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;
            var result = await _agencyRepository.SaveMenuMaster(Sidebar);
            return Json(result);
        }

        //[HttpPost]
        //public async Task<ActionResult> ListRentalType()
        //{
        //    var form = Request.Form;

        //    // ✅ Map DataTables default parameters
        //    var dataTablePara = new DataTablePara
        //    {
        //        iDisplayStart = Convert.ToInt32(form["start"]),
        //        iDisplayLength = Convert.ToInt32(form["length"]),
        //        iSortCol_0 = Convert.ToInt32(form["order[0][column]"]),
        //        sSortDir_0 = form["order[0][dir]"],
        //        sSearch = form["search[value]"]
        //    };

        //    // ✅ Map column filters dynamically (for first 10 columns)
        //    for (int i = 0; i < 30; i++)
        //    {
        //        string key = $"columns[{i}][search][value]";
        //        if (Request.Form.ContainsKey(key))
        //        {
        //            typeof(DataTablePara)
        //                .GetProperty($"sSearch_{i}")
        //                ?.SetValue(dataTablePara, Request.Form[key].ToString());
        //        }
        //    }
        //    dataTablePara.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
        //    dataTablePara.AgencyId = User.FindFirst(ClaimTypes.GroupSid)?.Value;
        //    var result = await _masterRepository.ListRentalType(dataTablePara);
        //    var requestForm = Request.Form;
        //    return Json(new
        //    {
        //        draw = requestForm["draw"],                     // Echo back the draw count
        //        recordsTotal = result.iTotalRecords,            // Total records in DB
        //        recordsFiltered = result.iTotalDisplayRecords,  // Total records after filtering
        //        data = result.aaData                            // Actual paged data
        //    });

        //}

        //[HttpPost]
        //public async Task<ActionResult> EditRentalType(int UniqueID)
        //{
        //    try
        //    {
        //        string UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;

        //        var data = await _masterRepository.EditRentalType(UniqueID, UserID);

        //        return Json(data);

        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}
        //[HttpPost]
        //public async Task<ActionResult> DeleteRentalType(int UniqueID)
        //{
        //    var RentalTypeMaster = new RentalTypeMaster
        //    {
        //        UniquId = UniqueID
        //    };
        //    RentalTypeMaster.UserID = User.FindFirst(ClaimTypes.PrimarySid)?.Value;
        //    var result = await _masterRepository.DeleteRentalType(RentalTypeMaster);
        //    return Json(new { success = result });
        //}


        #endregion
    }
}
