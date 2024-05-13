using PetzyVet.Domain.DTO;
using PetzyVet.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetzyVet.Domain.Interfaces
{
    public interface IVetRepository
    {
        List<Vet> GetAllVets();
        Vet GetVetById(int id);
        void AddVet(Vet vet);
        void DeleteVet(int id);
        void UpdateVet(Vet vet);
        void EditStatus(bool status,int id);
        List<VetDTO> GetAllVetIdsAndNames();
        void UpdateRating(int docid, int rating);
        List<string> GetUniqueSpecialties();
        List<Vet> GetVetsBySpecialty(List<string> specialties);
        void updatePhoto(int vetId, string photoPath);
        Vet GetVetByNpiNumber(string npiNumber);
        bool CheckNpiNumber(string Npi);
        void UpdateVetPhoto(Vet vet);
    }
}
