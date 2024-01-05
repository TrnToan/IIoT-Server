using MagicMirrorIotServer.Api.Application.Host;
using MagicMirrorIotServer.Api.Application.Mapper;
using MagicMirrorIotServer.Api.Hubs;
using MagicMirrorIotServer.Api.Infrastructure.Communication;
using MagicMirrorIotServer.Domain.SeedWork;
using MagicMirrorIotServer.Infrastructure;
using MagicMirrorIotServer.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CloudConnection"), b => b.MigrationsAssembly("MagicMirrorIotServer.Api"));
    options.EnableSensitiveDataLogging();
});

builder.Services.AddSignalR();

builder.Services.AddAutoMapper(typeof(ModelToViewModelProfile));

builder.Services.AddScoped<IEonNodeRepository, EonNodeRepository>();
builder.Services.AddScoped<ITagReadingRepository, TagReadingRepository>();

builder.Services.AddScoped<TagValueChangedEventHandler>();

builder.Services.AddSingleton<TagValueCache>();
builder.Services.AddSingleton<SparkplugAdapter>();

builder.Services.AddHostedService<IotServerHost>();

builder.Services.AddScoped<NotificationHub>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });

    options.AddPolicy("AllowAll", builder =>
    {
        builder
        .WithOrigins("localhost", "http://localhost:3000", "http://localhost:5173",
        "https://web-cha-tags-manager.vercel.app")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials();
    });
});
    
var app = builder.Build();

app.UseCors("AllowAll");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<NotificationHub>("/notificationHub");

app.Run();
