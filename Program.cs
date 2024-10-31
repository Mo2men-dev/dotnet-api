using BookStoreApi.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<BookStoreContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("BookStoreDb")));

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

var app = builder.Build();

app.UseCors("AllowAll");

// Other middleware
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
