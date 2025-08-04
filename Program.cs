using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaDotNetApi7.Extensions;
using PruebaTecnicaDotNetApi7.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configuracion de la cadena de conexion a la base de datos SQL Server
builder.Services.AddDatabaseServices(
    builder.Configuration.GetConnectionString("DefaultConnection")!
);

// configuracion de versionado de API por default, estableciendo la version 1.0 en "Microsoft.AspNetCore.Mvc.ApiVersion(1, 0)"
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new QueryStringApiVersionReader("version")
        );

    options.Conventions.Add(new Microsoft.AspNetCore.Mvc.Versioning.Conventions.VersionByNamespaceConvention());
});

// configuracion de versionado de API explorador para Swagger
builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI( config => config.SwaggerEndpoint("/swagger/v1/swagger.json", "Prueba Tecnica DotNet API 7 v1")
        );
    await app.InitializeDatabaseAsync();
}

await app.InitializeDatabaseAsync();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
