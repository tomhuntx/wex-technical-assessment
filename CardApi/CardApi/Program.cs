using CardApi.Data;
using CardApi.Models.Config;
using CardApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<CardService>();
builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<ExchangeRateService>();
builder.Services.AddHttpClient<ExchangeRateService>();

// Define SQLite database
builder.Services.AddDbContext<CardDbContext>(options =>
    options.UseSqlite("Data Source=cards.db"));

// Extract config values
builder.Services.Configure<ExternalApis>(
    builder.Configuration.GetSection("ExternalApis"));

var app = builder.Build();

// Create SQLite database and tables on app start
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CardDbContext>();
    db.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CardApi.Middleware.GlobalExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();