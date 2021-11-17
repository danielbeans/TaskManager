using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models;

namespace TaskManager.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        internal static string PersistencePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\SaveData.json";
        internal static JsonSerializerSettings Settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        private bool sortByPriority;

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<ItemViewModel> Items { get; set; }
        public ObservableCollection<ItemViewModel> FilteredItems
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return sortByPriority
                        ? new ObservableCollection<ItemViewModel>(Items.OrderByDescending(t => t.Item.Priority))
                        : new ObservableCollection<ItemViewModel>(Items.OrderBy(t => t.Item.Name));
                }

                return new ObservableCollection<ItemViewModel>(Items.Where(t => t.Item.Name.ToUpper().Contains(Query.ToUpper())
                                                    || t.Item.Description.ToUpper().Contains(Query.ToUpper())
                                                    || ((t.Item is Appointment) && (t.Item as Appointment).Attendees.Any(s => s.Contains(Query)))
                                                        ).OrderBy(t => t.Item.Priority));
            }
        }
        public MainViewModel()
        {
            Items = new ObservableCollection<ItemViewModel>();
        }

        public ItemViewModel SelectedItem { get; set; }

        public void RemoveItem()
        {
            if (SelectedItem != null)
            {
                Items.Remove(SelectedItem);
            }
        }

        public void ToggleCompleteness()
        {
            if (SelectedItem == null || (SelectedItem.Item as Models.Task) == null)
            {
                RefreshList();
                return;
            }
            (SelectedItem.Item as Models.Task).IsCompleted = !(SelectedItem.Item as Models.Task).IsCompleted;
            RefreshList();
        }

        public void RefreshList()
        {
            OnPropertyChanged("FilteredItems");
            SaveState();
        }

        public string Query { get; set; }

        public void SaveState()
        {
            File.WriteAllText(PersistencePath, JsonConvert.SerializeObject(this, Settings));
        }
        public void SortByPriority()
        {
            sortByPriority = true;
            RefreshList();
        }

        public void SortOff()
        {
            sortByPriority = false;
            RefreshList();
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
