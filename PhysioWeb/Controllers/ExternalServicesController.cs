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
        public async Task<IActionResult> SendMail([FromBody] ShareMailRequest request)
        {
            var attachments = new List<IFormFile>();

            foreach (var path in request.MediaPaths)
            {
                // Example input:
                // Property/1/Images/abc.jpg
                // Property/1/Videos/xyz.mp4

                string fullPath;

                if (path.Contains("/Images/"))
                {
                    fullPath = ResolveSecurePath("secure-images", path);
                }
                else if (path.Contains("/Videos/"))
                {
                    fullPath = ResolveSecurePath("secure-images", path);
                    // (you are storing videos under same folder)
                }
                else
                {
                    continue;
                }

                var stream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);

                attachments.Add(new FormFile(
                    stream,
                    0,
                    stream.Length,
                    "filesAttachments",
                    Path.GetFileName(fullPath))
                {
                    Headers = new HeaderDictionary(),
                    ContentType = GetContentType(fullPath)
                });
            }

            var html = @"
        <h2>Hello from ERP System</h2>
        <p>Property media shared securely.</p>
    ";

            await _emailService.SendEmailAsync(
                request.SenderEmail,
                "Property Media",
                html,
                attachments
            );

            return Ok("Email sent successfully");
        }
        private static string GetContentType(string path)
        {
            return Path.GetExtension(path).ToLower() switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".webp" => "image/webp",
                ".mp4" => "video/mp4",
                ".mov" => "video/quicktime",
                ".avi" => "video/x-msvideo",
                _ => "application/octet-stream"
            };
        }


        //[HttpGet("DownloadQuotation")]
        public IActionResult DownloadQuotation()
        {
            var pdfBytes = _pdfService.GenerateQuotationPdf();

            return File(pdfBytes, "application/pdf", "Quotation.pdf");
        }

        private string ResolveSecurePath(string baseFolder, string filePath)
        {
            var root = Path.Combine(Directory.GetCurrentDirectory(), baseFolder);

            var fullPath = Path.Combine(root, filePath);
            fullPath = Path.GetFullPath(fullPath);

            if (!fullPath.StartsWith(root))
                throw new InvalidOperationException("Invalid file path");

            if (!System.IO.File.Exists(fullPath))
                throw new FileNotFoundException();

            return fullPath;
        }

    }
    public class ShareMailRequest
    {
        public string SenderEmail { get; set; }
        public List<string> MediaPaths { get; set; }
    }

}
