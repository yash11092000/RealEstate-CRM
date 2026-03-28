using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PhysioWeb.Models;
using PhysioWeb.Repository;

namespace PhysioWeb.ViewComponents
{
    public class SidebarMenuViewComponent : ViewComponent
    {
        private readonly IAgencyRepository _agencyRepository;
        private readonly IMemoryCache _cache;

        public SidebarMenuViewComponent(IAgencyRepository agencyRepository, IMemoryCache cache)
        {
            _agencyRepository = agencyRepository;
            _cache = cache;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = ViewContext.HttpContext.User;

            string UserID = user.FindFirst(ClaimTypes.PrimarySid)?.Value;

            var cacheKey = $"SidebarMenu_{User.Identity.Name}";

            if (!_cache.TryGetValue(cacheKey, out var result))
            {
                result = await _agencyRepository.GetSideBar(UserID);
                _cache.Set(cacheKey, result, TimeSpan.FromMinutes(30));
            }
            return View(result);
        }
    }

}
