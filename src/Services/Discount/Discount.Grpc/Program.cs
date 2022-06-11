using Discount.Grpc.Common;
using Discount.Grpc.Context;
using Discount.Grpc.Services;
using Discount.Grpc.Webframework.Configuration;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var _sitesettings = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddAutoMappers(); // service
builder.Services.AddDbContext<DiscountContext>(options =>
{
    options.UseNpgsql(_sitesettings.PostgresDbSettings.PostgresDbConnection);
});
builder.DependencyContainerWithAutofac();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>(); // for accepting requests to grpc 
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DiscountContext>();
    await db.Database.MigrateAsync();
}
app.Run();
