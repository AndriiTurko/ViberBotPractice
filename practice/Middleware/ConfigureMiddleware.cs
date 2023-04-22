namespace practice.Middleware
{
    public static class ConfigureMiddleware
    {
        public static void ConfigureExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }

        public static void ConfigureViberWebhookMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ViberWebhookMiddleware>();
        }
    }
}
