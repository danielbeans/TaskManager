using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Persistance
{
    public class Database
    {
        private static int s_currentID = 1;

        public static List<Models.Task> Tasks = new List<Models.Task>();
        public static List<Appointment> Appointments = new List<Appointment>();

        public static Models.Task AddTask(Models.Task newTask)
        {
            newTask.Id = s_currentID++;
            Tasks.Add(newTask);
            return newTask;
        }

        public static Appointment AddAppointment(Appointment newApp)
        {
            newApp.Id = s_currentID++;
            Appointments.Add(newApp);
            return newApp;
        }

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
