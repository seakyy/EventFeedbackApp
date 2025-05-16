using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using EventFeedbackApp.Data;
using EventFeedbackApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventFeedbackApp.Pages.Admin
{
    public class CreateQuestionModel : PageModel
    {
        private readonly AppDbContext _db;
        public CreateQuestionModel(AppDbContext db) => _db = db;

        [BindProperty]
        public CreateQuestionViewModel Input { get; set; } = new();

        public void OnGet(int sessionId)
        {
            Input.SessionId = sessionId;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Security-Feature: Prüfen ob die Session überhaupt existiert
            var sessionExists = await _db.Sessions.AnyAsync(s => s.Id == Input.SessionId);
            if (!sessionExists)
            {
                ModelState.AddModelError("", "Die angegebene Session existiert nicht.");
                return Page();
            }

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

            return RedirectToPage("/Feedback/Questions", new { id = Input.SessionId }); // Only works locally


        }

    }
}
