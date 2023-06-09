using APIGateway.Aggregators;
using CacheManager.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerForOcelot(builder.Configuration);

// Configure JWT Bearer authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.Authority = "https://localhost:7006";
            options.Audience = "your_audience";
        });

// Configure authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("admin");
    });
});

builder.Services.AddOcelot(builder.Configuration)
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle()
         .WithExpiration(ExpirationMode.Sliding, TimeSpan.FromSeconds(60));
    })
    .AddSingletonDefinedAggregator<CatalogItemAggregator>();
builder.Services.AddMemoryCache();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerForOcelotUI(opt =>
    {
        opt.PathToSwaggerGenerator = "/swagger/docs";
    });
}


app.UseHttpsRedirection();

// Use authentication middleware
app.UseAuthentication();

// Use authorization middleware
app.UseAuthorization();

app.UseOcelot().Wait();

app.UseAuthorization();

app.MapControllers();

app.Run();
