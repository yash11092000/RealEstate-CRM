using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public interface IAgencyRepository
    {
        Task<SidebarMenu> GetParentsForSideBar();
        Task<List<SidebarMenu>> GetSideBar();
        Task<bool> SaveMenuMaster(SidebarMenu sidebar);
    }
}
