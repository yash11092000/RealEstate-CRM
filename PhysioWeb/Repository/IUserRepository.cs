using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public interface IUserRepository
    {
        Task<bool> CheckEmailExists(string email);
        Task<HomeDashboard> GetDashboardData();
        Task<Users> Login(string Email, string Password);
        Task<bool> RegisterUser(Register register);
        // Task<PropertyMaster> GetPropertyDetails();
    }
}
