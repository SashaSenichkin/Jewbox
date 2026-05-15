using Jewbox.Repositories;
using Jewbox.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<ISenderService, SenderService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSwaggerGen();
builder.Host.UseSerilog();

var app = builder.Build();

app.UseSerilogRequestLogging();   
app.UseSwagger();  
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();