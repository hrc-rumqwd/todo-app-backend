using Asp.Versioning;
using Scalar.AspNetCore;
using TodoApp.Application.Extensions;
using TodoApp.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddApplicationDependencies()
    .AddInfrastructureDependencies(builder.Configuration);

builder.Services.AddApiVersioning(cfg =>
{
    cfg.DefaultApiVersion = new ApiVersion(1, 0);
    cfg.AssumeDefaultVersionWhenUnspecified = true;
    cfg.ReportApiVersions = true;
    cfg.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("v"),
        new HeaderApiVersionReader("API-X-Version"),
        new UrlSegmentApiVersionReader()
    );
})
.AddApiExplorer(cfg =>
{
    cfg.GroupNameFormat = "'v'VVV";
    cfg.SubstituteApiVersionInUrl = true;
});

builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseRouting();

app.MapDefaultControllerRoute();

app.Run();