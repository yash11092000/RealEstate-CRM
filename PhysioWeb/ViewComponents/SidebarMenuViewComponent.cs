using Microsoft.AspNetCore.Mvc;
using PhysioWeb.Models;
using PhysioWeb.Repository;

namespace PhysioWeb.ViewComponents
{
    public class SidebarMenuViewComponent : ViewComponent
    {
        private readonly IAgencyRepository _agencyRepository;

        public SidebarMenuViewComponent(IAgencyRepository agencyRepository)
        {
            _agencyRepository = agencyRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var result = await _agencyRepository.GetSideBar();
            return View(result);
        }
    }

}
