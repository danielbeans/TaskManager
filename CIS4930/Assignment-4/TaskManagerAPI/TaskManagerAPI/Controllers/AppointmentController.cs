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
    public class AppointmentController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Appointment> Get()
        {
            return Database.Appointments;
        }
    }
}
