using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EventFeedbackApp.Data;
using EventFeedbackApp.Models;

namespace EventFeedbackApp.Pages.Admin
{
    public class CreateQuestionModel : PageModel
    {
        private readonly AppDbContext _db;
        public CreateQuestionModel(AppDbContext db) => _db = db;

        [BindProperty]
        public CreateQuestionViewModel Input { get; set; } = new();

        // <-- Fügt die SessionId beim GET-Request hinzu
        public void OnGet(int sessionId)
        {
            Input.SessionId = sessionId;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Ab hier ist Input.SessionId garantiert korrekt ≠ 0
            var q = new Question
            {
                SessionId = Input.SessionId,
                Text = Input.Text,
                Type = Input.Type
            };
            _db.Questions.Add(q);
            await _db.SaveChangesAsync();

            if (Input.Type == QuestionType.SingleChoice)
            {
                var opts = Input.OptionTexts
                    .Where(t => !string.IsNullOrWhiteSpace(t))
                    .Select(t => new Option { QuestionId = q.Id, Text = t.Trim() })
                    .ToList();
                if (opts.Any())
                {
                    _db.Options.AddRange(opts);
                    await _db.SaveChangesAsync();
                }
            }

            return RedirectToPage("/Feedback/Questions", new { id = Input.SessionId });
        }
    }
}
