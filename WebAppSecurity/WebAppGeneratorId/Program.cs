using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebAppGeneratorId.Data;
using WebAppGeneratorId.EndPoints;

var builder = WebApplication.CreateBuilder();

builder.Services.AddDbContext<PersonDbContext>(op => op.UseSqlite(
    builder.Configuration.GetConnectionString("Sqlite")));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.MapServicePersons();

WebApplication app = builder.Build();



app.MapGroup("/persons")
    .MapPersonEndPoints();

app.Run();