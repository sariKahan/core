
namespace TasksProject.Interfaces
{
    public interface TaskInterface<T>
    {

        public List<T> GetAll();

        public T Get(int id);

        public void Add(T obj);

        public bool Update(int id, T newObj);

        public bool Delete(int id);
    }
}