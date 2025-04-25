namespace EventFeedbackApp.Models
{
    public class Session
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
