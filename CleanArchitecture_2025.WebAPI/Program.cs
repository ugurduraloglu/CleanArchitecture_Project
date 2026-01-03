using CleanArchitecture_2025.Application;
using CleanArchitecture_2025.Infrastructure;
using CleanArchitecture_2025.WebAPI.Controllers;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.RateLimiting;
using Scalar.AspNetCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCors();
builder.Services.AddOpenApi();
builder.Services.AddControllers().AddOData(opt =>
        opt
        .Select()
        .Filter()
        .Count()
        .Expand()
        .OrderBy()
        .SetMaxTop(null)
        //.EnableQueryFeatures()
        .AddRouteComponents("odata", AppODataController.GetEdmModel())
);
builder.Services.AddRateLimiter(x =>
x.AddFixedWindowLimiter("fixed", cfg =>
{
    cfg.QueueLimit = 100;
    cfg.Window = TimeSpan.FromSeconds(1);
    cfg.PermitLimit = 100;
    cfg.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
}));

var app = builder.Build();


app.MapDefaultEndpoints();
app.MapOpenApi();
app.MapScalarApiReference();
app.MapDefaultEndpoints();

app.UseCors(x => 
x.AllowAnyHeader()
.AllowCredentials()
.AllowAnyMethod()
.SetIsOriginAllowed(t => true));
//Yazýlacak her kontroller RequireRateLimiting ile fixed limiter a girecek
app.MapControllers().RequireRateLimiting("fixed");

app.Run();
