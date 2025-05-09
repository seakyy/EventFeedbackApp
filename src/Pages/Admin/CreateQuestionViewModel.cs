namespace EventFeedbackApp.Pages.Admin
{
    using EventFeedbackApp.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CreateQuestionViewModel
    {
        [Required]
        public string Text { get; set; } = string.Empty;

        public QuestionType Type { get; set; }

        public List<string> OptionTexts { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int SessionId { get; set; }
    }

}
