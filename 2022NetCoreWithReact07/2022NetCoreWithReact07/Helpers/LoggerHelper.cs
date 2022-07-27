using System.Runtime.CompilerServices;
using System.Text;

namespace _2022NetCoreWithReact07.Helpers;

public class LoggerHelper<T>
{
    private readonly ILogger<T> _logger;
    private readonly string _className;

    public LoggerHelper(ILogger<T> logger, string className)
    {
        _logger = logger;
        _className = className;
    }

    public void LogStart(string additionalData = "", [CallerMemberName] string callerName = "")
    {
        LogMessage(callerName, "Started", additionalData);
    }
    
    public void LogFinish(string additionalData = "", [CallerMemberName] string callerName = "")
    {
        LogMessage(callerName, "Finished", additionalData);
    }

    private void LogMessage(string callerName, string message, string additionalData)
    {
        var logString = new StringBuilder($"{_className}.{callerName} {message}");

        if (!string.IsNullOrEmpty(additionalData))
            logString.Append(additionalData);

        _logger?.LogInformation($"\n{logString}\n");
    }
}