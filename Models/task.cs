
namespace TasksProject.Models;

public class task
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public String? Name { get; set; }

    public bool IsDone { get; set; }
}
