using BaeAiSystem;
using BaeIntegrations;
using BaeOpenAiIntegration;

var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Add services to the container.
/// </summary>
static void AddServicesToContainer(IServiceCollection services, ConfigurationManager configuration)
{
    // Add AI system integration manager
    var aiSystemIntegrationManager = new AiServiceIntegrationManager();
    // Register OpenAI integration
    aiSystemIntegrationManager.RegisterIntegration(new OpenAiIntegration(configuration["OpenAiKey"]
        ?? throw new Exception("OpenAI API key not found.")));
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
