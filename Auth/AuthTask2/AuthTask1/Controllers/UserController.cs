using Auth0.AuthenticationApi;
using Auth0.AuthenticationApi.Models;
using AuthTask2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthTask2.Controllers
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
    }
}
