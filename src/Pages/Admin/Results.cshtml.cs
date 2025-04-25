using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventFeedbackApp.Pages.Admin
{
    public class ResultsModel : PageModel
    {
        public int SessionId { get; set; }

        public void OnGet(int id)
        {
            SessionId = id;
        }
    }
}