using Discount.API.Data;
using Microsoft.EntityFrameworkCore;
using Discount.API.Common;
var builder = WebApplication.CreateBuilder(args);

var _sitesettings = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options.UseNpgsql(_sitesettings.postgresDbSettings.PostgreDbConnection);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
