﻿using System;
using System.Globalization;
using System.Collections.Generic;

namespace Task_Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskList Tasks = new TaskList();
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
                        Task tempTask = createTask();
                        Tasks.addTask(tempTask);
                        pauseUntilKeystroke();
                        break;
                    case 2:
                        drawConsole(2);
                        deleteTask(ref Tasks);
                        pauseUntilKeystroke();
                        break;
                    case 3:
                        drawConsole(3);
                        break;
                    case 4:
                        drawConsole(4);
                        break;
                    case 5:
                        drawConsole(5);
                        printIncompleteTasks(ref Tasks);
                        pauseUntilKeystroke();
                        break;
                    case 6:
                        drawConsole(6);
                        printAllTasks(ref Tasks);
                        pauseUntilKeystroke();
                        break;
                    case 0:
                        endApp = true;
                        break;
                }
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
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write("Deadline Date (mm/hh/yyyy min:hr am/pm): !Wrong format!");
                    Console.SetCursorPosition(Console.CursorLeft - 14, Console.CursorTop);
                }

                return new Task(name, description, deadline);
            }

            void deleteTask(ref TaskList tasks)
            {
                printAllShortTasks(ref tasks);

                Console.Write("\nChoose task to delete > ");

                if (int.TryParse(Console.ReadLine(), out int taskNum)) 
                {
                    if (tasks.removeTask(taskNum))
                        Console.WriteLine("\n- Task Deleted -");
                    else
                        Console.WriteLine("\n- Unable to delete task -");
                }   
                else
                {
                    Console.WriteLine("\n- Unable to delete task -");
                }

            }

            void drawConsole(int selection = 0)
            {
                Console.Clear();
                Console.WriteLine("Task Manager 1.0\n");

                switch (selection)
                {
                    case 1:
                        Console.WriteLine("** Create a new task **\n");
                        break;
                    case 2:
                        Console.WriteLine("** Delete a task **");
                        break;
                    case 3:
                        Console.WriteLine("** Select a task to edit **");
                        break;
                    case 4:
                        Console.WriteLine("** Select a task to complete **");
                        break;
                    case 5:
                        Console.WriteLine("** Incomplete tasks **");
                        break;
                    case 6:
                        Console.WriteLine("** Tasks **");
                        break;

                    default:
                        printMenu();
                        Console.Write("\n> ");
                        break;
                }
            }

            void printAllTasks(ref TaskList tasks)
            {
                tasks.printTasks(showAll: true);
                /*
                Console.WriteLine("* Incomplete *");
                tasks.printTasks(0);
                    
                if (!tasks.isEmpty)
                {
                    Console.WriteLine("\n* Completed *");
                    tasks.printTasks(1);
                }
                */
            }

            void printAllShortTasks(ref TaskList tasks)
            {
                tasks.printTasks(shorten: true, lines: false);
            }

            void printIncompleteTasks(ref TaskList tasks)
            {
                tasks.printTasks(taskType: 0);
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
                Console.WriteLine("\nPress ENTER to return");
                var tempKey = Console.ReadLine();
            }
        }

        class Task
        {
            public string name;
            public string description;
            public DateTime deadline;
            public bool isCompleted;
            public double taskID; 

            public Task()
            {
                name = "Some task";
                description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.";
                deadline = new DateTime(2020, 12, 31, 23, 59, 59);
                taskID = Double.Parse(deadline.ToString("yyyyMMddHHmmss"));
            }
            public Task(string name, string description, DateTime deadline, bool isCompleted = false)
            {
                this.name = name;
                this.description = description;
                this.deadline = deadline;
                this.isCompleted = isCompleted;
                taskID = Double.Parse(deadline.ToString("yyyyMMddHHmmss"));
            }

            public void Print()
            {
                Console.WriteLine("Name: " + name);
                Console.WriteLine("Deadline: " + deadline + '\n');
                Console.WriteLine(description + '\n');
                Console.WriteLine("[" + (isCompleted ? 'x' : ' ') + "] Completed");
            }

            public void shortPrint()
            {
                Console.WriteLine(name + " - Deadline: " + deadline);
            }
        }

        class TaskList
        {
            public SortedDictionary<double, Task> list = new SortedDictionary<double, Task>();
            public string listName;
            public bool isEmpty;

            public TaskList(string name = "untitled list")
            {
                listName = name;
                isEmpty = true;
            }

            public bool addTask(Task task)
            {
                try
                {
                    list.Add(task.taskID, task);
                }
                catch (ArgumentException)
                {
                    return false;
                }

                isEmpty = false;
                return true;
            }

            public bool removeTask(int taskNum = 1)
            {
                int count = 1;

                foreach(KeyValuePair<double, Task> kvp in list)
                {
                    if (count == taskNum)
                        if (!list.Remove(kvp.Key))
                            return false;
                        else
                            return true;

                    count++;
                }

                return false;
            }

            // taskType: Incomplete = 0, Complete = 1
            public void printTasks(int taskType = 0, bool showAll = false, bool shorten = false, bool lines = true)
            {
                int count = 1;

                if (list.Count == 0)
                    Console.WriteLine("\n* You have no tasks *");
                else
                {
                    if (shorten == true)
                        Console.Write('\n');

                    foreach (KeyValuePair<double, Task> kvp in list)
                    {
                        if (lines)
                            Console.WriteLine(new string('-', Console.WindowWidth));

                        if (showAll)
                        {
                            if (!shorten)
                                kvp.Value.Print();
                            else if (shorten)
                            {
                                Console.Write(count + ") ");
                                kvp.Value.shortPrint();
                            }
                        }
                        else if (!showAll)
                        {
                            switch (taskType)
                            {
                                case 0:
                                    if (!kvp.Value.isCompleted)
                                        if (!shorten)
                                            kvp.Value.Print();
                                        else if (shorten)
                                        {
                                            Console.Write(count + ") ");
                                            kvp.Value.shortPrint();
                                        }
                                    break;
                                case 1:
                                    if (kvp.Value.isCompleted)
                                        if (!shorten)
                                            kvp.Value.Print();
                                        else if (shorten)
                                        {
                                            Console.Write(count + ") ");
                                            kvp.Value.shortPrint();
                                        }
                                    break;
                            }
                        }
                        count++;
                    }
                }
            }
        }
    }
}
