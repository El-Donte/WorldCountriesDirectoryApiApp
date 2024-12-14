using WorldCountriesDirectoryApiApp.Model;
using WorldCountriesDirectoryApiApp.Storage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<CountryScenarios>();
builder.Services.AddTransient<ICountryStorage, CountryStorage>();
builder.Services.AddDbContext<ApplicationDbContext>();

var app = builder.Build();

app.MapControllers();

app.Run();
