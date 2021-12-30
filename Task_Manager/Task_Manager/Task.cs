using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager
{
    interface ITask : IItem
    {
        DateTime Deadline { get; set; }
        bool IsCompleted { get; set; }
        double TaskID { get; set; }
    }
    class Task : Item, ITask
    {
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
        public double TaskID { get; set; }

        public Task() : base()
        {
            Deadline = new DateTime(2020, 12, 31, 23, 59, 59);
            TaskID = -1;
        }

        public Task(string name, string description, DateTime Deadline, bool IsCompleted = false) : base(name, description)
        {
            this.Deadline = Deadline;
            this.IsCompleted = IsCompleted;
            TaskID = double.Parse(Deadline.ToString("yyyyMMddHHmmss"));
        }

        public Task(Task task) : base(task)
        {
            Deadline = task.Deadline;
            IsCompleted = task.IsCompleted;
            TaskID = task.TaskID;
        }

        public override string ToString()
        {
            string completed = "";
            if (IsCompleted)
                completed = "x";
            return $"(TASK)\n- {Name} - Due: {Deadline}\n- {Description}\n- Completed [{completed}]";
        }
    }
}
