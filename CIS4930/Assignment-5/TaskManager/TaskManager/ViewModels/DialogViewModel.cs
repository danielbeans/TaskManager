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

        private IList<ItemViewModel> _items;
        public TaskViewModel BoundTask { get; set; }
        public AppointmentViewModel BoundAppointment { get; set; }

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

                if ((BoundTask == null || BoundTask.Item.Id <= 0) && (BoundAppointment == null || BoundAppointment.Item.Id <= 0))
                {
                    if (isTask)
                    {
                        BoundTask = new TaskViewModel();
                        BoundAppointment = null;
                    }
                    else
                    {
                        BoundAppointment = new AppointmentViewModel();
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
                    switch (BoundTask.Item.Priority)
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
                    switch (BoundAppointment.Item.Priority)
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
                            BoundTask.Item.Priority = 1;
                        else
                            BoundAppointment.Item.Priority = 1;
                        break;
                    case "Medium":
                        if (isTask)
                            BoundTask.Item.Priority = 2;
                        else
                            BoundAppointment.Item.Priority = 2;
                        break;
                    case "High":
                        if (isTask)
                            BoundTask.Item.Priority = 3;
                        else
                            BoundAppointment.Item.Priority = 3;
                        break;
                }

                OnPropertyChanged("Priority");
            }
        }


        public Visibility IsTaskVisible { get => IsTask ? Visibility.Visible : Visibility.Collapsed; }
        public Visibility IsAppointmentVisible { get => IsTask ? Visibility.Collapsed : Visibility.Visible; }

        public DialogViewModel(ItemViewModel item)
        {
            if (item is AppointmentViewModel)
            {
                BoundAppointment = item as AppointmentViewModel;
                BoundTask = null;
                IsTask = false;

                OnPropertyChanged("BoundAppointment");
            }
            else if (item is TaskViewModel)
            {
                BoundTask = item as TaskViewModel;
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
