using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models;
using Windows.UI.Xaml;

namespace TaskManager.ViewModels
{
    class AppointmentViewModel : ItemViewModel, INotifyPropertyChanged
    {
        public override Visibility IsCompleteable => Visibility.Collapsed;

        public AppointmentViewModel(Appointment app)
        {
            Item = app;
        }

        public AppointmentViewModel()
        {
            Item = new Appointment();
        }
    }
}
