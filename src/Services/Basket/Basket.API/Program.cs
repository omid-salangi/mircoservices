using Basket.API.Common;
using Basket.API.GrpcServices;
using Catalog.API.WebFramework.Configuration;
using Discount.Grpc.Protos;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var _sitesettings = builder.Configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.ResolveConflictingActions(a => a.First());
});
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = _sitesettings.RedisSettings.ConnectionString;
});
builder.DependencyContainerWithAutofac();

builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(_sitesettings.EventBusSettings.HostAddress);
    });
});


//builder.Services.AddMassTransitHostedService();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(2, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;
    config.ReportApiVersions = true;
    config.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o => // this file generated on this project by build it
{
    o.Address = new Uri(_sitesettings.GrpcSettings.DiscountUrl);
});
builder.Services.AddScoped<DiscountGrpcService>(); // i dont know why autofac dont work

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
