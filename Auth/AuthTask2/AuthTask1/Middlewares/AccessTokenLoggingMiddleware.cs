using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;

namespace AuthTask2.Middlewares
{
    public class AccessTokenLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AccessTokenLoggingMiddleware> _logger;

        public AccessTokenLoggingMiddleware(RequestDelegate next, ILogger<AccessTokenLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api/carting"))
            {
                string? accessToken = context.Request.Headers["Authorization"][0];

                if (!string.IsNullOrEmpty(accessToken) && accessToken.StartsWith("Bearer "))
                {
                    // Remove the "Bearer " prefix from the token
                    string token = accessToken[7..];

                    JwtSecurityTokenHandler tokenHandler = new();

                    // Read the JWT token
                    JwtSecurityToken? jwtToken = tokenHandler.ReadJwtToken(token);

                    // Get the claims from the token
                    IEnumerable<System.Security.Claims.Claim> claims = jwtToken.Claims;


                    foreach (System.Security.Claims.Claim claim in context.User.Claims)
                    {
                        // since it is test project will log in debugger, for real project we will use logger
                        Debug.WriteLine($"{claim.Type}: {claim.Value}");
                    }
                }
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
