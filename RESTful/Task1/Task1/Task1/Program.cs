using Microsoft.EntityFrameworkCore;
using System;
using Task1.DAL;
using Task1.DAL.Entities;
using Task1.DAL.Repositories.Implementations;
using Task1.DAL.Repositories.Interfaces;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase("Task-1"));
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Add testing data
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

if (context is not null)
{
    context.Categories.Add(new Category
    {
        Id = 1,
        Name = "Test1"
    });

    context.Items.Add(new Item
    {
        Name = "Item1",
        CategoryId = 1
    });

    context.SaveChanges();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
