using EventFeedbackApp.Data;
using EventFeedbackApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace EventFeedbackApp.Pages.Admin
{
    public class CreateQuestionModel : PageModel
    {
        private readonly AppDbContext _db;
        public CreateQuestionModel(AppDbContext db) => _db = db;

        [BindProperty(SupportsGet = true)]
        public int SessionId { get; set; }

        [BindProperty]
        public Question Question { get; set; } = new();

        [BindProperty]
        public List<string> OptionTexts { get; set; } = new List<string> { "", "" };

        public async Task<IActionResult> OnGetAsync(int id)
        {
            SessionId = id;
            if (!await _db.Sessions.AnyAsync(s => s.Id == id))
                return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            Question.SessionId = SessionId;
            _db.Questions.Add(Question);
            await _db.SaveChangesAsync();

            if (Question.Type == QuestionType.SingleChoice)
            {
                foreach (var text in OptionTexts.Where(t => !string.IsNullOrWhiteSpace(t)))
                {
                    _db.Options.Add(new Option
                    {
                        QuestionId = Question.Id,
                        Text = text.Trim()
                    });
                }
            }
            await _db.SaveChangesAsync();

            return RedirectToPage("/Admin/ResultsSignalR", new { id = SessionId });
        }
    }
}