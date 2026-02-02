using Microsoft.AspNetCore.Components;
using PhysioWeb.Data;
using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly DbHelper _dbHelper;

        public DashboardRepository(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;

        }
        public async Task<LeadDashboard> GetLeadDashboardData(string? userID, string? agencyId)
        {
            try
            {
                string[] parametersName = { "UserID", "AgencyId" };
                object[] Values = { userID, agencyId };

                string Sp = "FMR_GetLeadDashboardData";
                var data = await _dbHelper.GetDataReaderAsync(Sp, parametersName, Values);

                while (data.Read())
                {
                    var LeadData = new LeadDashboard(data);
                    return LeadData;
                }
                return null;
            }
            catch (Exception ex)
            {
                // Optional: log error here
                throw;
            }
        }
    }
}
