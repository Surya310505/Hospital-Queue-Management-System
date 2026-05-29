using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalQueueSystem.Data;
using HospitalQueueSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalQueueSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueueControllers:ControllerBase
    {
        private readonly HospitalDbContext _context;
        public QueueControllers(HospitalDbContext context)
        {
            _context=context;
        }
        [HttpPost]
        public IActionResult AddPatient(PatientQueue patient)
        {
            var lasttoken=_context.PatientQueues.OrderByDescending(x=>x.Tokennumber).FirstOrDefault();
            patient.Tokennumber=lasttoken==null?1:lasttoken.Tokennumber+1;
            patient.Emergencyscore=PredictEmergency(patient);
            _context.PatientQueues.Add(patient);
            _context.SaveChanges();
            return Ok(patient);
        }
        [HttpGet]
        public IActionResult GetQueue()
        {
            var queue=_context.PatientQueues.OrderByDescending(x=>x.Emergencyscore).ThenBy(x=>x.Created).ToList();
            return Ok(queue);
        }
        [NonAction]
        public int PredictEmergency(PatientQueue patient)
        {
            if(patient.IsEmergency) return 100;
            return 20;
        }
    }
}