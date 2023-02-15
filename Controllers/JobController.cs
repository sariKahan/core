using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using שעור1_בניית_שרת_בסיסי.Models;
using שעור1_בניית_שרת_בסיסי.Services;

namespace שעור1_בניית_שרת_בסיסי.Controllers;

[ApiController]
[Route("[controller]")]
public class JobController : ControllerBase
{

    [HttpGet]
    public IEnumerable<Job> Get()
    {
       return JobService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Job> Get(int id)
    {
        var Job = JobService.Get(id);
        if (Job == null)
            return NotFound();
        return Job;
    }

    [HttpPost]
    public ActionResult Post(Job Job)
    {
        JobService.Add(Job);
        return CreatedAtAction(nameof(Post), new { id = Job.Id }, Job);
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id, Job Job)
    {
        if (! JobService.Update(id, Job))
            return BadRequest();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete (int id)
    {
        if (! JobService.Delete(id))
            return NotFound();
        return NoContent();            
    }

}
