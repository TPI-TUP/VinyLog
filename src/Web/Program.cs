using Application.Services;
using Application.Interfaces;
using Infrastructure.Data;
using Infrastructure.Services;
using Infrastructure.Repositories;
using Domain.Interfaces;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<ArtistService>();
builder.Services.AddScoped<IArtistRepository, ArtistRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICustomAuthenticationService, AutenticacionService>();


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
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = builder.Configuration["Authentication:Issuer"],
           ValidAudience = builder.Configuration["Authentication:Audience"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
       };
   });

builder.Services.Configure<IUserRepository>(builder.Configuration.GetSection("Authentication"));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// builder.Services.AddSwaggerGen(setupAction =>
// {
//     setupAction.AddSecurityDefinition("ApiBearerAuth", new OpenApiSecurityScheme()
//     {
//         Type = SecuritySchemeType.Http,
//         Scheme = "Bearer",
//         Description = "Aca pegar el token generado al loguearse."
//     });

// setupAction.AddSecurityRequirement(new OpenApiSecurityRequirement
// {
//     {
//         new OpenApiSecurityScheme
//         {
//             Reference = new OpenApiReference
//             {
//                 Type = ReferenceType.SecurityScheme,
//                 Id = "ApiBearerAuth"
//             }
//         }, new List<string>()
//     }
// });
// });

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
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
