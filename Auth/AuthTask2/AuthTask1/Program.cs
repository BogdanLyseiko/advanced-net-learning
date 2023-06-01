using AuthTask2.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
string domain = $"https://{builder.Configuration["Auth0:Domain"]}/";

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.Authority = domain;
    options.Audience = builder.Configuration["Auth0:Audience"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = ClaimTypes.NameIdentifier
    };
});

// Add services to the container.
builder.Services.AddAuthorization();

builder.Services.AddAuth0AuthenticationClientCore("dev-nhlmu3tufd5rx8vp.us.auth0.com");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/carting"),
        appBuilder =>
        {
            appBuilder.UseMiddleware<AccessTokenLoggingMiddleware>();
        });

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
