using BaeAiSystem;
using BaeAiSystem.Config;
using BaeDB;
using BaeIntegrations;
using BaeOpenAiIntegration;
using BaeOpenAiIntegration.Service;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BaeAuthentication;
using Microsoft.IdentityModel.Logging;

#if DEBUG
// Show PII in debug mode (DO NOT ENABLE IN PRODUCTION)
IdentityModelEventSource.ShowPII = true;
#endif

var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Add OpenAI integration to the integration manager.
/// </summary>
/// <param name="integrationManager">The integration manager.</param>
/// <param name="services">The services.</param>
/// <param name="configuration">The configuration.</param>
static void AddOpenAiIntegration(AiServiceIntegrationManager integrationManager, IServiceCollection services, IConfiguration configuration)
{
    // Register OpenAI integration
    // TODO: Fetch the api key from the database once implemented
#if DEBUG
    integrationManager.RegisterIntegration(new OpenAiIntegration(configuration["OpenAiKey"]
        ?? null));
#else
    integrationManager.RegisterIntegration(new OpenAiIntegration(null));
#endif
    services.AddSingleton<IOpenAiService, OpenAiService>();
}

/// <summary>
/// Add services to the container.
/// </summary>
static void AddServicesToContainer(BaeDbContext dbContext, IServiceCollection services, ConfigurationManager configuration)
{
    // Add AI system integration manager
    var aiSystemIntegrationManager = new AiServiceIntegrationManager(dbContext);

    // Add OpenAI integration
    AddOpenAiIntegration(aiSystemIntegrationManager, services, configuration);

    // Add AI system integration manager to services
    services.AddSingleton(aiSystemIntegrationManager);

    // Add AI system service
    services.AddSingleton<IAiSystemService, AiSystemService>();
}

/// <summary>
/// Add the database context 
/// </summary>
/// <param name="services">The services.</param>
/// <param name="configuration">The configuration.</param>
static BaeDbContext AddDatabaseContext(IServiceCollection services, ConfigurationManager configuration)
{
    var databaseSettings = configuration.GetSection("AIScannerDatabase")
        .Get<AIScannerDatabaseSettings>() ?? throw new Exception("Database settings not found.");
    var client = new MongoClient(databaseSettings.ConnectionString);
    var database = client.GetDatabase(databaseSettings.DatabaseName);
    var context = BaeDbContext.Create(database);
    services.AddSingleton(context);
    return context;
}

/// <summary>
/// Setup authentication.
/// </summary>
/// <param name="services">The services.</param>
static void SetupAuthentication(WebApplicationBuilder builder)
{
    var configSection = builder.Configuration.GetSection("BaeAuthentication") ?? throw new Exception("Must provide authentication config");
    var config = configSection.Get<BaeAuthenticationConfig>() ?? throw new Exception("Must provide authentication config");
    builder.Services.AddSingleton(config);

    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddEntityFrameworkStores<BaeDbContext>()
        .AddUserManager<UserManager<IdentityUser>>()
        .AddDefaultTokenProviders();

    builder.Services.AddSingleton<IBaeAuthenticationService, BaeAuthenticationService>();

    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
        options.SlidingExpiration = true;

        options.LoginPath = "/api/identity/account/login";
        options.LogoutPath = "/api/identity/account/logout";
    });

    // Configure authentication
    builder.Services.AddAuthentication(
        options =>
        {
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                // Should be validated in deployement
#if DEBUG
                ValidateIssuer = false,
                ValidateAudience = false,
#else
                ValidateIssuer = true,
                ValidateAudience = true,
#endif
                TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.EncryptionKey)),
                RequireSignedTokens = false, // False because we are handling encryption ourselves
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });
}

// Add database context
var dbContext = AddDatabaseContext(builder.Services, builder.Configuration);
// Add services to the container.
AddServicesToContainer(dbContext, builder.Services, builder.Configuration);
// Setup authentication
SetupAuthentication(builder);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Database config
builder.Services.Configure<AIScannerDatabaseSettings>(
    builder.Configuration.GetSection("AIScannerDatabase"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Setup authentication
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
