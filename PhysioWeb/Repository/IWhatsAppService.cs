
namespace PhysioWeb.Repository
{
    public interface IWhatsAppService
    {
        Task<string> SendImageAsync(string toPhoneNumber, string mediaId);
        Task<string> UploadImageAsync(string v);
    }
}
