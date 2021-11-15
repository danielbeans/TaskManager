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
        public ObservableCollection<Item> Items { get; set; }
        public ObservableCollection<Item> FilteredItems
        {
            get
            {
                if (string.IsNullOrEmpty(Query))
                {
                    return sortByPriority
                        ? new ObservableCollection<Item>(Items.OrderByDescending(t => t.Priority))
                        : new ObservableCollection<Item>(Items.OrderBy(t => t.Name));
                }

                return new ObservableCollection<Item>(Items.Where(t => t.Name.ToUpper().Contains(Query.ToUpper())
                                                    || t.Description.ToUpper().Contains(Query.ToUpper())
                                                    || ((t is Appointment) && (t as Appointment).Attendees.Any(s => s.Contains(Query)))
                                                        ).OrderBy(t => t.Priority));
            }
        }
        public MainViewModel()
        {
            Items = new ObservableCollection<Item>();
        }

        public Item SelectedItem { get; set; }

        public void RemoveItem()
        {
            if (SelectedItem != null)
            {
                Items.Remove(SelectedItem);
            }
        }

        public void ToggleCompleteness()
        {
            if (SelectedItem == null || (SelectedItem as Models.Task) == null)
            {
                RefreshList();
                return;
            }
            (SelectedItem as Models.Task).IsCompleted = !(SelectedItem as Models.Task).IsCompleted;
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
