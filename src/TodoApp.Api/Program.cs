using Asp.Versioning;
using Scalar.AspNetCore;
using TodoApp.Application.Extensions;
using TodoApp.Infrastructure.Extensions;
using TodoApp.Infrastructure.Extensions.Persistence;

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

builder.Services.AddCors(cfg =>
{
    cfg.AddPolicy("AllowLocalOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors("AllowLocalOrigins");

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.UseAuthorization();

app.MapDefaultControllerRoute();

await app.UseSeedData();

app.Run();

