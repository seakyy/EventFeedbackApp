using Microsoft.AspNetCore.Mvc.RazorPages;
using EventFeedbackApp.Models;

namespace EventFeedbackApp.Pages.Feedback
{
    public class QuestionsModel : PageModel
    {
        public int SessionId { get; set; }
        public List<Question> Questions { get; set; } = new();

        public void OnGet(int id)
        {
            SessionId = id;

            // POC: Dummy-Frage(n) – später aus DB laden
            Questions = new List<Question>
            {
                new Question
                {
                    Id = 1,
                    Text = "Wie hat Ihnen die Session gefallen?",
                    Type = QuestionType.SingleChoice,
                    Options = new List<Option>
                    {
                        new Option { Id = 1, Text = "👍 Gut" },
                        new Option { Id = 2, Text = "👌 OK" },
                        new Option { Id = 3, Text = "👎 Schlecht" }
                    }
                }
            };
        }
    }
}