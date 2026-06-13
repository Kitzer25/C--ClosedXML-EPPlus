using System.Text.Json.Serialization;
using Application.Configuration;
using Infraestructure.Configuration;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

/*
 * Configurations
 */
builder.Services.AddInfraestructure();
builder.Services.AddApplication();

/*
 * Context
 */
builder.Services.AddDbContext<AppDbContext>(o =>
{
    var stringscnn = builder.Configuration.GetConnectionString("DefaultString");

    o.UseMySql(
        stringscnn,
        ServerVersion.AutoDetect(stringscnn)
    );
});

/*
 * Swagger
 */
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
