using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace TaskManager.Models
{
    public class Item : INotifyPropertyChanged
    {
        private static int s_currentID= 0;

        private int id;
        public int Id { get => id; set => id = s_currentID++; }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual Visibility IsCompleteable { get; }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                if (name == null)
                    name = "";
                OnPropertyChanged(nameof(Name));
            }
        }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                if (description == null)
                    description = "";
                OnPropertyChanged(nameof(Description));
            }
        }
        public DateTime DateAdded { get; }
        public int ItemID { get; }

        public int Priority { get; set; }

        public Item()
        {
            DateAdded = DateTime.Now;
            ItemID = s_currentID;
            s_currentID++;
        }

        public Item(string name, string description) : this()
        {
            Name = name;
            Description = description;
        }

        public Item(Item item)
        {
            Name = item.Name;
            Description = item.Description;
            DateAdded = item.DateAdded;
            ItemID = item.ItemID;
        }

        public virtual bool Completed { get; set; }
        public virtual string Display { get; }

        public override string ToString()
        {
            return $"{Name} - {Description}";
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
