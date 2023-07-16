namespace Api.Helpers;

public class GenericLoggerHelper
{
    private readonly ILogger<GenericLoggerHelper> _logger;
    public GenericLoggerHelper(ILogger<GenericLoggerHelper> logger)
    {
        _logger = logger;
        _logger.LogInformation(1, "Generic Helper has been constructed");
    }
    public void JustADumbFunctionCall()
    {
        _logger.LogInformation("JustADumbFunctionCall has been called");
    }
}