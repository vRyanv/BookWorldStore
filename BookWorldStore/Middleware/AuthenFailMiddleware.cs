namespace BookWorldStore.Middleware
{
    public class AuthenFailMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenFailMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == 403)
            {
                context.Response.Redirect("/Home/Login");
            }
        }
    }
}
