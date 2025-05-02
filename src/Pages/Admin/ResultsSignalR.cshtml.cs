using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventFeedbackApp.Pages.Admin
{
    public class ResultsSignalRModel : PageModel
    {
        public int SessionId { get; set; }
        public void OnGet(int id) => SessionId = id;
    }
}