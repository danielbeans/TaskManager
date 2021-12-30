using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            return Database.Current.Tasks;
        }

        [HttpPost("add")]
        public Models.Task Add([FromBody] Models.Task task)
        {
            try
            {
                return Database.Current.AddTask(task);
            } catch (Exception)
            {
                return null;
            }
        }

        [HttpPost("update")]
        public bool Update([FromBody] Models.Task task)
        {
            if (!Database.Current.UpdateTask(task))
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
                Database.Current.Delete(id);
            } catch(Exception)
            {
                return false;
            }

            return true;
        }
    }
}
