using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace TaskManagerAPI.Models
{
    public class Appointment: Item , INotifyPropertyChanged
    {
        public Appointment()
        {
            BoundStart = DateTime.Today;
            BoundEnd = DateTime.Today.AddDays(1);
            Priority = 1;
        }


        public ObservableCollection<string> Attendees { get; set; }
        private string attendeesString;
        public string AttendeesString
        {
            get
            {
                return attendeesString;
            }
            set
            {
                attendeesString = value;
                if (attendeesString == null)
                    attendeesString = "";
                Attendees = new ObservableCollection<string>(attendeesString.Split(',').ToList());
                OnPropertyChanged("Attendees");
            }
        }

        public DateTime StartTime { get; set; }
        private DateTimeOffset boundStart;
        public DateTimeOffset BoundStart
        {
            get
            {
                return boundStart;
            }
            set
            {
                boundStart = value;
                StartTime = boundStart.Date;
                OnPropertyChanged("StartTime");
            }
        }
        public DateTime EndTime { get; set; }
        private DateTimeOffset boundEnd;
        public DateTimeOffset BoundEnd
        {
            get
            {
                return boundEnd;
            }
            set
            {
                boundEnd = value;
                EndTime = boundEnd.Date;
                OnPropertyChanged("EndTime");
            }
        }

        public override string Display => $"{Name}\n{Description}\n{StartTime.ToString("d")} - {EndTime.ToString("d")}\n{attendeesString}\n(Appointment)";

    }
}
