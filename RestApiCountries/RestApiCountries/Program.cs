using System.Reflection;
using Microsoft.OpenApi.Models;
using Refit;
using RestApiCountries.DataSource;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "EU Countries Web API",
        Description = "An ASP.NET Core Web API .NET 6 application that consumes data from REST Countries API" +
                      " and returns selected information on EU countries"
    });

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

// Refit configuration
builder.Services.AddRefitClient<IRestCountriesApi>()
       .ConfigureHttpClient(httpClient => httpClient.BaseAddress = new Uri(config["IRestCountriesApi:WebsiteURL"]));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
