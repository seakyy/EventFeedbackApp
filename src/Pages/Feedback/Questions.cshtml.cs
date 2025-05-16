using EventFeedbackApp.Data;
using EventFeedbackApp.Hubs;
using EventFeedbackApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace EventFeedbackApp.Pages.Feedback
{
    public class QuestionsModel : PageModel
    {
        private readonly AppDbContext _db;
        private readonly IHubContext<FeedbackHub> _hub;

        public QuestionsModel(AppDbContext db, IHubContext<FeedbackHub> hub)
        {
            _db = db;
            _hub = hub;
        }

        // ID aus der Route übergeben
        [BindProperty]
        public int SessionId { get; set; }

        public List<Question> Questions { get; set; } = new();

        // Antworten werden gebunden
        [BindProperty]
        public Dictionary<string, string> Answers { get; set; } = new();

        public async Task OnGetAsync(int id)
        {
            SessionId = id;
            Questions = await _db.Questions
                                 .Include(q => q.Options)
                                 .Where(q => q.SessionId == id)
                                 .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var entry in Answers)
            {
                if (entry.Key.StartsWith("q_") &&
                    int.TryParse(entry.Key.Substring(2), out var qid))
                {
                    _db.Responses.Add(new Response
                    {
                        QuestionId = qid,
                        Answer = entry.Value,
                        Timestamp = DateTime.UtcNow
                    });
                }
            }
            await _db.SaveChangesAsync();
            await _hub.Clients.All.SendAsync("ReceiveUpdate", SessionId);

            return RedirectToPage("Questions", new { id = SessionId });
        }
    }
}