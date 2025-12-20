namespace PhysioWeb.Repository
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail,string subject,string htmlBody,List<IFormFile> attachments = null);
    }
}
