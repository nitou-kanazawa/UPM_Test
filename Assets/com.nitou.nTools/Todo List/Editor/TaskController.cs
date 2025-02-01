using System.Collections.Generic;

namespace Nitou.Tools.TodoTask{

    public class TaskController {
        
        private ITaskRepository repository;
        public List<TodoTask> Tasks { get; private set; }

        public TaskController(ITaskRepository repo) {
            repository = repo;
            Tasks = repository.LoadTasks();
        }

        public void AddTask(string name) {
            if (!string.IsNullOrEmpty(name)) {
                Tasks.Add(new TodoTask { name = name });
            }
        }

        public void RemoveTask(int index) {
            if (index >= 0 && index < Tasks.Count) {
                Tasks.RemoveAt(index);
            }
        }

        public void SaveTasks() {
            repository.SaveTasks(Tasks);
        }
    }

}
