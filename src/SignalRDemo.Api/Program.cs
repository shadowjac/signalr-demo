using Microsoft.AspNetCore.Authentication.JwtBearer;
using SignalRDemo.Api;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

builder.Services.AddHostedService<ServerTimeNotifier>();

builder.Services.AddCors();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.UseHttpsRedirection();

app.MapHub<NotificationHub>("/notifications");

app.Run();