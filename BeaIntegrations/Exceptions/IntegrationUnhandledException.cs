namespace BaeIntegrations.Exceptions;

/// <summary>
/// Gets returned when no ai key is provided
/// </summary>
public class IntegrationUnhandledException : Exception
{
    public IntegrationUnhandledException(string integration) : base($"{integration} integration will not be handled")
    {
    }
}
