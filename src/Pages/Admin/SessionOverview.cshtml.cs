using EventFeedbackApp.Data;
using EventFeedbackApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EventFeedbackApp.Pages.Admin
{
    public class SessionOverviewModel : PageModel
    {
        private readonly AppDbContext _db;

        public SessionOverviewModel(AppDbContext db)
        {
            _db = db;
        }

        public List<Session> Sessions { get; set; } = new();
        public string NgrokBaseUrl { get; set; } = "https://588e-178-38-5-163.ngrok-free.app";

        public async Task OnGetAsync()
        {
            Sessions = await _db.Sessions.OrderBy(s => s.Id).ToListAsync();
        }
    }
}