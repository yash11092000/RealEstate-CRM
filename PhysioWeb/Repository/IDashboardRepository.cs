using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public interface IDashboardRepository
    {
        Task<LeadDashboard> GetLeadDashboardData(string? userID, string? agencyId);
    }
}
