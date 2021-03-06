using System;
using System.ComponentModel;

namespace TaskManager.Models
{
    public class Task: Item, INotifyPropertyChanged
    {
        public Task()
        {
            BoundDeadline = DateTime.Today;
            Priority = 1;
        }

        public DateTime Deadline { get; set; }
        private DateTimeOffset boundDeadline;
        public DateTimeOffset BoundDeadline
        {
            get
            {
                return boundDeadline;
            }
            set
            {
                boundDeadline = value;
                Deadline = boundDeadline.Date;
                OnPropertyChanged(nameof(Deadline));
            }
        }

        private bool isCompleted;
        public bool IsCompleted
        {
            get
            {
                return isCompleted;
            }
            set
            {
                isCompleted = value;
                OnPropertyChanged(nameof(Completed));
            }
        }

        public override bool Completed { get => IsCompleted; set => IsCompleted = value; }

        public override string Display => $"{Name}{(Completed ? " (Completed)" : "")}\n{Description}\n{Deadline.ToString("d")}\n(Task)";
    }
}
