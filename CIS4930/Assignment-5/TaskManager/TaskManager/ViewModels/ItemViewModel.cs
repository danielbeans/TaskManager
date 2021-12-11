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
    public class ItemViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Item Item { get; set; }

        public virtual Visibility IsCompleteable { get; }
    }
}
