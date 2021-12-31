using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Persistance
{
    public class Database
    {
        private static readonly Random _random = new Random();
        private IMongoDatabase _database;
        private Database(IMongoDatabase db)
        {
            _database = db;
        }

        private static Database instance;
        public static Database Current
        {
            get
            {
                if (instance == null)
                {
                    var settings = MongoClientSettings.FromConnectionString("mongodb+srv://daniel:lQM8H1GPEagLH2hX@cis-4930.zfoco.mongodb.net/Assignment-5?authSource=admin");
                    var client = new MongoClient(settings);
                    var db = client.GetDatabase("Assignment-5");
                    instance = new Database(db);
                }

                return instance;
            }
        }


        public Models.Task AddTask(Models.Task newTask)
        {
            newTask.Id = _random.Next();
            BsonDocument newTaskDocument = BsonDocument.Parse(JsonConvert.SerializeObject(newTask));
            _database.GetCollection<BsonDocument>("tasks").InsertOne(newTaskDocument);
            return newTask;
        }

        public Appointment AddAppointment(Appointment newApp)
        {
            newApp.Id = _random.Next();
            BsonDocument newAppDocument = BsonDocument.Parse(JsonConvert.SerializeObject(newApp));
            _database.GetCollection<BsonDocument>("appointments").InsertOne(newAppDocument);
            return newApp;
        }

        public bool UpdateTask(Models.Task newTask)
        {
            var replaceFilter = Builders<BsonDocument>.Filter.Eq("Id", newTask.Id);
            BsonDocument newTaskDocument = BsonDocument.Parse(JsonConvert.SerializeObject(newTask));
            _database.GetCollection<BsonDocument>("tasks").ReplaceOne(replaceFilter, newTaskDocument);
            return true;
        }

        public bool UpdateAppointment(Appointment newApp)
        {
            var replaceFilter = Builders<BsonDocument>.Filter.Eq("Id", newApp.Id);
            BsonDocument newAppDocument = BsonDocument.Parse(JsonConvert.SerializeObject(newApp));
            _database.GetCollection<BsonDocument>("appointments").ReplaceOne(replaceFilter, newAppDocument);
            return true;
        }

        public bool Delete(string id)
        {
            var deleteFilter = Builders<BsonDocument>.Filter.Eq("Id", int.Parse(id));
            _database.GetCollection<BsonDocument>("tasks").DeleteOne(deleteFilter);
            _database.GetCollection<BsonDocument>("appointments").DeleteOne(deleteFilter);
            return true;
        }

        public List<Models.Task> Tasks
        {
            get
            {
                var taskBson = _database.GetCollection<BsonDocument>("tasks");
                var data = taskBson.Find(_ => true).ToList();
                var _tasks = new List<Models.Task>();
                foreach (var item in data)
                {
                    var obj = BsonSerializer.Deserialize<Models.Task>(item);
                    _tasks.Add(obj);
                }
                return _tasks;
            }

        }

        public List<Appointment> Appointments
        {
            get
            {
                var appBson = _database.GetCollection<BsonDocument>("appointments");
                var data = appBson.Find(_ => true).ToList();
                var _apps = new List<Appointment>();
                foreach (var item in data)
                {
                    var json = item.ToJson();
                    var obj = BsonSerializer.Deserialize<Appointment>(item);
                    _apps.Add(obj);
                }
                return _apps;
            }

        }
    }
}
