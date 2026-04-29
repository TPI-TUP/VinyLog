using Application.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ArtistService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Configure the SQLite Connection
var connection = new SqliteConnection("Data Source=VinyLogDataBase.db");
connection.Open();

// Set journal mode to DELETE using PRAGMA statement
using (var command = connection.CreateCommand())
{
    command.CommandText = "PRAGMA journal_mode = DELETE;";
    command.ExecuteNonQuery();
}

builder.Services.AddDbContext<ApplicationContext>(dbContextOptions => dbContextOptions.UseSqlite(connection));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
