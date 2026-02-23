using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public interface IDashboardRepository
    {
        Task<LeadDashboard> GetLeadDashboardData(string? userID, string? agencyId);
        Task<DataTableResult> LeadList(DataTablePara dataTablePara);
        Task<DataTableResult> AssignedUnassignLeadsList(DataTablePara dataTablePara);
    }
}
