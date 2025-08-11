namespace CulinaryAPI.Middleware.JwtCookie
{
    public class JwtCookieMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Chỉ thêm Authorization header nếu chưa có
            if (!context.Request.Headers.ContainsKey("Authorization"))
            {
                var token = context.Request.Cookies["AccessToken"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Append("Authorization", $"Bearer {token}");
                }
            }

            await _next(context);
        }
    }
}
