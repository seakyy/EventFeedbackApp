using EventFeedbackApp.Data;
using EventFeedbackApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using QRCoder;

namespace EventFeedbackApp.Pages.Admin
{
    public class CreateSessionModel : PageModel
    {
        private readonly AppDbContext _db;
        private readonly IConfiguration _config;

        public CreateSessionModel(AppDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        [BindProperty]
        public Session Session { get; set; } = new();

        public string QrCodeBase64 { get; set; }

        public bool SessionSaved { get; set; } = false;

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _db.Sessions.Add(Session);
            await _db.SaveChangesAsync();
            SessionSaved = true;

            // Nutze die BaseUrl aus der Konfiguration (z. B. ngrok-Adresse)
            string baseUrl = _config["App:BaseUrl"];
            string sessionUrl = $"{baseUrl}/Feedback/JoinSession?sessionId={Session.Id}";

            using var qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(sessionUrl, QRCodeGenerator.ECCLevel.Q);
            var svgQrCode = new SvgQRCode(qrCodeData);
            string svgImage = svgQrCode.GetGraphic(5);

            var svgBytes = System.Text.Encoding.UTF8.GetBytes(svgImage);
            var base64Svg = Convert.ToBase64String(svgBytes);
            QrCodeBase64 = $"data:image/svg+xml;base64,{base64Svg}";

            return Page(); // Zeige das QR-Bild an
        }
    }
}