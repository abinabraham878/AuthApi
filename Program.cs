using AuthApi.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("My Policy", builder =>
    {
        builder.AllowAnyOrigin().
        AllowAnyHeader().
        AllowAnyMethod();
    });
});
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnStrn"));
});

builder.Services.AddDbContext<DepartmentDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnStrn"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("My Policy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
