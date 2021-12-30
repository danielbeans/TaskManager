using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel;

namespace TaskManagerAPI.Models
{
    public class Task: Item, INotifyPropertyChanged
    {
        public Task()
        {
            BoundDeadline = DateTime.Today;
            Priority = 1;
        }

        [BsonElement("Deadline")]
        public DateTime Deadline { get; set; }
        [BsonElement("BoundDeadline")]
        private DateTimeOffset boundDeadline;
        [BsonIgnore]
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

        [BsonElement("IsCompleted")]
        private bool isCompleted;
        [BsonIgnore]
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

        [BsonIgnore]
        public override bool Completed { get => IsCompleted; set => IsCompleted = value; }

        [BsonElement("Display")]
        public override string Display => $"{Name}{(Completed ? " (Completed)" : "")}\n{Description}\n{Deadline.ToString("d")}\n(Task)";
    }
}
