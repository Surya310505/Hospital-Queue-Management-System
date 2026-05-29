using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace HospitalQueueSystem.Models
{
    public class Appoinment
    {
        public int Id{get;set;}
        public int DoctorId{get;set;}
        public string Patientname{get;set;}=string.Empty;
        public DateTime Appoinmentdate{get;set;}
    }
}