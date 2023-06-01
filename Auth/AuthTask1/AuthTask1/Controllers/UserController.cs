using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using AuthTask1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthTask1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthenticationApiClient _auth0Client;
        private readonly IConfiguration _configuration;

        public UserController(IAuthenticationApiClient auth0Client, IConfiguration configuration)
        {
            _auth0Client = auth0Client;
            _configuration = configuration;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            ResourceOwnerTokenRequest request = new()
            {
                ClientId = _configuration["Auth0:ClientId"],
                ClientSecret = _configuration["Auth0:ClientSecret"],
                Audience = _configuration["Auth0:Audience"],
                Realm = "Username-Password-Authentication",
                Username = loginRequest.Username,
                Password = loginRequest.Password,
                Scope = "openid"
            };

            AccessTokenResponse response = await _auth0Client.GetTokenAsync(request);

            if (response == null)
            {
                // Authentication failed
                return Unauthorized();
            }

            // Authentication succeeded, return the token
            return Ok(new { Token = response.AccessToken, RefreshToken = response.RefreshToken });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            // Call the Auth0 token endpoint to refresh the token
            AccessTokenResponse response = await _auth0Client.GetTokenAsync(new RefreshTokenRequest
            {
                ClientId = _configuration["Auth0:ClientId"],
                ClientSecret = _configuration["Auth0:ClientSecret"],
                RefreshToken = refreshToken,
            });

            if (response == null)
            {
                // Handle token refresh failure
                return BadRequest();
            }

            // Get the new access token from the refresh token response
            string accessToken = response.AccessToken;

            // Return the new access token
            return Ok(new { AccessToken = accessToken, RefreshToken = response.RefreshToken });
        }
    }
}
