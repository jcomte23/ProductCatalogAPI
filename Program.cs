using DotNetEnv;
using MongoDB.Driver;
using ProductCatalogAPI.Models;

Env.Load();

// Cargar las variables de entorno
var mongoConnectionString = Environment.GetEnvironmentVariable("MONGODB_URI");
var databaseName = Environment.GetEnvironmentVariable("MONGODB_DATABASE");
var collectionName = Environment.GetEnvironmentVariable("MONGODB_COLLECTION");

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    return new MongoClient(mongoConnectionString);
});

builder.Services.AddSingleton(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var database = client.GetDatabase(databaseName);
    return database.GetCollection<Category>(collectionName);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseWelcomePage(new WelcomePageOptions
{
    Path = "/"
});

app.MapControllers();

app.Run();