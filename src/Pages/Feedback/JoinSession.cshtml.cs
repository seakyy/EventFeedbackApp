using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventFeedbackApp.Pages.Feedback
{
    public class JoinSessionModel : PageModel
    {
        [BindProperty]
        public int SessionId { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            return RedirectToPage("Questions", new { id = SessionId });
        }
    }
}