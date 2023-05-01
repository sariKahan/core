using TasksProject.Services;

namespace TasksProject.Middleware;
public class DetailsMiddleware{
    private readonly ILogService logger;
    private readonly RequestDelegate next;
     public DetailsMiddleware (RequestDelegate next, ILogService logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext ctx)
    {
        
        await next(ctx);
        logger.Log(LogLevel.Debug,$"path: {ctx.Request.Path}");
    }
}
//ctx.Request.Headers.Authorization