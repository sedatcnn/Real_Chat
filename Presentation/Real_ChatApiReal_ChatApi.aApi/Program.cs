using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Real_ChatApi.Application.Features.MedaitR.Handlers.GroupHandler;
using Real_ChatApi.Application.Features.MedaitR.Handlers.MessageHandler;
using Real_ChatApi.Application.Features.MedaitR.Handlers.UserHandler;
using Real_ChatApi.Application.Features.MedaitR.Queries.MessageQueries;
using Real_ChatApi.Application.Interfaces;
using Real_ChatApi.Application.Tools;
using Real_ChatApi.Infrastructure.Context;
using Real_ChatApi.Persistence.Repositories;
using System;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.SignalR.StackExchangeRedis;
using StackExchange.Redis;
using Real_ChatApi.Infrastructure.Redis;
var builder = WebApplication.CreateBuilder(args);

// -------------------- SERVICES --------------------
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .SetIsOriginAllowed(_ => true)
              .AllowCredentials();
    });
});
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(1);
    options.Cookie.MaxAge = TimeSpan.FromHours(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.RequireHttpsMetadata = false;
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidAudience = JwtTokenDefaults.ValidAudience,
            ValidIssuer = JwtTokenDefaults.ValidIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtTokenDefaults.Key)),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            RoleClaimType = ClaimTypes.Role,
            NameClaimType = ClaimTypes.Name
        };
    });

// -------------------- DEPENDENCY INJECTION --------------------
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IWithRepository, WithRepository>();
builder.Services.AddScoped<TokenService>();

//builder.Services.AddDbContext<ChatDbContext>(options =>
//    options.UseNpgsql(builder.Configuration.GetConnectionString("DockerDefaultConnection")));

//// Development ortamýnda:
builder.Services.AddDbContext<ChatDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DockerDefaultConnection")));

builder.Services.AddMediatR(
    typeof(GetUsersHandler).Assembly,
    typeof(GetGroupsQueryHandler).Assembly,
    typeof(GetMessagesQueryHandler).Assembly
);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -------------------- SignalR --------------------
builder.Services.AddSignalR().AddStackExchangeRedis("redis:6379");
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
    ConnectionMultiplexer.Connect(ConfigurationOptions.Parse("redis:6379", true))
);
builder.Services.AddScoped<MessageCacheService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");


app.UseAuthentication();
app.UseAuthorization();
app.MapHub<ChatHub>("/chathub");
app.UseSession();

app.MapControllers();

app.Run();
