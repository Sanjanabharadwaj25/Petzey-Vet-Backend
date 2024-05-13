using PetzyVet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetzyVet.Data
{
    public class VetDbContext:DbContext
    {
        public VetDbContext():base("defaultConnection")
        {
            
        }
        public DbSet<Vet>Vets { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
