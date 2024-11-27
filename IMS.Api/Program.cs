using IMS.Infrastructure.DBContext;
using IMS.Infrastructure.IdentityModels;
using IMS.Infrastructure.RegisterServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



//DB Connection EntityFramework
// Load the appsettings.api.json configuration file
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();
var IsDevelopment = builder.Configuration.GetValue<bool>("IsDevelopment");

var dbCon = IsDevelopment ? "DevConnection" : "DevConnectionProduction";
builder.Services.AddDbContext<IMSContextEF>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(dbCon)));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString(dbCon), sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5, // Adjust this as needed
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null);
    }));



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "IMS",
        Version = "V1.0"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\""
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[] { }
        }
    });
});
builder.Services.AddIdentity<ApplicationDbUser, IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(opt =>
{
    // Password settings 
    //opt.Password.RequireDigit = true;
    //opt.Password.RequireLowercase = true;
    //opt.Password.RequireUppercase = true;
    //opt.Password.RequireNonAlphanumeric = true;
    opt.Password.RequiredLength = 8;

    // Lockout settings 
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
    opt.Lockout.MaxFailedAccessAttempts = 5;

    //Signin option
    opt.SignIn.RequireConfirmedEmail = false;

    // User settings 
    opt.User.RequireUniqueEmail = true;

    //Token Option
    opt.Tokens.AuthenticatorTokenProvider = "Name of AuthenticatorTokenProvider";
});
builder.Services.AddInfrastructure();
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;  // Add this line
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(opts =>
{
    opts.Cookie.IsEssential = true; // make the session cookie Essential
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = async context =>
        {
            // Skip the default logic
            context.HandleResponse();

            // Set the custom response status code
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

            // Optionally, set custom headers
            context.Response.Headers.Append("CTF-Sec", "Unauthorized");

            // Write a custom message
            var responseMessage = new
            {
                statusCode = StatusCodes.Status401Unauthorized,
                success = false,
                message = "You are not authorized! Please log in to access this resource."
            };
            var jsonResponse = JsonSerializer.Serialize(responseMessage);

            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(jsonResponse);
        }
    };


});
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("http://localhost:4200", "http://103.119.101.170:4003", "http://192.168.20.6:4003").AllowAnyMethod().AllowAnyHeader()
            .AllowCredentials();
}));
var app = builder.Build();
// Middleware to log the origin of requests
app.Use(async (context, next) =>
{
    // Extract the Origin header (if available)
    var origin = context.Request.Headers["Origin"].ToString() ?? "Unknown Origin";



    // Call the next middleware
    await next.Invoke();
});
// Configure the HTTP request pipeline.
if (IsDevelopment)
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "IMS API v1.0");
        if (app.Environment.IsProduction())
        {
            c.RoutePrefix = string.Empty;
        }
    });
}
app.UseCors("corsapp");
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();
app.UseAuthentication();
app.UseSession();  // Make sure this is after UseAuthorization

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
