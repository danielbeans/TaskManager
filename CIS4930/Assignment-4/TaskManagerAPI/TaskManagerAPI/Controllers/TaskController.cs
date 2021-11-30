using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagerAPI.Models;
using TaskManagerAPI.Persistance;

namespace TaskManagerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Models.Task> Get()
        {
            return Database.Tasks;
        }

        [HttpPost("add")]
        public bool Add([FromBody] Models.Task task)
        {
            try
            {
                Database.Tasks.Add(task);
            } catch (Exception)
            {
                return false;
            }

            return true;
        }

        [HttpPost("update")]
        public bool Update([FromBody] Models.Task task)
        {
            if (!Database.UpdateTask(task))
            { 
                return false;
            } 

            return true;
        }


        [HttpGet("delete/{id}")]
        public bool Delete(string id)
        {
            try
            {
                var taskToDelete = Database.Tasks.FirstOrDefault(a => a.Id.ToString() == id);
                Database.Tasks.Remove(taskToDelete);
            } catch(Exception)
            {
                return false;
            }

            return true;
        }
    }
}
