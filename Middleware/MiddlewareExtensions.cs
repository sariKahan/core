namespace TasksProject.Middleware;
public static class MiddlewareExtensions{
        public static IApplicationBuilder UseDetailsMiddleware(
        this IApplicationBuilder app
    )
    {
        return app.UseMiddleware<DetailsMiddleware>();
       
    }
}