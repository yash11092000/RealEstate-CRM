using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MimeKit;
using NuGet.Configuration;
using PhysioWeb.Models;
using PhysioWeb.Repository;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Options;

namespace PhysioWeb.Controllers
{
    public class ExternalServicesController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly IPdfServices _pdfService;

        public ExternalServicesController(IEmailService emailService, IOptions<EmailSettings> settings, IPdfServices pdfService)
        {
            _emailService = emailService;
            _pdfService = pdfService;
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(string SenderEmail, List<IFormFile> filesAttachments)
        {
            var html = @"
            <h2>Hello from ERP System</h2>
            <p>This email includes text and images.</p>
            <img src='https://yourdomain.com/logo.png' width='150'/>
            ";

            await _emailService.SendEmailAsync(
                SenderEmail,
                "Test Email with Image",
                html,
                filesAttachments);

            return Ok("Email sent successfully");
        }


        //[HttpGet("DownloadQuotation")]
        public IActionResult DownloadQuotation()
        {
            var pdfBytes = _pdfService.GenerateQuotationPdf();

            return File(pdfBytes,"application/pdf","Quotation.pdf");
        }
    }
}
