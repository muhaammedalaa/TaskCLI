namespace TaskCli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var CommandLine = args[0];
            var TaskService = new TaskService();
            switch (CommandLine)
            {
                case "add":
                    TaskService.AddTask(args[1]);
                    break;
                case "update":
                    TaskService.UpdateTask(int.Parse(args[1]), args[2]);
                    break;
                case "delete":
                    TaskService.DeleteTask(int.Parse(args[1]));
                    break;
                case "done":
                    TaskService.MarkTaskAsDone(int.Parse(args[1]));
                    break;
                case "inprogress":
                    TaskService.MarkTaskAsInProgress(int.Parse(args[1]));
                    break;
                case "list":
                    if (args.Length > 1)
                        TaskService.List(args[1]);
                    else
                        TaskService.List();
                    break;
                default:
                    Console.WriteLine("Unknown command");
                    break;
            }
        }
    }
}
