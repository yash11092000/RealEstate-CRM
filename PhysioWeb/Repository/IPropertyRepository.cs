using PhysioWeb.Models;

namespace PhysioWeb.Repository
{
    public interface IPropertyRepository
    {
        Task<PropertyMaster> GetPropertyDetails(int propertyId);
        Task<bool> SendRequest(string contactPersonName, string contactPersonEmail, string contactPersonPhone, string description, int propertyId);
    }
}
