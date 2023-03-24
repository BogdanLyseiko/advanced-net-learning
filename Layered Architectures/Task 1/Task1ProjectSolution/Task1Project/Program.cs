using FluentValidation;
using FluentValidation.AspNetCore;
using Task1Project.BLL.Implementations;
using Task1Project.BLL.Interfaces;
using Task1Project.BLL.Validators;
using Task1Project.BLL.ViewModels;
using Task1Project.DAL.Configuration;
using Task1Project.DAL.Entities;
using Task1Project.DAL.Repositories.Implementations;
using Task1Project.DAL.Repositories.Interfaces;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<ILiteDbContext, LiteDbContext>();
builder.Services.AddTransient<IRepository<Cart>, Repository<Cart>>();
builder.Services.AddTransient<ICartService, CartService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddScoped<IValidator<CartViewModel>, CartViewModelValidator>();

builder.Services.Configure<LiteDbOptions>(builder.Configuration.GetSection("LiteDbOptions"));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();