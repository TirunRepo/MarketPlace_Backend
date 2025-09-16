using FluentValidation;
using MarketPlace.Business.Interfaces;
using MarketPlace.Business.Interfaces.Inventory;
using MarketPlace.Business.Services;
using MarketPlace.Business.Services.Inventory;
using MarketPlace.Common.Mapping;
using MarketPlace.DataAccess.DBContext;
using MarketPlace.DataAccess.Repositories.Inventory.Interface;
using MarketPlace.DataAccess.Repositories.Inventory.Respository;
using MarketPlace.Infrastucture.JwtTokenGenerator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Config & DbContext
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        sql => sql.EnableRetryOnFailure()));

// Services
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<JwtTokenGenerator>();
builder.Services.AddScoped<IDeparturePortService, CruiseDeparturePortService>();
builder.Services.AddScoped<ICruiseDeparturePortRepository, CruiseDeparturePortRepository>();
builder.Services.AddScoped<IcruiseInventoryService, CruiseInventoryService>();
builder.Services.AddScoped<ICruiseInventoryRepository, CruiseInventoryRepository>();
builder.Services.AddScoped<ICruiseLineService, CruiseLineService>();
builder.Services.AddScoped<ICruiseShipService, CruiseShipServices>();
builder.Services.AddScoped<IcruiseShipRepository, CruiseShipRepository>();
builder.Services.AddScoped<ICruiseLineService, CruiseLineService>();
builder.Services.AddScoped<ICruiseLineRepository, CruiseLineRepository>();
builder.Services.AddScoped<ISailDateService, SailDateService>();
builder.Services.AddScoped<ISailDateRepository, SailDateRepository>();
builder.Services.AddScoped<ICruisePricingInventoryService, CruisePricingInventoryService>();
builder.Services.AddScoped<ICruisePricingInventoryRepository, CruisePricingInventoryRepository>();

builder.Services.AddScoped<ICruisePricingCabinService, CruisePricingCabinService>();
builder.Services.AddScoped<ICruisePricingCabinRepository, CruisePricingCabinRepository>();

builder.Services.AddScoped<IDestinationService, CruiseDestinationService>();
builder.Services.AddScoped<ICruiseDestinationRepository, CruiseDestinationRepository>();
// JWT settings
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? throw new InvalidOperationException("JWT Key is missing"))
        )
    };

    // ✅ Extract JWT from HTTP-only cookie
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            if (context.Request.Cookies.ContainsKey("accessToken"))
            {
                context.Token = context.Request.Cookies["accessToken"];
            }
            return Task.CompletedTask;
        }
    };
});

// Authorization
builder.Services.AddAuthorization();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins("https://localhost:3000") // frontend
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("v1", new OpenApiInfo { Title = "MarketPlace API", Version = "v1" });
    o.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Enter JWT token in format: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Cookie,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    o.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

// Middleware order is important
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ CORS must be before auth
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
