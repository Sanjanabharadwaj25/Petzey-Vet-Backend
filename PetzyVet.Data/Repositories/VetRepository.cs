using PetzyVet.Domain.DTO;
using PetzyVet.Domain.Entities;
using PetzyVet.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetzyVet.Data.Repositories
{
    public class VetRepository : IVetRepository
    {
        private VetDbContext db;
        public VetRepository(VetDbContext context)
        {
            this.db= context;
            
        }

        public void AddVet(Vet vet)
        {
            db.Vets.Add(vet);
            db.SaveChanges();
        }

        public void DeleteVet(int id)
        {
            db.Vets.Remove(db.Vets.Find(id));
            db.Addresses.Remove(db.Addresses.Find(db.Vets.Find(id).AddressId));
            db.SaveChanges();
        }

        public void EditStatus(bool status, int id)
        {
            Vet existingVet = db.Vets.Find(id);

            if (existingVet != null)
            {
                existingVet.Status = status;

                db.Entry(existingVet).State = EntityState.Modified;

                db.SaveChanges();
            }
        }
        public void updatePhoto(int vetId, string photoPath)
        {
            var vet = db.Vets.Find(vetId);

            if (vet != null)
            {
                // Update the Photo property
                vet.Photo = photoPath;

                // Optionally, save changes to the database
                db.SaveChanges();
            }
        }


        public List<Vet> GetAllVets()
        {
            return db.Vets.ToList();
        }

        public Vet GetVetById(int id)
        {
            return db.Vets.Find(id);
        }

        public void UpdateVet(Vet vet)
        {
            var existingVet = db.Vets.Find(vet.VetId);
            if (existingVet != null)
            {
                db.Entry(existingVet).CurrentValues.SetValues(vet);
                db.SaveChanges();
            }
        }
        public void UpdateVetPhoto(Vet vet)
        {
            var existingVet = db.Vets.Find(vet.VetId);
            if (existingVet != null)
            {
                db.Entry(existingVet.Photo).CurrentValues.SetValues(vet.Photo);
                db.SaveChanges();
            }
        }
        public List<VetDTO> GetAllVetIdsAndNames()
        {
            var vets = db.Vets
                .Where(v => v.Status) // Filter where Status is true
                .Select(v => new VetDTO
                {
                    VetId = v.VetId,
                    Name = v.FName + " " + v.LName
                })
                .ToList();

            return vets;
        }


        public void UpdateRating(int docid, int rating)
        {
            var doc = db.Vets.Find(docid);
            if (doc != null)
            {
                double sum = doc.Rating * doc.Counter;
                doc.Counter++;
                sum += rating;
                doc.Rating = sum / doc.Counter;
                db.Entry(doc).State = EntityState.Modified;
                db.SaveChanges();

            }
        }

        public List<string> GetUniqueSpecialties()
        {
            return db.Vets.Select(v => v.Speciality).Distinct().ToList();
        }

        public List<Vet> GetVetsBySpecialty(List<string> specialties)
        {
            return db.Vets.Where(v => specialties.Contains(v.Speciality)).ToList();
        }

        public Vet GetVetByNpiNumber(string npiNumber)
        {
            return db.Vets.Where(v=>v.NPINumber==npiNumber).FirstOrDefault();
        }
        public bool CheckNpiNumber(string Npi)
        {
            return db.Vets.Where(v=>v.NPINumber.Equals(Npi)).Any();
        }
    }
}
