using Microsoft.EntityFrameworkCore;
using EventFeedbackApp.Models;

namespace EventFeedbackApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Session> Sessions => Set<Session>();
        public DbSet<Question> Questions => Set<Question>();
        public DbSet<Option> Options => Set<Option>();
        public DbSet<Response> Responses => Set<Response>();
    }
}