using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Quote.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

//string connectionString = Environment.GetEnvironmentVariable("SQLConnectionString");
//IConfigurationRoot configuration = new ConfigurationBuilder()
//       .SetBasePath(projectPath)
//       .AddJsonFile("appsettings.json")
//       .Build();
string connectionString = configuration.GetConnectionString("SQLConnectionString");
builder.Services.AddDbContext<QuotesContext>(options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll",
        builder => {
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
