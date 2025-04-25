namespace EventFeedbackApp.Models
{
    public class Option
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
        public string Text { get; set; } = null!;
    }
}
