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

        [HttpPost("add")]
        public Appointment Add([FromBody] Appointment appointment)
        {
            try
            {
                return Database.AddAppointment(appointment);
            }
            catch (Exception)
            {
                return null;
            }
        }

        [HttpPost("update")]
        public bool Update([FromBody] Appointment appointment)
        {
            if (!Database.UpdateAppointment(appointment))
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
                var appToDelete = Database.Appointments.FirstOrDefault(a => a.Id.ToString() == id);
                Database.Appointments.Remove(appToDelete);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}