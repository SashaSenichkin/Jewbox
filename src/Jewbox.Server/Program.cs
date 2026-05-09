using Jewbox.Models;
using Jewbox.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
builder.Services.AddScoped<SenderService>();

var corsOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

if (corsOrigins is { Length: > 0 })
{
    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
            policy.WithOrigins(corsOrigins)
                .AllowAnyHeader()
                .AllowAnyMethod());
    });
}

var app = builder.Build();

if (corsOrigins is { Length: > 0 })
{
    app.UseCors();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapPost("/api/booking/send", async (Booking booking, SenderService sender, CancellationToken ct) =>
{
    var status = await sender.SendRequestAsync(booking);
    return Results.Json(status);
});

app.MapFallbackToFile("index.html");

app.Run();
