using GraphQLTask.Configuration;
using GraphQLTask.Entities;
using GraphQLTask.GraphQL.Mutations;
using GraphQLTask.GraphQL.Queries;
using GraphQLTask.Repositories.Implementations;
using GraphQLTask.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IItemRepository, ItemRepository>();
builder.Services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("GraphQlAPi"));
builder.Services.AddGraphQLServer()
    .AddQueryType(q => q.Name("Query"))
    .AddMutationType(q => q.Name("Mutation"))
    .AddType<CategoryQuery>()
    .AddType<ItemQuery>()
    .AddType<CategoryMutation>()
    .AddType<ItemMutation>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapGraphQL();

app.MapControllers();
SeedDataHelper.InitializeInMemoryDatabase(app);

app.Run();
