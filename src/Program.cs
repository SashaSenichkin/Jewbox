using JavaScriptEngineSwitcher.ChakraCore;
using JavaScriptEngineSwitcher.Extensions.MsDependencyInjection;
using Jewbox.Repositories;
using Jewbox.Services;
using React.AspNet;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<ISenderService, SenderService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddJsEngineSwitcher(options => options.DefaultEngineName = ChakraCoreJsEngine.EngineName)
    .AddChakraCore();
builder.Services.AddReact();
builder.Services.AddMvc();

builder.Services.AddSwaggerGen();
//builder.Host.UseSerilog();

var app = builder.Build();

//app.UseSerilogRequestLogging();   
app.UseSwagger();  
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();

app.UseReact(config =>
{
    config
        .AddScript("~/js/remarkable.min.js")
        .AddScript("~/js/tutorial.jsx");
});

app.UseStaticFiles();
app.UseRouting();

app.Run();