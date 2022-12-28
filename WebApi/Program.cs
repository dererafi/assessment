using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using repository;
using shared;

[assembly: ApiController]
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IBetMoveService, BetMoveService>();
builder.Services.AddScoped<IMapper, Mapper>();

using (var db = new SqlLiteDbContext())
{
    db.Database.EnsureCreated();
    db.Database.Migrate();
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapControllers();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    //options.RoutePrefix = string.Empty;
    options.RoutePrefix =  "api/game";
});

app.UseAuthorization();

app.MapGet("/", () => "WEB API is up and running. Navigate to https://localhost:7036/api/game/");

app.Run();