using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace TaskCli
{
    internal class TaskService
    {
        private readonly string filePath = "tasks.json";
        private List<TaskItem> Load ()
        {
            if (!File.Exists(filePath))
            {
                File.WriteAllText(filePath, "[]");
                return new List<TaskItem>();
            }
            var json = File.ReadAllText(filePath);
            try
            {
                return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
            }
            catch (JsonException)
            {
                // If the JSON is malformed, reset the file
                File.WriteAllText(filePath, "[]");
                return new List<TaskItem>();
            }
           

        }
        private void Save (List<TaskItem> tasks)
        {
            var json = JsonSerializer.Serialize(tasks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
        public void AddTask(string description)
        {
            var tasks = Load();
            var newTask = new TaskItem
            {
                Id = tasks.Any() ? tasks.Max(t => t.Id) + 1 : 1,
                Description = description,
                Status = "todo",
                CreatedAt = DateTime.Now,
                LastUpdatedAt = DateTime.Now
            };
            tasks.Add(newTask);
            Save(tasks);
        }
        public void UpdateTask(int id, string status)
        {
            var tasks = Load();
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                task.Status = status;
                task.LastUpdatedAt = DateTime.Now;
                Save(tasks);
                Console.WriteLine("Task updated successfully");
            }
        }
        public void DeleteTask(int id)
        {
            var tasks = Load();
            var task = tasks.FirstOrDefault(t => t.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
                Save(tasks);
                Console.WriteLine("Task deleted successfully");
            }
        }
        public void MarkTaskAsDone(int id)
        {
            UpdateTask(id, "done");
        }
        public void MarkTaskAsInProgress(int id)
        {
            UpdateTask(id, "in-progress");
        }
        public void List (string? status = null)
        {
            var tasks = Load();
            IEnumerable<TaskItem> output = tasks;
            if (!string.IsNullOrEmpty(status))
            {
                output = tasks.Where(t => t.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

            }
            if (!output.Any()) { Console.WriteLine("No tasks found."); return; }
            foreach (var task in output)
            {
                Console.WriteLine($"ID: {task.Id}");
                Console.WriteLine($"Description: {task.Description}");
                Console.WriteLine($"Status: {task.Status}");
                Console.WriteLine($"Created At: {task.CreatedAt}");
                Console.WriteLine($"Last Updated At: {task.LastUpdatedAt}");
                Console.WriteLine(new string('-', 20));
            }
               
        }
        
    }
}
