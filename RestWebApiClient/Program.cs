using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System.Text.Json;
using RestWebApiClient.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Register HttpClient with Polly policies for retry and circuit breaker
builder.Services.AddHttpClient("MyApiClient", client =>
{
    client.BaseAddress = new Uri("https://api.example.com/");
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    client.Timeout = TimeSpan.FromSeconds(30);  // Set appropriate timeout
})
   .AddTransientHttpErrorPolicy(policy =>
       policy.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(300))) // Retry on failure
    .AddTransientHttpErrorPolicy(policy =>
        policy.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));  // Break circuit on failures

// Register API Service
builder.Services.AddScoped<MyApiService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
