using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TaskManager.ViewModels
{
    class DialogViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private IList<Item> _items;
        public Models.Task BoundTask { get; set; }
        public Appointment BoundAppointment { get; set; }

        private bool isTask;
        public bool IsTask
        {
            get
            {
                return isTask;
            }

            set
            {
                isTask = value;

                if ((BoundTask == null || BoundTask.Id <= 0) && (BoundAppointment == null || BoundAppointment.Id <= 0))
                {
                    if (isTask)
                    {
                        BoundTask = new Models.Task();
                        BoundAppointment = null;
                    }
                    else
                    {
                        BoundAppointment = new Appointment();
                        BoundTask = null;
                    }
                }

                OnPropertyChanged("IsTaskVisible");
                OnPropertyChanged("IsAppointmentVisible");
                OnPropertyChanged("BoundAppointment");
                OnPropertyChanged("BoundTask");
            }
        }

        private string priority;
        public string Priority
        {
            get
            {
                if (IsTask)
                {
                    switch (BoundTask.Priority)
                    {
                        case 1:
                            return "Low";
                        case 2:
                            return "Medium";
                        case 3:
                            return "High";
                    }
                }
                else
                {
                    switch (BoundAppointment.Priority)
                    {
                        case 1:
                            return "Low";
                        case 2:
                            return "Medium";
                        case 3:
                            return "High";
                    }
                }
                return "Low";
            }
            set
            {
                priority = value;

                switch (priority)
                {
                    case "Low":
                        if (isTask)
                            BoundTask.Priority = 1;
                        else
                            BoundAppointment.Priority = 1;
                        break;
                    case "Medium":
                        if (isTask)
                            BoundTask.Priority = 2;
                        else
                            BoundAppointment.Priority = 2;
                        break;
                    case "High":
                        if (isTask)
                            BoundTask.Priority = 3;
                        else
                            BoundAppointment.Priority = 3;
                        break;
                }

                OnPropertyChanged("Priority");
            }
        }


        public Visibility IsTaskVisible { get => IsTask ? Visibility.Visible : Visibility.Collapsed; }
        public Visibility IsAppointmentVisible { get => IsTask ? Visibility.Collapsed : Visibility.Visible; }

        public DialogViewModel(Item item)
        {
            if (item is Appointment)
            {
                BoundAppointment = item as Appointment;
                BoundTask = null;
                IsTask = false;

                OnPropertyChanged("BoundAppointment");
            }
            else if (item is Models.Task)
            {
                BoundTask = item as Models.Task;
                BoundAppointment = null;
                IsTask = true;

                OnPropertyChanged("BoundTask");
            }
            else
            {
                IsTask = true;
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
