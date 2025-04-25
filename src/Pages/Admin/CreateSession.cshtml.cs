using EventFeedbackApp.Data;
using EventFeedbackApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventFeedbackApp.Pages.Admin
{
    public class CreateSessionModel : PageModel
    {
        private readonly AppDbContext _db;
        public CreateSessionModel(AppDbContext db) => _db = db;

        [BindProperty]
        public Session Session { get; set; } = new();

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            _db.Sessions.Add(Session);
            await _db.SaveChangesAsync();
            return RedirectToPage("/Admin/CreateSession", new { saved = true });
        }
    }
}