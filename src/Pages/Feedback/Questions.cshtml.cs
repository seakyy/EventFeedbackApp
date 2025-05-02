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

        // Hier wird die ID aus der Route übergeben
        [BindProperty]
        public int SessionId { get; set; }

        public List<Question> Questions { get; set; } = new();

        // Antworten werden gebunden
        [BindProperty]
        public Dictionary<string, string> Answers { get; set; } = new();

        // ✔️ Nimm die id als Parameter entgegen und setze SessionId
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
            // SessionId bleibt erhalten
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

            // Leite zurück auf dieselbe Seite, damit SessionId erneut über OnGetAsync übernommen wird
            return RedirectToPage("Questions", new { id = SessionId });
        }
    }
}