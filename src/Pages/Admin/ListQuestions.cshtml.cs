using EventFeedbackApp.Data;
using EventFeedbackApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ListQuestionsModel : PageModel
{
    private readonly AppDbContext _db;
    public ListQuestionsModel(AppDbContext db) => _db = db;

    [BindProperty(SupportsGet = true)]
    public int SessionId { get; set; }
    public IList<Question> Questions { get; set; } = new List<Question>();

    public async Task OnGetAsync()
    {
        Questions = await _db.Questions
            .Include(q => q.Options)
            .Where(q => q.SessionId == SessionId)
            .ToListAsync();
    }
}