using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JobProject.Models;
using JobProject.Services;
using JobProject.Interfaces;

namespace JobProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private JobInterface job;
        public JobController(JobInterface job)
        {
            this.job=job;
        }
        [HttpGet]
        public IEnumerable<Job> Get()
        {
        return job.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Job> Get(int id)
        {
            var Job = job.Get(id);
            if (Job == null)
                return NotFound();
            return Job;
        }

        [HttpPost]
        public ActionResult Post(Job Job)
        {
            job.Add(Job);
            return CreatedAtAction(nameof(Post), new { id = Job.Id }, Job);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, Job Job)
        {
            if (! job.Update(id, Job))
                return BadRequest();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete (int id)
        {
            if (! job.Delete(id))
                return NotFound();
            return NoContent();            
        }

    }
}


