using Discount.API.Data;
using Microsoft.EntityFrameworkCore;
using Discount.API.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Discount.API.Webframework.Configuration;

var builder = WebApplication.CreateBuilder(args);

var _sitesettings = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(a => a.First());
});

builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(2, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
    config.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddDbContext<DiscountContext>(options =>
{
    options.UseNpgsql(_sitesettings.PostgresDbSettings.PostgresDbConnection);
});

builder.DependencyContainerWithAutofac();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DiscountContext>();
    await db.Database.MigrateAsync();
}
app.UseAuthorization();

app.MapControllers();

app.Run();
