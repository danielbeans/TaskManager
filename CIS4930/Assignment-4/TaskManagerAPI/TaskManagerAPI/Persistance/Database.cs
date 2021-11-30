﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Persistance
{
    public class Database
    {
        public static List<Models.Task> Tasks = new List<Models.Task>
        {
            new Models.Task { Id = 1, Name = "First", Description = "First task", Priority = 2, Deadline = DateTime.Now},
            new Models.Task { Id = 2, Name = "Second", Description = "Second task", Priority = 3, Deadline = DateTime.Now} 
        };

        public static List<Appointment> Appointments = new List<Appointment>
        {
            new Appointment { Id = 1, Name = "First", Description = "First appointment", Priority = 2, StartTime = DateTime.Now, EndTime = DateTime.Now},
            new Appointment { Id = 2, Name = "Second", Description = "Second appointment", Priority = 3, StartTime = DateTime.Now, EndTime = DateTime.Now}
        };

        public static bool UpdateTask(Models.Task newTask)
        {
            var index = Tasks.FindIndex(x => x.Id == newTask.Id);
            if(index > -1)
            {
                Tasks[index] = newTask;
                return true;
            }

            return false;
        }

        public static bool UpdateAppointment(Appointment newApp)
        {
            var index = Appointments.FindIndex(x => x.Id == newApp.Id);
            if (index > -1)
            {
                Appointments[index] = newApp;
                return true;
            }

            return false;
        }
    }
}
