using Microsoft.AspNetCore.Hosting;

namespace TasksProject.Services;

public class LogService :ILogService
{
    private readonly string filePath;
    public LogService(IWebHostEnvironment web)
    {
        filePath = Path.Combine(web.ContentRootPath, "Logs", "DetailsLog.log")      ;
    }
    public void Log(LogLevel level, string message)
    {
        using (var sr = new StreamWriter(filePath, true))
        {
            sr.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} [{level}] {message}");
        }

    }

}

public interface ILogService
{
    void Log(LogLevel level, string message);
}