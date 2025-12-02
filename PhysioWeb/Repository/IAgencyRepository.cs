using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public interface IAgencyRepository
    {
        Task<int> GetNextOrder(int parentId);
        Task<SidebarMenu> GetParentsForSideBar();
        Task<List<SidebarMenu>> GetSideBar();
        Task<bool> SaveMenuMaster(SidebarMenu sidebar);
    }
}
