using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetzyVet.Domain.DTO
{
    public class VetCardDTO
    {
        public int VetId { get; set; }
        public string NPINumber { get; set; }

        public string Name {  get; set; }
        public string PhoneNumber { get; set; }
        public string Speciality { get; set; }
        public string Photo { get; set; }
        public string City { get; set; }
        public double Rating { get; set; }
        public double Counter { get; set; }
    }
}
