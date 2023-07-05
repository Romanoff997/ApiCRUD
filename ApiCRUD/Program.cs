using ApiCRUD.Domain.Repositories.Abstract;
using ApiCRUD.Domain.Repositories.EntityFramework;
using ApiCRUD.Domain.Repositories;
using ApiCRUD.Services;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
Config config = builder.Configuration.GetSection("Settings").Get<Config>();
builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(config.ConnectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IClientModelRepository, EFClientModelRepository>();;
builder.Services.AddTransient<DataManager>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// global error handler
app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapControllers();

app.Run();
