using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public interface IAgencyRepository
    {
        Task<SidebarMenu> GetParentsForSideBar();
        Task<bool> SaveMenuMaster(SidebarMenu sidebar);
    }
}
