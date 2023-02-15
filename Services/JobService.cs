using שעור1_בניית_שרת_בסיסי.Models;
using System.Collections.Generic;
using System.Linq;

namespace שעור1_בניית_שרת_בסיסי.Services
{
    public static class JobService
    {
        private static List<Job> ListJobs = new List<Job>
        {
            new Job{Id = 1,Name = "hwCore",IsDone = false},
            new Job{Id = 2,Name = "testC#",IsDone = false}
        };

        public static List<Job> GetAll() => ListJobs;
        public static Job Get(int id)
        {
            return ListJobs.FirstOrDefault(t => t.Id == id);
        }

        public static void Add(Job Job)
        {
            Job.Id = ListJobs.Max(p => p.Id) + 1;
            ListJobs.Add(Job);
        }

        public static bool Update(int id, Job newJob)
        {
            if (newJob.Id != id)
                return false;
            
            Job Job = ListJobs.FirstOrDefault(t => t.Id == id);
            Job.Name = newJob.Name;
            Job.IsDone = newJob.IsDone;
            return true;
        }

        public static bool Delete(int id)
        {
            Job Job = ListJobs.FirstOrDefault(t => t.Id == id);
            if (Job == null)
                return false;
            ListJobs.Remove(Job);
            return true;
        }
    }
}

