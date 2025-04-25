namespace EventFeedbackApp.Models
{
    public enum QuestionType { SingleChoice, Rating, Text }
    public class Question
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public Session Session { get; set; } = null!;
        public string Text { get; set; } = null!;
        public QuestionType Type { get; set; }
        public ICollection<Option> Options { get; set; } = new List<Option>();
    }
}
