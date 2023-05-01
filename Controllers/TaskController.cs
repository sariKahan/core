
using Microsoft.AspNetCore.Mvc;
using TasksProject.Models;
using TasksProject.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TasksProject.Services;

namespace TasksProject.Controllers;
[ApiController]
[Route("[controller]")]

public class TasksProject : ControllerBase
{

    private TaskInterface<task> task;

    public TasksProject(TaskInterface<task> task)
    {
        this.task = task;

    }
    [HttpGet]
    [Route("[action]")]
    [Authorize(Policy = "User")]
    public IEnumerable<task> Get()
    {
        return task.GetAll().Where(task => task.UserId == TaskService.getId(Request.Headers.Authorization));
    }

    [Authorize(Policy = "User")]
    [HttpGet("{id}")]
    public ActionResult<task> Get(int id)
    {
        task t = task.Get(id);
        if (t == null || t.UserId != TaskService.getId(Request.Headers.Authorization))
            return NotFound();
        return t;

    }
    
    
    [HttpPost]
    [Route("[action]")]
    [Authorize(Policy = "User")]
    public ActionResult Post(task task)
    {
        task.UserId = TaskService.getId(Request.Headers.Authorization);
        this.task.Add(task);
        return CreatedAtAction(nameof(Post), new { id = task.Id }, task);
    }
    [HttpPut("{id}")]
    [Authorize(Policy = "User")]
    public ActionResult Put(int id, task Task)
    {
        // return Job.Name;
        var t = task.Get(id);
        // return j;
        if (t == null || t.UserId != TaskService.getId(Request.Headers.Authorization))
        {
            return BadRequest();
        }

        task.Update(id, t);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "User")]
    public ActionResult Delete(int id)
    {
        if (task.GetAll().FirstOrDefault(t => t.Id == id).UserId != TaskService.getId(Request.Headers.Authorization))
            if (!task.Delete(id))
                return NotFound();
        return NoContent();
    }


}
