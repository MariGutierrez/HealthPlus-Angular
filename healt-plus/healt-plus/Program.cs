using healt_plus.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/////////////////             CONEXI�N             ///////////
var connectionString = builder.Configuration.GetConnectionString("cadenaSQL");

// AGREGAMOS LA CONFIGURACI�N PARA SQL
builder.Services.AddDbContext<HealtContext>(options => options.UseSqlServer(connectionString));

// DEFINIMOS LA NUEVA POL�TICA DE CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ACTIVAMOS LA POL�TICA CORS
app.UseCors("NuevaPolitica");

app.UseAuthorization();

app.MapControllers();

app.Run();
