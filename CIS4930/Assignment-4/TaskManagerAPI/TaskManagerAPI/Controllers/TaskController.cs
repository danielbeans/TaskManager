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
        public string Add([FromBody] Models.Task task)
        {
            try
            {
                Database.Tasks.Add(task);
            } catch (Exception)
            {
                return "Could not add to database";
            }

            return "Successfully added task";
        }

        [HttpGet("Delete/{id}")]
        public bool Delete(string id)
        {
            try
            {
                var appToDelete = Database.Tasks.FirstOrDefault(a => a.Id.ToString() == id);
                Database.Tasks.Remove(appToDelete);
            } catch(Exception)
            {
                return false;
            }

            return true;
        }


    }
}
