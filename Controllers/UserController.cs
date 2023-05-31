using Microsoft.AspNetCore.Mvc;
using TasksProject.Models;
using TasksProject.Interfaces;
using System.Security.Claims;
using TasksProject.Services;
using Microsoft.AspNetCore.Authorization;

namespace TasksProject.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private TaskInterface<User> user;
    public UserController(TaskInterface<User> user)
    {
        this.user = user;
    }
    [HttpPost]
    [Route("[action]")]
public ActionResult<String> Login([FromBody] User user)
    {
        bool flag = false;
        var claims = new List<Claim> { };
        var dt = DateTime.Now;
        if (user.Name == "Dina"
        && user.Password == $"W{dt.Year}#{dt.Day}!")
        {
            claims.Add(new Claim("type", "Admin"));
            flag = true;
        }
        else
        {
            int userId = UserService.isExist(user.Name, user.Password);
            if (userId == -1)
            {
                return NotFound();
            }
        claims.Add(new Claim("type", "User"));
        claims.Add(new Claim("id",userId.ToString()));
        }

        var token = TokenService.GetToken(claims);
        return new OkObjectResult(new {token = TokenService.WriteToken(token),isAdmin = flag}) ;
    }
    [HttpGet]
    [Authorize(Policy = "Admin")]
    public IEnumerable<User> Get()
    {
        return user.GetAll();
    }

    [HttpGet("{id}")]
    [Authorize(Policy = "Admin")]
    public ActionResult<User> Get(int id)
    {
        var User = user.Get(id);
        if (User == null)
            return NotFound();
        return User;
    }

    [HttpPost]
    [Authorize(Policy = "Admin")]
    public ActionResult Post(User user)
    {
        this.user.Add(user);
        return CreatedAtAction(nameof(Post), new { id = user.Id }, user);
    }


    [HttpPut("{id}")]
    [Authorize(Policy = "Admin")]
    public ActionResult Put(int id, User user)
    {
        if (!this.user.Update(id, user))
            return BadRequest();
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = "Admin")]
    public ActionResult Delete(int id)
    {
        if (!user.Delete(id))
            return NotFound();
        return NoContent();
    }

}



