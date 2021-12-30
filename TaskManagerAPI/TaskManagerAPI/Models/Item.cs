using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TaskManagerAPI.Models
{
    public class Item : INotifyPropertyChanged
    {
        public static int s_currentID = 1;

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        private int id;
        public int Id { get => id; set => id = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        [BsonElement("Name")]
        private string name;
        [BsonIgnore]
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

        [BsonElement("Description")]
        private string description;
        [BsonIgnore]
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

        [BsonElement("DateAdded")]
        public DateTime DateAdded { get; }

        [BsonElement("Priority")]
        public int Priority { get; set; }

        public Item()
        {
            DateAdded = DateTime.Now;
            Id = 0;
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
            Id = item.Id;
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
