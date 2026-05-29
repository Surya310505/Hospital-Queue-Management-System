using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalQueueSystem.Models
{
    public class PatientQueue
    {
        public int Id{get;set;}
        public string Patientname{get;set;}=string.Empty;
        public string Department{get;set;}=string.Empty;
        public int Tokennumber{get;set;}
        public bool IsEmergency{get;set;}
        public int Emergencyscore{get;set;}
        public DateTime Created{get;set;}=DateTime.UtcNow;
    }
}