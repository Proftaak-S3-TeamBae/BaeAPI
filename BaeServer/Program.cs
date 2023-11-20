using BaeAiSystem;
using BaeAiSystem.Config;
using BaeDB;
using BaeIntegrations;
using BaeOpenAiIntegration;
using BaeOpenAiIntegration.Service;
using MongoDB.Driver;

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
static BaeDbContext AddDatabaseContext(IServiceCollection service, ConfigurationManager configuration)
{
    var databaseSettings = configuration.GetSection("AIScannerDatabase")
        .Get<AIScannerDatabaseSettings>() ?? throw new Exception("Database settings not found.");
    var client = new MongoClient(databaseSettings.ConnectionString);
    var database = client.GetDatabase(databaseSettings.DatabaseName);
    var context = BaeDbContext.Create(database);
    service.AddSingleton(context);
    return context;
}

// Add database context
var dbContext = AddDatabaseContext(builder.Services, builder.Configuration);
// Add services to the container.
AddServicesToContainer(dbContext, builder.Services, builder.Configuration);

builder.Services.AddControllers();
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
app.UseAuthorization();
app.MapControllers();
app.Run();
