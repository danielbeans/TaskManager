// Written by Daniel Williams
// Iteration 2

using System;
using System.IO;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;

namespace Task_Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Task> taskList = new List<Task>();
            List<Appointment> appList = new List<Appointment>();
            bool endApp = false;

            LoadFromFile(ref taskList, "tasks.json");
            LoadFromFile(ref appList, "appointments.json");

            while (!endApp)
            {
                DrawConsole();

                switch (SelectionLoop(6))
                {
                    case 1:
                        DrawConsole(1);
                        Console.WriteLine("1) Task\n2) Appointment");
                        Console.Write("\n> ");
                        switch (SelectionLoop(2))
                        {
                            case 1:
                                CreateTask(taskList);
                                break;
                            case 2:
                                CreateAppointment(appList);
                                break;
                        }
                        PauseUntilKeystroke();
                        break;
                    case 2:
                        DrawConsole(2);
                        Console.WriteLine("1) Task\n2) Appointment");
                        Console.Write("\n> ");
                        switch (SelectionLoop(2))
                        {
                            case 1:
                                DeleteTask(taskList);
                                break;
                            case 2:
                                DeleteAppointment(appList);
                                break;
                        }
                        PauseUntilKeystroke();
                        break;
                    case 3:
                        DrawConsole(3);
                        Console.WriteLine("1) Task\n2) Appointment");
                        Console.Write("\n> ");
                        switch (SelectionLoop(2))
                        {
                            case 1:
                                EditTask(taskList);
                                break;
                            case 2:
                                EditAppointment(appList);
                                break;
                        }
                        PauseUntilKeystroke();
                        break;
                    case 4:
                        DrawConsole(4);
                        completeTask(taskList);
                        PauseUntilKeystroke();
                        break;
                    case 5:
                        DrawConsole(5);
                        Console.WriteLine("1) Tasks\n2) Appointments\n3) All");
                        Console.Write("\n> ");
                        switch (SelectionLoop(3))
                        {
                            case 1:
                                PrintItems(taskList);
                                break;
                            case 2:
                                PrintItems(appList);
                                break;
                            case 3:
                                PrintAllItems(taskList, appList);
                                PauseUntilKeystroke();
                                break;
                        }
                        break;
                    case 6:
                        DrawConsole(6);
                        Search(new Tuple<List<Task>, List<Appointment>>(taskList, appList));
                        PauseUntilKeystroke();
                        break;
                    case 0:
                        endApp = true;
                        break;
                }
            }

            // Helper functions

            void CreateTask(List<Task> tasks)
            {
                Console.Clear();
                DrawConsole(1);

                Item item = CreateItem();
                DateTime Deadline = ParseDate("Deadline");
                tasks.Add(new Task(item.Name, item.Description, Deadline));

                SaveToFile(tasks, "tasks.json");
            }

            void CreateAppointment(List<Appointment> appointments)
            {
                Console.Clear();
                DrawConsole(1);

                Item item = CreateItem();

                DateTime Start = ParseDate("Start"), Stop = ParseDate("Stop");

                var attendees = new List<string>();
                Console.Write("Attendees (type nothing to stop)\n> ");
                string attendee = Console.ReadLine();
                while (attendee != "")
                {
                    attendees.Add(attendee);
                    Console.Write("> ");
                    attendee = Console.ReadLine();
                }
                appointments.Add(new Appointment(item.Name, item.Description, Start, Stop, attendees));

                SaveToFile(appointments, "appointments.json");
            }
           
            Item CreateItem()
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Description: ");
                string description = Console.ReadLine();

                return new Item(name, description);
            }
            
            void EditTask(List<Task> tasks)
            {
                Console.Clear();
                DrawConsole(3);
                PrintTasks(tasks);

                Console.Write("Choose task to edit > ");

                if (int.TryParse(Console.ReadLine(), out int taskNum) && taskNum <= tasks.Count & taskNum > 0)
                {
                    taskNum--;
                    Console.Clear();
                    DrawConsole(3);

                    DateTime? tempDateTime = null;

                    Console.WriteLine("Edit task (press ENTER to skip):");
                    Item item = EditItem();

                    Console.Write("Deadline: ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime Deadline))
                        tempDateTime = Deadline;

                    if (item.Name != "")
                        tasks[taskNum].Name = item.Name;

                    if (item.Description != "")
                        tasks[taskNum].Description = item.Description;

                    if (tempDateTime is not null)
                        tasks[taskNum].Deadline = (DateTime)tempDateTime;
                }

                SaveToFile(tasks, "tasks.json");
            }
            
            void EditAppointment(List<Appointment> appointments)
            {
                Console.Clear();
                DrawConsole(3);
                PrintAppointments(appointments);

                Console.Write("Choose appointment to edit > ");

                if (int.TryParse(Console.ReadLine(), out int appNum) && appNum <= appointments.Count && appNum > 0)
                {
                    appNum--;
                    Console.Clear();
                    DrawConsole(3);

                    DateTime? tempStart = null;
                    DateTime? tempStop = null;
                    var attendees = new List<string>();

                    Console.WriteLine("Edit task (press ENTER to skip):");
                    Item item = EditItem();

                    Console.Write("Start: ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime start))
                        tempStart = start;

                    Console.Write("Stop: ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime stop))
                        tempStop = stop;

                    Console.Write("Attendees (type nothing to stop)\n> ");
                    string attendee = Console.ReadLine();
                    while (attendee != "")
                    {
                        attendees.Add(attendee);
                        Console.Write("> ");
                        attendee = Console.ReadLine();
                    }

                    if (item.Name != "")
                        appointments[appNum].Name = item.Name;

                    if (item.Description != "")
                        appointments[appNum].Description = item.Description;

                    if (tempStart is not null)
                        appointments[appNum].Start = (DateTime)tempStart;

                    if (tempStop is not null)
                        appointments[appNum].Stop = (DateTime)tempStop;

                    if (attendees.Count > 0)
                        appointments[appNum].Attendees = attendees;
                }

                SaveToFile(appointments, "appointments.json");
            }
            
            Item EditItem()
            {
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("Description: ");
                string description = Console.ReadLine();

                return new Item(name, description);
            }

            void DeleteTask(List<Task> tasks)
            {
                Console.Clear();
                DrawConsole(2);
                PrintTasks(tasks);

                Console.Write("\nChoose task to delete > ");

                DeleteItem<Task>(tasks);
                SaveToFile(tasks, "tasks.json");
            }

            void DeleteAppointment(List<Appointment> appointments)
            {
                Console.Clear();
                DrawConsole(2);
                PrintAppointments(appointments);

                Console.Write("\nChoose item to delete > ");

                DeleteItem<Appointment>(appointments);
                SaveToFile(appointments, "appointments.json");
            }

            void DeleteItem<T>(List<T> items)
            {
                if (int.TryParse(Console.ReadLine(), out int itemNum) && itemNum <= items.Count && itemNum > 0)
                {
                    itemNum--;
                    Console.Clear();
                    DrawConsole(2);

                    items.RemoveAt(itemNum);
                    Console.WriteLine("* Item Deleted *");
                }
            }
            
            void PrintTasks(List<Task> tasks)
            {
                int count = 1;
                Console.WriteLine("--- Tasks ---\n");

                if (tasks.Count != 0)
                    foreach (var task in tasks)
                    {
                        Console.WriteLine($"{count++}){task}\n");
                    }
                else
                    Console.WriteLine("* No Tasks *\n");
            }

            void PrintAppointments(List<Appointment> appointments)
            {
                int count = 1;
                Console.WriteLine("--- Appointments ---\n");

                if (appointments.Count != 0)
                    foreach (var appointment in appointments)
                    {
                        Console.WriteLine($"{count++}){appointment}\n");
                    }
                else
                    Console.WriteLine("* No Appointments *");
            }

            void PrintItems<T>(List<T> items)
            {
                Console.Clear();
                DrawConsole(5);

                if (items.Count != 0)
                {
                    var itemNav = new ListNavigator<T>(items);
                    bool isNav = true;

                    while (isNav)
                    {
                        Console.Clear();
                        DrawConsole(5);

                        var page = itemNav.GetCurrentPage();
                        foreach (var item in page)
                        {
                            Console.WriteLine($"{item.Value}\n");
                        }

                        Console.WriteLine("<- (P)revious     (N)ext ->");
                        Console.Write("(Press ENTER to return) > ");


                        var selection = Console.ReadLine();
                        if (selection.Equals("P", StringComparison.InvariantCultureIgnoreCase) && itemNav.HasPreviousPage)
                        {
                            itemNav.GoBackward();
                        }
                        else if (selection.Equals("N", StringComparison.InvariantCultureIgnoreCase) && itemNav.HasNextPage)
                        {
                            itemNav.GoForward();
                        }
                        else if(selection.Equals(""))
                        {
                            isNav = false;
                        }
                    }
                }
                else
                    Console.WriteLine("No Tasks");
            }

            void PrintAllItems(List<Task> tasks, List<Appointment> apps)
            {
                Console.Clear();
                DrawConsole(5);

                if (tasks.Count != 0 || apps.Count != 0)
                {
                    int count = 1;
                    PrintTasks(tasks);
                    PrintAppointments(apps);
                }
                else
                    Console.WriteLine("* No Tasks or Appointments *");
            }

            void completeTask(List<Task> tasks)
            {
                Console.Clear();
                DrawConsole(3);
                PrintTasks(tasks);

                Console.Write("\nChoose task to complete > ");

                if (int.TryParse(Console.ReadLine(), out int taskNum) && taskNum <= tasks.Count && taskNum > 0)
                {
                    taskNum--;
                    Console.Clear();
                    DrawConsole(3);

                    tasks[taskNum].IsCompleted = true;
                    Console.WriteLine("* Task Completed *");
                }
            }

            void DrawConsole(int selection = 0)
            {
                Console.Clear();
                Console.WriteLine("Task Manager 2.0\n");

                switch (selection)
                {
                    case 1:
                        Console.WriteLine("####  Create a New Task/Appointment  ####\n");
                        break;
                    case 2:
                        Console.WriteLine("####  Delete a Task/Appointment  ####\n");
                        break;
                    case 3:
                        Console.WriteLine("####  Select a Task/Appointment to Edit  ####\n");
                        break;
                    case 4:
                        Console.WriteLine("####  Select a Task to Complete  ####\n");
                        break;
                    case 5:
                        Console.WriteLine("####  View Tasks and Appointments  ####\n");
                        break;
                    case 6:
                        Console.WriteLine("####  Search  ####\n");
                        break;

                    default:
                        PrintMenu();
                        Console.Write("\n> ");
                        break;
                }
            }

            void PrintMenu()
            {
                Console.WriteLine("** Commands **");
                Console.WriteLine("1) Create");
                Console.WriteLine("2) Delete");
                Console.WriteLine("3) Edit");
                Console.WriteLine("4) Complete a task");
                Console.WriteLine("5) View tasks/appointment");
                Console.WriteLine("6) Search");
                Console.WriteLine("0) Quit");
            }

            bool LoadFromFile<T>(ref List<T> items, string fileName)
            {
                try
                {
                    JsonSerializerOptions options = new() { WriteIndented = true };
                    string dataText = File.ReadAllText(fileName);
                    items = JsonSerializer.Deserialize<List<T>>(dataText, options);

                    return true;
                }
                catch
                {
                    return false;
                }
            }

            void SaveToFile<T>(List<T> items, string fileName)
            {
                JsonSerializerOptions options = new() { WriteIndented = true };
                File.WriteAllText(fileName, JsonSerializer.Serialize<List<T>>(items, options));
            }

            void Search(Tuple<List<Task>, List<Appointment>> items)
            {
                Console.Clear();
                DrawConsole(6);

                Console.Write("Type string to search > ");
                string query = Console.ReadLine();

                var selectTasks = from task in items.Item1
                                  where task.Name.Contains(query) || task.Description.Contains(query)
                                  select task;

                var selectAppoinments = from appointment in items.Item2
                                        where appointment.Name.Contains(query) || appointment.Description.Contains(query) || appointment.Attendees.Contains(query)
                                        select appointment;

                Console.WriteLine();
                List<Task> taskList = selectTasks.ToList();
                List<Appointment> appList= selectAppoinments.ToList();

                PrintTasks(taskList);
                PrintAppointments(appList);
            }

            // Quality of life functions

            void PauseUntilKeystroke()
            {
                Console.WriteLine("\nPress ENTER to return");
                var tempKey = Console.ReadLine();
            }

            int SelectionLoop(int limit = 0)
            {
                int selection;

                while (!int.TryParse(Console.ReadLine(), out selection) || selection < 0 || selection > limit)
                {
                    Console.WriteLine("Invalid selection.");
                    Console.Write("> ");
                }

                return selection;
            }

            DateTime ParseDate(string name = "")
            {
                if (name != "")
                    name += " ";

                DateTime date;
                Console.Write($"{name}Date (mm/hh/yy hr:min am/pm): ");

                // Notify user if Deadline is entered with wrong format
                while (!DateTime.TryParse(Console.ReadLine(), out date))
                {
                    Console.SetCursorPosition(0, Console.CursorTop - 1);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.CursorTop);
                    Console.Write("Deadline Date (mm/hh/yy hr:min am/pm): !Wrong format!");
                    Console.SetCursorPosition(Console.CursorLeft - 14, Console.CursorTop);
                }

                return date;
            }
        }
    }
}
