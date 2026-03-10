using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public interface IDashboardRepository
    {
        Task<LeadDashboard> GetLeadDashboardData(string? userID, string? agencyId);
        Task<DataTableResult> LeadList(DataTablePara dataTablePara);
        Task<DataTableResult> AssignedUnassignLeadsList(DataTablePara dataTablePara,int IsFromList);
        Task<NewLead> GetDropDowndata(string UserID);
        Task<bool> SaveAssignLeadsToUser(int UserIdForAssign, List<int> LeadIds, string UserID, string AgencyId);
    }
}
