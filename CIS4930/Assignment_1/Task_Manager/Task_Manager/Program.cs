using System;

namespace Task_Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            bool endApp = false;
            int menuSelection;

            while (!endApp)
            {
                drawConsole();
                while (!int.TryParse(Console.ReadLine(), out menuSelection) || menuSelection < 0 || menuSelection > 6)
                {
                    Console.WriteLine("Invalid menu selection.");
                    Console.Write("> ");
                }

                switch (menuSelection)
                {
                    case 1:
                        drawConsole(1);
                        createTask();
                        break;
                    case 2:
                        drawConsole(2);
                        break;
                    case 3:
                        drawConsole(3);
                        break;
                    case 4:
                        drawConsole(4);
                        break;
                    case 5:
                        drawConsole(5);
                        break;
                    case 6:
                        drawConsole(6);
                        break;
                    case 0:
                        endApp = true;
                        break;
                }

                pauseUntilKeystroke();
            }

            // Helper functions

            Task createTask()
            {
                DateTime deadline;

                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Description: ");
                string description = Console.ReadLine();
                Console.Write("Deadline Date (mm/hh/yyyy min:hr am/pm): ");

                // Notify user if deadline is entered with wrong format
                while (!DateTime.TryParse(Console.ReadLine(), out deadline))
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write("Deadline Date (mm/hh/yyyy min:hr am/pm): !Incorrect format!");
                    Console.SetCursorPosition(Console.CursorLeft - 18, Console.CursorTop);
                }

                return new Task(name, description, deadline);
            }


            void drawConsole(int selection = 0)
            {
                Console.Clear();
                Console.WriteLine("Task Manager 1.0\n");

                switch (selection)
                {
                    case 1:
                        Console.WriteLine("* Create a new task *\n");
                        break;
                    case 2:
                        Console.WriteLine("* Create a delete task *\n");
                        break;
                    case 3:
                        Console.WriteLine("* Select a task to edit *\n");
                        break;
                    case 4:
                        Console.WriteLine("* Select a task to complete *\n");
                        break;
                    case 5:
                        Console.WriteLine("** Tncomplete tasks **\n");
                        break;
                    case 6:
                        Console.WriteLine("** Tasks **\n");
                        break;

                    default:
                        printMenu();
                        break;
                }

                Console.Write("\n> ");

            }

            void printMenu()
            {
                Console.WriteLine("** Commands **");
                Console.WriteLine("1) Create a new task");
                Console.WriteLine("2) Delete a task");
                Console.WriteLine("3) Edit a task");
                Console.WriteLine("4) Complete a task");
                Console.WriteLine("5) List incomplete tasks");
                Console.WriteLine("6) List all tasks");
                Console.WriteLine("0) Quit");
            }


            // Development functions

            void pauseUntilKeystroke()
            {
                var tempKey = Console.ReadLine();
            }
        }

        class Task
        {
            public string name;
            public string description;
            public DateTime deadline;
            public bool isCompleted;

            public Task(string name, string description, DateTime deadline, bool isCompleted = false)
            {
                this.name = name;
                this.description = description;
                this.deadline = deadline;
                this.isCompleted = isCompleted;
            }
        }
    }
}
