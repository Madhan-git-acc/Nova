using CLOTHAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// PostgreSQL
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// JWT Authentication
var jwt = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwt["SecretKey"]!))
        };
    });

builder.Services.AddAuthorization();
//builder.Services.AddCors(opt => opt.AddPolicy("AllowAngular", policy =>
//    policy.WithOrigins("http://localhost:4200", "https://nova-angular-cqnbmp0r0-madhan-git-accs-projects.vercel.app")
//          .AllowAnyHeader()
//          .AllowAnyMethod()));
builder.Services.AddCors(opt => opt.AddPolicy("AllowAngular", policy =>
    policy.SetIsOriginAllowed(origin =>
        origin.Contains("vercel.app") ||
        origin.Contains("localhost"))
    .AllowAnyHeader()
    .AllowAnyMethod()
));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();  // <- Default, no extra using needed

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("AllowAngular");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Use(async (context, next) =>
{
    var path = context.Request.Path;
    if (path == "/" || path == "/index.html")
    {
        context.Response.Redirect("/swagger");
        return;
    }
    await next();
});

app.Run();