namespace EventFeedbackApp.Models
{
    public class Response
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;
        public string Answer { get; set; } = null!; // Option.Id oder Freitext
        public DateTime Timestamp { get; set; }
    }
}
