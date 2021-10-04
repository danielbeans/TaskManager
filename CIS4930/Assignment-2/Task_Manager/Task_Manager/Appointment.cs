using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager
{
    interface IAppointment : IItem
    {
        DateTime Start { get; set; }
        DateTime Stop { get; set; }
        List<string> Attendees { get; set; }
        double AppointmentID { get; set; }
    }
    class Appointment : Item, IAppointment
    {
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public List<string> Attendees { get; set; }
        public double AppointmentID { get; set; }

        public Appointment() : base()
        {
            Start = new DateTime(2020, 12, 31, 23, 59, 59);
            Stop = new DateTime(2021, 1, 1, 0, 0, 0);
            AppointmentID = -1;
        }

        public Appointment(string name, string description, DateTime Start, DateTime Stop, List<string> Attendees) : base(name, description)
        {
            this.Start = Start;
            this.Stop = Stop;
            this.Attendees = Attendees;
            AppointmentID = double.Parse(Stop.ToString("yyyyMMddHHmmss"));
        }

        public Appointment(Appointment appointment) : base(appointment)
        {
            Start = appointment.Start;
            Stop = appointment.Stop;
            AppointmentID = appointment.AppointmentID;
        }
        public override string ToString()
        {
            var stringReturn = $"(APP)\n- {Name} - Start: {Start}  End:{Stop}\n- {Description}\n- {{ ";
            foreach (var attendee in Attendees)
            {
                stringReturn += $"{attendee}, ";
            }

            return $"{stringReturn.Substring(0, stringReturn.Length - 2)} }}";
        }
    }
}
