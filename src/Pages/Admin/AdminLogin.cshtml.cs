using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EventFeedbackApp.Pages.Admin
{
    public class AdminLoginModel : PageModel
    {
        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public IActionResult OnPost()
        {
            if (Password == "DavidA123") // Admin-Passwort
            {
                Response.Cookies.Append("IsAdmin", "true", new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    Expires = DateTimeOffset.UtcNow.AddHours(12)
                });

                return Redirect("/Admin/CreateSession");
            }

            ErrorMessage = "Falsches Passwort.";
            return Page();
        }
    }
}