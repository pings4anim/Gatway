
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add Ocelot
builder.Services.AddOcelot();
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy =>
        {
            policy.WithOrigins("https://cicdnewui-g8fhccgyege2d2bp.canadacentral-01.azurewebsites.net")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline test.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/version", () => "Deployed version: 1.0.1 - CORS updated-Piyush-CI-Gatway-2");
app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();
await app.UseOcelot();
app.MapControllers();

app.Run();
