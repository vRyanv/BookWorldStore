﻿namespace BookWorldStore.Middleware
{
    public class NotFoundMiddleware
    {
        private readonly RequestDelegate _next;

        public NotFoundMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == 404)
            {
                context.Response.Redirect("/Home/NotFoundPage");
            }
        }
    }
}
