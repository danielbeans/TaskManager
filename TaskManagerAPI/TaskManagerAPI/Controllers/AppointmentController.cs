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
            return Database.Current.Appointments;
        }

        [HttpPost("add")]
        public Appointment Add([FromBody] Appointment appointment)
        {
            try
            {
                return Database.Current.AddAppointment(appointment);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost("update")]
        public bool Update([FromBody] Appointment appointment)
        {
            if (!Database.Current.UpdateAppointment(appointment))
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
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}