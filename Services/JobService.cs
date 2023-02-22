using JobProject.Models;
using System.Collections.Generic;
using System.Linq;
using JobProject.Interfaces;

namespace JobProject.Services
{
    public class JobService : JobInterface
    {
        private List<Job> ListJobs = new List<Job>
        {
            new Job{Id = 1,Name = "hwCore",IsDone = false},
            new Job{Id = 2,Name = "testC#",IsDone = false}
        };

        public List<Job> GetAll() => ListJobs;
        public Job Get(int id)
        {
            return ListJobs.FirstOrDefault(t => t.Id == id);
        }

        public void Add(Job Job)
        {
            Job.Id = ListJobs.Max(p => p.Id) + 1;
            ListJobs.Add(Job);
        }

        public bool Update(int id, Job newJob)
        {
            if (newJob.Id != id)
                return false;
            
            Job Job = ListJobs.FirstOrDefault(t => t.Id == id);
            Job.Name = newJob.Name;
            Job.IsDone = newJob.IsDone;
            return true;
        }

        public bool Delete(int id)
        {
            Job Job = ListJobs.FirstOrDefault(t => t.Id == id);
            if (Job == null)
                return false;
            ListJobs.Remove(Job);
            return true;
        }
    }
}

