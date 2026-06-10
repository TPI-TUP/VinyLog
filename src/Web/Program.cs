using Application.Services;
using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.Repositories;
using Domain.Interfaces;
using Web.Middlewares;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<ICustomAuthenticationService, AutenticacionService>();
builder.Services.AddHttpClient<IYoutubeService, YoutubeService>();
builder.Services.AddScoped<GlobalExceptionHandlingMiddleware>();

builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        // 1. EQUIVALENTE A: setupAction.AddSecurityDefinition("ApiBearerAuth", ...)
        var schemeName = "ApiBearerAuth";

        var securityScheme = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer", // .NET 10 requiere minúsculas para estándares de OpenAPI 3.1
            BearerFormat = "JWT",
            Description = "Acá pegar el token generado al loguearse."
        };

        // Instanciar componentes si vienen nulos y añadir la definición
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
        document.Components.SecuritySchemes[schemeName] = securityScheme;

        // 2. EQUIVALENTE A: setupAction.AddSecurityRequirement(...)
        // CAMBIO CRÍTICO .NET 10: Desaparece 'Reference = new OpenApiReference...'.
        // Ahora se usa 'OpenApiSecuritySchemeReference' pasándole el nombre y el documento raíz.
        var schemeReference = new OpenApiSecuritySchemeReference(schemeName, document);

        var requirement = new OpenApiSecurityRequirement
        {
            [schemeReference] = [] // Sintaxis limpia para los alcances (scopes)
        };

        // Asignar el requerimiento de seguridad de forma global al documento
        document.Security = new List<OpenApiSecurityRequirement> { requirement };

        return Task.CompletedTask;
    });
});



builder.Services.AddAuthentication("Bearer")
   .AddJwtBearer(options =>
   {
       var secretKey =
       builder.Configuration["Authentication:SecretForKey"]
       ?? throw new InvalidOperationException(
           "SecretForKey no está configurada.");
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = builder.Configuration["Authentication:Issuer"],
           ValidAudience = builder.Configuration["Authentication:Audience"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey))
       };
   });

builder.Services.Configure<AutenticacionService.AutenticacionServiceOptions>(builder.Configuration.GetSection("Authentication"));


// Configuracion de la Base de Datos
string connectionString = builder.Configuration["ConnectionStrings:SQLiteConnectionString"]!;
var connection = new SqliteConnection(connectionString);
connection.Open();

using (var command = connection.CreateCommand())
{
    command.CommandText = "PRAGMA journal_mode = DELETE;";
    command.ExecuteNonQuery();
}

builder.Services.AddDbContext<ApplicationContext>(dbContextOptions => dbContextOptions.UseSqlite(connection));

var app = builder.Build();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
// Configure the HTTP request pipeline.

// if (app.Environment.IsDevelopment())
// {
app.MapOpenApi();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "My API V1");
});
// }

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
