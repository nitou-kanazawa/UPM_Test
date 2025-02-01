using System.Collections.Generic;

namespace Nitou.Tools.TodoTask{

    [System.Serializable]
    public class TodoTask {
        public string name;
        public bool isDone;
    }

    [System.Serializable]
    public class TodoTaskListWrapper {
        public List<TodoTask> Tasks;
    }
}
