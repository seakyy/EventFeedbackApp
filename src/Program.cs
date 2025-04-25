using EventFeedbackApp.Data;
using Microsoft.EntityFrameworkCore;

namespace EventFeedbackApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // DbContext
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Data Source=feedback.db"));

            // Razor Pages
            builder.Services.AddRazorPages();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.MapGet("/api/admin/sessions/{id}/results", async (int id, AppDbContext db) =>
            {
                var stats = await db.Responses
                    .Where(r => db.Questions.Any(q => q.Id == r.QuestionId && q.SessionId == id))
                    .GroupBy(r => new { r.QuestionId, r.Answer })
                    .Select(g => new {
                        QuestionId = g.Key.QuestionId,
                        Answer = g.Key.Answer,
                        Count = g.Count()
                    })
                    .ToListAsync();
                return Results.Ok(stats);
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.MapRazorPages();
            app.Run();
        }
    }
}
