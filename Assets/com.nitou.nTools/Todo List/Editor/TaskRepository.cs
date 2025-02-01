#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Nitou.Tools.TodoTask {

    /// <summary>
    /// <see cref="TodoTask"/>のコンテナ
    /// </summary>
    public interface ITaskRepository {

        List<TodoTask> LoadTasks();
        void SaveTasks(List<TodoTask> tasks);
    }


    public class SharedTaskRepository : ITaskRepository {
        private const string SharedTasksKey = "SharedTasks";

        public List<TodoTask> LoadTasks() {
            if (!EditorPrefs.HasKey(SharedTasksKey))
                return new List<TodoTask>();

            string json = EditorPrefs.GetString(SharedTasksKey);
            return JsonUtility.FromJson<TodoTaskListWrapper>(json).Tasks;
        }

        public void SaveTasks(List<TodoTask> tasks) {
            var wrapper = new TodoTaskListWrapper { Tasks = tasks };
            string json = JsonUtility.ToJson(wrapper);
            EditorPrefs.SetString(SharedTasksKey, json);
        }
    }


    public class ProjectTaskRepository : ITaskRepository {
        private string projectKey = "ProjectTasks_" + Application.productName;

        public List<TodoTask> LoadTasks() {
            if (!EditorPrefs.HasKey(projectKey))
                return new List<TodoTask>();

            string json = EditorPrefs.GetString(projectKey);
            return JsonUtility.FromJson<TodoTaskListWrapper>(json).Tasks;
        }

        public void SaveTasks(List<TodoTask> tasks) {
            var wrapper = new TodoTaskListWrapper { Tasks = tasks };
            string json = JsonUtility.ToJson(wrapper);
            EditorPrefs.SetString(projectKey, json);
        }
    }
}
#endif