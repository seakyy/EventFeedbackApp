namespace EventFeedbackApp.Middleware
{
    public class AdminAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private const string AdminCookieName = "IsAdmin";

        public AdminAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value;

            if (path != null && path.StartsWith("/Admin", StringComparison.OrdinalIgnoreCase))
            {
                var isAdmin = context.Request.Cookies.TryGetValue(AdminCookieName, out var val) && val == "true";

                if (!isAdmin && !path.Contains("/Admin/AdminLogin", StringComparison.OrdinalIgnoreCase))
                {
                    context.Response.Redirect("/Admin/AdminLogin");
                    return;
                }
            }

            await _next(context);
        }
    }

}
