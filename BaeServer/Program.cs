var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Add services to the container.
/// </summary>
static void AddServicesToContainer(IServiceCollection services)
{

}

// Add services to the container.
AddServicesToContainer(builder.Services);

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
