using System.Net.Http.Headers;  
using System.Text;

namespace ITVTestTask.Services
{
    public class BasicAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public BasicAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
            {
                context.Response.StatusCode = 401;
                context.Response.Headers["WWW-Authenticate"] = "Basic realm=\"ITVTestTask\"";
                await context.Response.WriteAsync("Заголовок авторизации отсутствует.");
                return;
            }

            var authHeader = AuthenticationHeaderValue.Parse(authorizationHeader);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter ?? string.Empty);
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);

            if (credentials.Length != 2 || credentials[0] != "Maxim" || credentials[1] != "Plusnin")
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Неверные имя или пароль.");
                return;
            }

            await _next(context);
        }
    }
    public static class BasicAuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseBasicAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BasicAuthenticationMiddleware>();
        }
    }
}
