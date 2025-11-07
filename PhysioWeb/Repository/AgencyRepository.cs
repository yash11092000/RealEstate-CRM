using Microsoft.AspNetCore.Components;
using PhysioWeb.Data;
using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public class AgencyRepository : IAgencyRepository
    {
        private readonly DbHelper _dbHelper;

        public AgencyRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;

        }

        public async Task<SidebarMenu> GetParentsForSideBar()
        {
            try
            {
                string[] parametersName = { };
                object[] Values = { };

                string Sp = "FMR_GetSideBarParents";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parametersName, Values);
                SidebarMenu sidebar = new SidebarMenu();
                while (data.Read())
                {
                    sidebar.ParentList.Add(new DropDownSource(data, true));
                }
                return sidebar;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }

        }

        public async Task<List<SidebarMenu>> GetSideBar()
        {
            try
            {
                string[] parametersName = { };
                object[] Values = { };

                string Sp = "FMR_GetSideBar";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parametersName, Values);
                List<SidebarMenu> sidebar = new();
                while (data.Read())
                {
                    sidebar.Add(new SidebarMenu(data));
                }
                return sidebar;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }


        }

        public async Task<bool> SaveMenuMaster(SidebarMenu sidebar)
        {
            try
            {
                string[] parametersName = { "MenuId", "MenuName", "MenuUrl", "MenuType", "ParentMenuId", "MenuIcon", "DisplayOrder", "OpenInNewTab", "IsActive", "UserID", "AgencyId" };
                object[] Values = { sidebar.UniquId, sidebar.MenuName, sidebar.MenuUrl,sidebar.MenuType,sidebar.ParentMenuId,sidebar.MenuIcon,sidebar.DisplayOrder,sidebar.OpenInNewTab,
                    sidebar.IsActive,sidebar.UserID ,sidebar.AgencyId };

                string Sp = "FMR_SaveSideMenu";
                int RecordAffected = await _dbHelper.ExecuteNonQueryAsync(Sp, parametersName, Values);
                return RecordAffected > 0;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }

        }
    }
}
