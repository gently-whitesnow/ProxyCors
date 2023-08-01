using System.Net;
using ProxyCors;

var builder = WebApplication.CreateBuilder();
builder.WebHost.UseKestrel(options =>
{
    options.Listen(IPAddress.Any, 322);
    options.AllowSynchronousIO = true;
});

builder.Services.AddSingleton<ProxyManager>();
builder.Services.AddSingleton<ProxyAdapter>();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
    options.AddPolicy("AllowAnyOriginsPolicy",
        builder =>
            builder.WithMethods("GET")
                .AllowAnyOrigin()));


var app = builder.Build();

app.UseCors("AllowAnyOriginsPolicy");
app.MapControllers();

app.Run();