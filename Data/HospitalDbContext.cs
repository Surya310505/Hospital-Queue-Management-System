using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using HospitalQueueSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalQueueSystem.Data
{
    public class HospitalDbContext:DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options):base(options)
        {
            
        }
        public DbSet<User> Users => Set<User>();
        public DbSet<Doctor> Doctors =>Set<Doctor>();
        public DbSet<PatientQueue> PatientQueues=>Set<PatientQueue>();
        public DbSet<Appoinment> Appoinments =>Set<Appoinment>();

    }
}