using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace HospitalQueueSystem.Models
{
    public class Doctor
    {
        public int Id{get;set;}
        public string Name{get;set;}=string.Empty;
        public string Department{get;set;}=string.Empty;
        public bool IsAvailable{get;set;}=true;

    }
}