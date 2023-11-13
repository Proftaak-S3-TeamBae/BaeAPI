using BaeAiSystem;
using BaeIntegrations;
using BaeOpenAiIntegration;
using BaeOpenAiIntegration.Service;

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
static void AddServicesToContainer(IServiceCollection services, ConfigurationManager configuration)
{
    // Add AI system integration manager
    var aiSystemIntegrationManager = new AiServiceIntegrationManager();

    // Add OpenAI integration
    AddOpenAiIntegration(aiSystemIntegrationManager, services, configuration);

    // Add AI system integration manager to services
    services.AddSingleton(aiSystemIntegrationManager);

    // Add AI system service
    services.AddSingleton<IAiSystemService, AiSystemService>();
}

// Add services to the container.
AddServicesToContainer(builder.Services, builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
