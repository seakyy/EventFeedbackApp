using System.IO;
using EventFeedbackApp.Data;
using EventFeedbackApp.Hubs;
using EventFeedbackApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbPath = Path.Combine(builder.Environment.ContentRootPath, "feedback.db");

// 1) DbContext mit SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

// 2) SignalR
builder.Services.AddSignalR();

// 3) Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// 4) Automatisch Migrationen anwenden
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (!db.Questions.Any())
    {
        var q = new Question { SessionId = 1, Text = "Wie fandest du die Session?", Type = QuestionType.SingleChoice };
        db.Questions.Add(q);
        await db.SaveChangesAsync();
        db.Options.AddRange(
            new Option { QuestionId = q.Id, Text = "👍 Gut" },
            new Option { QuestionId = q.Id, Text = "👌 OK" },
            new Option { QuestionId = q.Id, Text = "👎 Schlecht" }
        );
        await db.SaveChangesAsync();
    }
}


// 5) Middleware & Routing
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// 6) Endpoints

// API: Ergebnisse abrufen
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

// SignalR-Hub
app.MapHub<FeedbackHub>("/feedbackHub");

// Razor Pages
app.MapRazorPages();

app.Run();