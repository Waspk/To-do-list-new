
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TodoListApplication
{
    class Task
    {
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsDone { get; set; }
        public string Project { get; set; }
    }

    class Program
    {
        static List<Task> tasks = new List<Task>();
        static string dataFilePath = "tasks.txt";

        static void Main(string[] args)
        {
            LoadTasks();

            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("TODO LIST APPLICATION");
                Console.WriteLine("1. View Tasks");
                Console.WriteLine("2. Add Task");
                Console.WriteLine("3. Edit Task");
                Console.WriteLine("4. Mark Task as Done");
                Console.WriteLine("5. Remove Task");
                Console.WriteLine("6. Save and Quit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ViewTasks();
                        break;
                    case "2":
                        AddTask();
                        break;
                    case "3":
                        EditTask();
                        break;
                    case "4":
                        MarkTaskAsDone();
                        break;
                    case "5":
                        RemoveTask();
                        break;
                    case "6":
                        SaveTasks();
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        static void LoadTasks()
        {
            if (File.Exists(dataFilePath))
            {
                string[] lines = File.ReadAllLines(dataFilePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    Task task = new Task
                    {
                        Title = parts[0],
                        DueDate = DateTime.Parse(parts[1]),
                        IsDone = bool.Parse(parts[2]),
                        Project = parts[3]
                    };
                    tasks.Add(task);
                }
            }
        }

        static void SaveTasks()
        {
            List<string> lines = new List<string>();
            foreach (Task task in tasks)
            {
                string line = $"{task.Title},{task.DueDate},{task.IsDone},{task.Project}";
                lines.Add(line);
            }
            File.WriteAllLines(dataFilePath, lines);
            Console.WriteLine("Tasks saved successfully.");
        }

        static void ViewTasks()
        {
            if (tasks.Count == 0)
            {
                Console.WriteLine("No tasks found.");
            }
            else
            {
                Console.WriteLine("TASKS:");
                tasks = tasks.OrderBy(task => task.DueDate).ToList();
                foreach (Task task in tasks)
                {
                    Console.WriteLine($"Title: {task.Title} | Due Date: {task.DueDate} | Status: {(task.IsDone ? "Done" : "Pending")} | Project: {task.Project}");
                }
            }
        }

        static void AddTask()
        {
            Console.Write("Enter task title: ");
            string title = Console.ReadLine();

            Console.Write("Enter due date (YYYY-MM-DD): ");
            DateTime dueDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter project: ");
            string project = Console.ReadLine();

            Task newTask = new Task
            {
                Title = title,
                DueDate = dueDate,
                IsDone = false,
                Project = project
            };

            tasks.Add(newTask);
            Console.WriteLine("Task added successfully.");
        }

        static void EditTask()
        {
            ViewTasks();
            Console.Write("Enter the title of the task to edit: ");
            string titleToEdit = Console.ReadLine();

            Task taskToEdit = tasks.Find(task => task.Title == titleToEdit);
            if (taskToEdit != null)
            {
                Console.Write("Enter new title: ");
                taskToEdit.Title = Console.ReadLine();

                Console.Write("Enter new due date (YYYY-MM-DD): ");
                taskToEdit.DueDate = DateTime.Parse(Console.ReadLine());

                Console.Write("Enter new project: ");
                taskToEdit.Project = Console.ReadLine();

                Console.WriteLine("Task edited successfully.");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
        }

        static void MarkTaskAsDone()
        {
            ViewTasks();
            Console.Write("Enter the title of the task to mark as done: ");
            string titleToMark = Console.ReadLine();

            Task taskToMark = tasks.Find(task => task.Title == titleToMark);
            if (taskToMark != null)
            {
                taskToMark.IsDone = true;
                Console.WriteLine("Task marked as done.");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
        }

        static void RemoveTask()
        {
            ViewTasks();
            Console.Write("Enter the title of the task to remove: ");
            string titleToRemove = Console.ReadLine();

            Task taskToRemove = tasks.Find(task => task.Title == titleToRemove);
            if (taskToRemove != null)
            {
                tasks.Remove(taskToRemove);
                Console.WriteLine("Task removed successfully.");
            }
            else
            {
                Console.WriteLine("Task not found.");
            }
        }
    }
}
