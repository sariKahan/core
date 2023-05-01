using TasksProject.Models;
using TasksProject.Interfaces;
using System.Text.Json;

namespace TasksProject.Services
{
    public class UserService : TaskInterface<User>
    {
        List<User>? ListUsers { get; }
        private IWebHostEnvironment  webHost;
        private string filePath;
        public UserService(IWebHostEnvironment webHost)
        {
            this.webHost = webHost;
            this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "user.json");
            //this.filePath = webHost.ContentRootPath+@"/Data/Pizza.json";
            using (var jsonFile = File.OpenText(filePath))
            {
                ListUsers = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        }


        private void saveToFile()
        {
            File.WriteAllText(filePath, JsonSerializer.Serialize(ListUsers));
        }
        public List<User> GetAll() => ListUsers;
        public User Get(int id)
        {
            return ListUsers.FirstOrDefault(t => t.Id == id);
        }

        public void Add(User user)
        {
            user.Id = ListUsers.Max(p => p.Id) + 1;
            ListUsers.Add(user);
            saveToFile();
        }

        public bool Update(int id, User newUser)
        {
            if (newUser.Id != id)
                return false;
            
            User? user = ListUsers?.FirstOrDefault(t => t.Id == id);
            if(user == null)
                return false;
            user.Name = newUser.Name;
            user.Password = newUser.Password;
            saveToFile();
            return true;
        }

        public bool Delete(int id)
        {
            User? user = ListUsers?.FirstOrDefault(t => t.Id == id);
            if (user == null)
                return false;
            ListUsers?.Remove(user);
            saveToFile();
            return true;
        }
              public static int isExist(string name, string password)
        {
            // ListJobs.Add(new Job(){Id=0,Name="oiuh"});
            string filePath = Path.Combine("Data", "user.json");
            List<User>? ListUsers;
            using (var jsonFile = System.IO.File.OpenText(filePath))
            {
               ListUsers = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
        //ListUsers.Add(new User(){Id=09876543});
          User user = ListUsers.FirstOrDefault(u=> u.Name==name && u.Password == password);
            if(user != null)
                return user.Id;
            return -1;
            
        }
    }
}

