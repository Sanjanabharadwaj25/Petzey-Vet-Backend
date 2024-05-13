namespace PetzyVet.Data.Migrations
{
    using PetzyVet.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PetzyVet.Data.VetDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PetzyVet.Data.VetDbContext context)
        {
            var addresses = new List<Address>
{
    new Address
    {
        City = "New York",
        Street = "123 Main St",
        ZipCode = "10001",
        State = "NY"
    },
    new Address
    {
        City = "Los Angeles",
        Street = "456 Elm St",
        ZipCode = "90001",
        State = "CA"
    },
    new Address
    {
        City = "Chicago",
        Street = "789 Oak St",
        ZipCode = "60601",
        State = "IL"
    },
    new Address
    {
        City = "Houston",
        Street = "101 Pine St",
        ZipCode = "77001",
        State = "TX"
    },
    new Address
    {
        City = "Philadelphia",
        Street = "202 Maple St",
        ZipCode = "19101",
        State = "PA"
    },
    new Address
    {
        City = "Bengaluru",
        Street = "202 Maple Street",
        ZipCode = "12301",
        State = "Karnataka"
    },
    new Address
    {
        City = "Bengaluru",
        Street = "202 Maple Street",
        ZipCode = "12001",
        State = "Karnataka"
    }
};



            var moreVets = new List<Vet>
{
    new Vet
    {
        LName = "Garcia",
        FName = "Maria",
        NPINumber = "135792468",
        Username = "mariagarcia",
        Phone = "555-555-5555",
        Email = "maria.garcia@example.com",
        Speciality = "Dentistry",
        ShortBio = "Passionate about pet dental health and hygiene.",
        Status = true,
        Photo = "maria_garcia.jpg",
        Gender = "Female",
        DOB = new DateTime(1985, 10, 10),
        Rating = 4.2,
        //AddressId = 3 // You need to provide a valid AddressId here
        Address =addresses[0]
    },
    new Vet
    {
        LName = "Johnson",
        FName = "Michael",
        NPINumber = "246813579",
        Username = "michaeljohnson",
        Phone = "111-111-1111",
        Email = "michael.johnson@example.com",
        Speciality = "Oncology",
        ShortBio = "Expertise in diagnosing and treating pet cancers.",
        Status = true,
        Photo = "michael_johnson.jpg",
        Gender = "Male",
        DOB = new DateTime(1978, 7, 20),
        Rating = 4.9,
        //AddressId = 4 ,// You need to provide a valid AddressId here
        Address=addresses[1]
    },
    new Vet
    {
        LName = "Brown",
        FName = "Emily",
        NPINumber = "369258147",
        Username = "emilybrown",
        Phone = "999-999-9999",
        Email = "emily.brown@example.com",
        Speciality = "Behavioral Medicine",
        ShortBio = "Passionate about understanding and addressing pet behavior issues.",
        Status = true,
        Photo = "emily_brown.jpg",
        Gender = "Female",
        DOB = new DateTime(1982, 4, 5),
        Rating = 4.7,
        //AddressId = 5, // You need to provide a valid AddressId here
        Address=addresses[2]
    },
    new Vet
    {
        LName = "Martinez",
        FName = "David",
        NPINumber = "987654321",
        Username = "davidmartinez",
        Phone = "777-777-7777",
        Email = "david.martinez@example.com",
        Speciality = "Emergency Medicine",
        ShortBio = "Experienced in handling pet emergencies with care and efficiency.",
        Status = true,
        Photo = "david_martinez.jpg",
        Gender = "Male",
        DOB = new DateTime(1970, 12, 15),
        Rating = 4.6,
        //AddressId = 6 ,// You need to provide a valid AddressId here
        Address=addresses[3]
    },
    new Vet
    {
        LName = "Anderson",
        FName = "Jessica",
        NPINumber = "123487695",
        Username = "jessicaanderson",
        Phone = "888-888-8888",
        Email = "jessica.anderson@example.com",
        Speciality = "Dermatology",
        ShortBio = "Specializing in diagnosing and treating skin conditions in pets.",
        Status = true,
        Photo = "jessica_anderson.jpg",
        Gender = "Female",
        DOB = new DateTime(1973, 9, 30),
        Rating = 4.3,
        //AddressId = 7 // You need to provide a valid AddressId here
        Address=addresses[4]
    },
    new Vet
    {
        LName = "Soujanya",
        FName = "S",
        NPINumber = "134487695",
        Username = "SoujanyaS",
        Phone = "888-888-8888",
        Email = "soujanya6620@gmail.com",
        Speciality = "Dermatology",
        ShortBio = "Specializing in diagnosing and treating skin conditions in pets.",
        Status = true,
        Photo = "jessica_anderson.jpg",
        Gender = "Female",
        DOB = new DateTime(1973, 9, 30),
        Rating = 4.3,
        //AddressId = 7 // You need to provide a valid AddressId here
        Address=addresses[5]
    },
    new Vet
    {
        LName = "Sanjana",
        FName = "Bharadwaj",
        NPINumber = "125587695",
        Username = "Sanjana",
        Phone = "888-888-8888",
        Email = "sanjana.bharadwaj01@gmail.com",
        Speciality = "Dermatology",
        ShortBio = "Specializing in diagnosing and treating skin conditions in pets.",
        Status = true,
        Photo = "jessica_anderson.jpg",
        Gender = "Female",
        DOB = new DateTime(1973, 9, 30),
        Rating = 4.3,
        //AddressId = 7 // You need to provide a valid AddressId here
        Address=addresses[6]
    }

};

            moreVets.ForEach(v => context.Vets.AddOrUpdate(v));
        }
    }
}
