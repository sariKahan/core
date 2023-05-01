using TasksProject.Interfaces;
using TasksProject.Services;
using TasksProject.Models;
// using TasksProject.Middleware;


namespace TasksProject.Utilities
{
    public static class Helper
    {
        public static void buildObj(this IServiceCollection services)
        {
                services.AddSingleton<TaskInterface<task>, TaskService>();
                services.AddSingleton<TaskInterface<User>, UserService>();
                services.AddSingleton<ILogService,LogService>();
        }
    }
}