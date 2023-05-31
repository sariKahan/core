using TasksProject.Models;
using TasksProject.Interfaces;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;

namespace TasksProject.Services
{
    public class TaskService : TaskInterface<task>
    {
        List<task>? ListTasks { get; }
        
        private IWebHostEnvironment  webHost;
        private string filePath;
        public TaskService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "task.json");
            using (var jsonFile = File.OpenText(filePath))
            {
                ListTasks = JsonSerializer.Deserialize<List<task>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }
        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(ListTasks));
        }
        public List<task> GetAll() => ListTasks!;
        public task Get(int id)
        {
            return ListTasks!.FirstOrDefault(t => t.Id == id)!;
        }

        public void Add(task task)
        {
            task.Id = ListTasks!.Max(p => p.Id) + 1;
            ListTasks!.Add(task);
            saveToFile();
        }

        public bool Update(int id, task newTask)
        {
           
            task Task = Get(id);
            if(Task == null ){
                return false;
            }
            Task.Name = newTask.Name;
            Task.IsDone = newTask.IsDone;
            saveToFile();
            return true;
        }

        public bool Delete(int id)
        {
            task Task = ListTasks!.FirstOrDefault(t => t.Id == id)!;
            if (Task == null)
                return false;
            ListTasks?.Remove(Task);
            saveToFile();
            return true;
        }
  
    public static int getId(string token){
        token = token.Split(" ")[1];
        var handler = new JwtSecurityTokenHandler();
        var decodedValue = handler.ReadJwtToken(token) as JwtSecurityToken;
        var id = decodedValue.Claims.First(claim => claim.Type == "id").Value;
        return Convert.ToInt32(id);
    }

    }
}

