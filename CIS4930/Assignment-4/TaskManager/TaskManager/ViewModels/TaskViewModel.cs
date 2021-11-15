using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace TaskManager.ViewModels
{
    class TaskViewModel : ItemViewModel, INotifyPropertyChanged
    {
        public override Visibility IsCompleteable => Visibility.Visible;

        public TaskViewModel(Task task)
        {
            //Item = task;
        }

        public TaskViewModel()
        {
            //Item = new task();
        }
    }
}
