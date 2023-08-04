using ApiCRUD.Domain.Repositories.Abstract;
using ApiCRUD.Domain.Repositories.EntityFramework;
using ApiCRUD.Domain.Repositories;
using ApiCRUD.Services;
using Microsoft.EntityFrameworkCore;
using ApiCRUD.Helpers;
using ApiCRUD.Mappings;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
Config config = builder.Configuration.GetSection("Settings").Get<Config>();
builder.Services.AddDbContext<MyDbContext>(options => options.UseSqlServer(config.ConnectionString));
builder.Services.AddSingleton<IJsonConverter>(provider => {

    return new JsonNewtonConverter();
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IClientModelRepository, EFClientModelRepository>();;

builder.Services.AddTransient<DataManager>();
builder.Services.AddTransient<MappingServiceNative>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


var app = builder.Build();

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
