using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PetzyVet.API.Controllers;
using PetzyVet.Domain.Entities;
using PetzyVet.Domain.DTO;
using PetzyVet.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Results;
using System.Web.Http;
using System.Net;
using Microsoft.AspNet.OData;
using System.IO;
using System.Web;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace PetzyVet.API.Tests.Controllers
{
    [TestClass]
    public class VetsControllerTests
    {
        
        [TestMethod]
        public void GetAllVets_Returns_Ok_With_VetCards()
        {
            // Arrange
            var vets = new List<Vet>
    {
        new Vet { /* Vet properties */ },
        new Vet { /* Vet properties */ }
    };

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetAllVets()).Returns(vets);

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetAllVets();
            var contentResult = actionResult as OkNegotiatedContentResult<List<VetCardDTO>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.Count);
            // Additional assertions as needed
        }

        [TestMethod]
        public void GetAllVets_Returns_NotFound_When_Vets_Empty()
        {
            // Arrange
            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetAllVets()).Returns(new List<Vet>());

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetAllVets();

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetAllVets_Returns_NotFound_When_Vets_Null()
        {
            // Arrange
            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetAllVets()).Returns((List<Vet>)null);

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetAllVets();

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetAllVets_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetAllVets()).Throws<Exception>();

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetAllVets();

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }

        [TestMethod]
        public void GetVetById_Returns_Ok_With_VetProfileDTO()
        {
            // Arrange
            int vetId = 1;
            var vet = new Vet
            {
                VetId = vetId,
                /* Vet properties */
            };

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetById(vetId)).Returns(vet);

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetVetById(vetId);
            var contentResult = actionResult as OkNegotiatedContentResult<VetProfileDTO>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            // Additional assertions as needed
        }

        [TestMethod]
        public void GetVetById_Returns_NotFound_When_Vet_Not_Found()
        {
            // Arrange
            int vetId = 1;

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetById(vetId)).Returns((Vet)null);

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetVetById(vetId);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetVetById_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            int vetId = 1;

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetById(vetId)).Throws<Exception>();

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetVetById(vetId);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }

        [TestMethod]
        public void AddVet_Returns_Ok_When_Vet_Not_Null()
        {
            // Arrange
            var vet = new Vet
            {
                /* Initialize Vet properties */
            };

            var mockRepository = new Mock<IVetRepository>();

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.AddVet(vet);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void AddVet_Returns_BadRequest_When_Vet_Null()
        {
            // Arrange
            Vet vet = null;

            var mockRepository = new Mock<IVetRepository>();

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.AddVet(vet);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        [TestMethod]
        public void AddVet_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            var vet = new Vet
            {
                /* Initialize Vet properties */
            };

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.AddVet(vet)).Throws<Exception>();

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.AddVet(vet);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }

        [TestMethod]
        public void DeleteVet_Returns_Ok_When_Vet_Deleted_Successfully()
        {
            // Arrange
            int vetId = 1;

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetById(vetId)).Returns(new Vet { VetId = vetId });

            var controller = new VetsController { vetRepository = mockRepository.Object };

            // Act
            IHttpActionResult actionResult = controller.DeleteVet(vetId);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void DeleteVet_Returns_NotFound_For_NonExisting_Vet_ID()
        {
            // Arrange
            int vetId = 999; // Non-existing vet ID

            var mockRepository = new Mock<IVetRepository>();
            var controller = new VetsController { vetRepository = mockRepository.Object };

            // Act
            IHttpActionResult actionResult = controller.DeleteVet(vetId);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteVet_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            int vetId = 1;

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.DeleteVet(vetId)).Throws<Exception>();

            var controller = new VetsController { vetRepository = mockRepository.Object };

            // Act
            IHttpActionResult actionResult = controller.DeleteVet(vetId);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }


        [TestMethod]
        public void EditStatus_Returns_Ok_When_Status_Edited_Successfully()
        {
            // Arrange
            int vetId = 1;
            bool status = true;

            var mockRepository = new Mock<IVetRepository>();
            var controller = new VetsController { vetRepository = mockRepository.Object };

            // Act
            IHttpActionResult actionResult = controller.EditStatus(vetId, status);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        [TestMethod]
        public void EditStatus_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            int vetId = 1;
            bool status = true;

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.EditStatus(status, vetId)).Throws<Exception>();

            var controller = new VetsController { vetRepository = mockRepository.Object };

            // Act
            IHttpActionResult actionResult = controller.EditStatus(vetId, status);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }


        [TestMethod]
        public void GetVetsByListOfIds_Returns_BadRequest_When_DoctorIds_Null()
        {
            // Arrange
            List<int> doctorIds = null;
            var controller = new VetsController { vetRepository = Mock.Of<IVetRepository>() };

            // Act
            IHttpActionResult actionResult = controller.GetVetsByListOfIds(doctorIds);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }


        [TestMethod]
        public void GetVetsByListOfIds_Returns_BadRequest_When_DoctorIds_Empty()
        {
            // Arrange
            var doctorIds = new List<int>();
            var controller = new VetsController { vetRepository = Mock.Of<IVetRepository>() };

            // Act
            IHttpActionResult actionResult = controller.GetVetsByListOfIds(doctorIds);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
        }


        [TestMethod]
        public void GetVetsByListOfIds_Returns_Ok_With_VetDetails()
        {
            // Arrange
            var doctorIds = new List<int> { 1, 2 };

            var vet1 = new Vet { VetId = 1, FName = "John", LName = "Doe", Speciality = "Dentist", Photo = " " };
            var vet2 = new Vet { VetId = 2, FName = "Jane", LName = "Smith", Speciality = "Surgeon", Photo = " " };

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetById(It.IsAny<int>())).Returns<int>(id =>
            {
                switch (id)
                {
                    case 1:
                        return vet1;
                    case 2:
                        return vet2;
                    default:
                        return null;
                }
            });

            var controller = new VetsController { vetRepository = mockRepository.Object };

            // Act
            IHttpActionResult actionResult = controller.GetVetsByListOfIds(doctorIds);
            var contentResult = actionResult as OkNegotiatedContentResult<List<VetIdNameDTO>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(2, contentResult.Content.Count);
            Assert.AreEqual("John Doe", contentResult.Content[0].Name);
            Assert.AreEqual("Jane Smith", contentResult.Content[1].Name);
        }

        [TestMethod]
        public void GetVetsByListOfIds_ExceptionHandling()
        {
            // Arrange
            var doctorIds = new List<int> { 1, 2 };

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetById(It.IsAny<int>())).Throws<Exception>(); // Mocking exception when calling GetVetById

            var controller = new VetsController { vetRepository = mockRepository.Object };

            // Act
            IHttpActionResult actionResult = controller.GetVetsByListOfIds(doctorIds);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }

        [TestMethod]
        public void DeleteVet_ExceptionHandling()
        {
            // Arrange
            int vetId = 1;

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetById(vetId)).Throws<Exception>(); // Mocking exception when calling GetVetById
                                                                                //mockRepository.Setup(x => x.DeleteVet(vetId)).Throws<Exception>(); // Uncomment this line if you also want to test exception in DeleteVet

            var controller = new VetsController { vetRepository = mockRepository.Object };

            // Act
            IHttpActionResult actionResult = controller.DeleteVet(vetId);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }

        [TestMethod]
        public void UpdateVet_Returns_Ok_When_Vet_Updated_Successfully()
        {
            // Arrange
            int vetId = 1;
            var vet = new Vet { VetId = vetId, /* Initialize other Vet properties */ };

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetById(vetId)).Returns(vet);

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            var delta = new Delta<Vet>(typeof(Vet));
            // Set properties to update in delta
            delta.TrySetPropertyValue("PropertyName", "NewValue");

            // Act
            IHttpActionResult actionResult = controller.UpdateVet(vetId, delta);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

/*        [TestMethod]
        public void UpdateVet_Returns_BadRequest_For_Null_Vet_Object()
        {
            // Arrange
            int vetId = 1;
            Vet vet = null;

            var mockRepository = new Mock<IVetRepository>();
            var controller = new VetsController { vetRepository = mockRepository.Object };

            var delta = new Delta<Vet>(typeof(Vet)); // Create an empty Delta<Vet> object

            // Act
            IHttpActionResult actionResult = controller.UpdateVet(vetId, delta);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }*/

        [TestMethod]
        public void UpdateVet_Returns_NotFound_For_NonExisting_Vet_ID()
        {
            // Arrange
            int vetId = 999; // Non-existing vet ID
            var mockRepository = new Mock<IVetRepository>();
            var controller = new VetsController { vetRepository = mockRepository.Object };

            var delta = new Delta<Vet>(typeof(Vet));
            // Set properties to update in delta
            delta.TrySetPropertyValue("PropertyName", "NewValue");

            // Act
            IHttpActionResult actionResult = controller.UpdateVet(vetId, delta);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void UpdateVet_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            int vetId = 1;

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetById(vetId)).Throws<Exception>();

            var controller = new VetsController { vetRepository = mockRepository.Object };

            var delta = new Delta<Vet>(typeof(Vet));
            // Set properties to update in delta
            delta.TrySetPropertyValue("PropertyName", "NewValue");

            // Act
            IHttpActionResult actionResult = controller.UpdateVet(vetId, delta);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }


        [TestMethod]
        public void GetFullVetById_Returns_Ok_When_Vet_Exists()
        {
            // Arrange
            int vetId = 1;
            var vet = new Vet { VetId = vetId, /* Initialize other Vet properties */ };

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetById(vetId)).Returns(vet);

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetFullVetById(vetId);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Vet>));
            var contentResult = actionResult as OkNegotiatedContentResult<Vet>;
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(vet, contentResult.Content);
        }

        [TestMethod]
        public void GetFullVetById_Returns_NotFound_When_Vet_Not_Exists()
        {
            // Arrange
            int vetId = 999; // Non-existing vet ID
            var mockRepository = new Mock<IVetRepository>();
            var controller = new VetsController { vetRepository = mockRepository.Object };

            // Act
            IHttpActionResult actionResult = controller.GetFullVetById(vetId);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetFullVetById_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            int vetId = 1;
            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetById(vetId)).Throws<Exception>();

            var controller = new VetsController { vetRepository = mockRepository.Object };

            // Act
            IHttpActionResult actionResult = controller.GetFullVetById(vetId);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }

        [TestMethod]
        public void GetVetsandIds_Returns_Ok_With_Vets()
        {
            // Arrange
            var mockRepository = new Mock<IVetRepository>();
            List<VetDTO> vetDTOs = new List<VetDTO> { new VetDTO { VetId = 1, Name = "Vet1" }, new VetDTO { VetId = 2, Name = "Vet2" } };
            mockRepository.Setup(x => x.GetAllVetIdsAndNames()).Returns(vetDTOs);

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetVetsandIds();

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<List<VetDTO>>));
            var contentResult = actionResult as OkNegotiatedContentResult<List<VetDTO>>;
            Assert.IsNotNull(contentResult);
            CollectionAssert.AreEqual(vetDTOs, contentResult.Content);
        }

        [TestMethod]
        public void GetVetsandIds_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetAllVetIdsAndNames()).Throws<Exception>();

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetVetsandIds();

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }

        [TestMethod]
        public void UpdateRating_Returns_Ok_When_Rating_Updated_Successfully()
        {
            // Arrange
            int docId = 1;
            int rating = 5;

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.UpdateRating(docId, rating));

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.UpdateRating(docId, rating);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<string>));
            var contentResult = actionResult as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(contentResult);
            Assert.AreEqual("Rating Updated", contentResult.Content);
        }

        [TestMethod]
        public void UpdateRating_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            int docId = 1;
            int rating = 5;

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.UpdateRating(docId, rating)).Throws<Exception>();

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.UpdateRating(docId, rating);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }



        [TestMethod]
        public void GetTopRatedVets_Returns_Top_Vets_Successfully()
        {
            // Arrange
            var mockRepository = new Mock<IVetRepository>();
            var vets = new List<Vet>
            {
                new Vet { VetId = 6, FName = "Sarah", LName = "Jones", NPINumber = "13579", Phone = "9876543210", Speciality = "Internal Medicine", Photo = "photo6.jpg", Rating = 4.4 },
                new Vet { VetId = 7, FName = "Daniel", LName = "Garcia", NPINumber = "24680", Phone = "8765432109", Speciality = "Dermatology", Photo = "photo7.jpg", Rating = 4.7 },
                new Vet { VetId = 8, FName = "Rachel", LName = "Martinez", NPINumber = "97531", Phone = "7654321098", Speciality = "Ophthalmology", Photo = "photo8.jpg", Rating = 4.3 },
                new Vet { VetId = 9, FName = "Ryan", LName = "Anderson", NPINumber = "86420", Phone = "6543210987", Speciality = "Radiology", Photo = "photo9.jpg", Rating = 4.6 },
                new Vet { VetId = 10, FName = "Jessica", LName = "Taylor", NPINumber = "31415", Phone = "5432109876", Speciality = "Emergency Medicine", Photo = "photo10.jpg", Rating = 4.8 },
                new Vet { VetId = 11, FName = "Kevin", LName = "Clark", NPINumber = "27182", Phone = "4321098765", Speciality = "Anesthesiology", Photo = "photo11.jpg", Rating = 4.5 }

            };
            mockRepository.Setup(x => x.GetAllVets()).Returns(vets);

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetTopRatedVets();

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<List<VetCardDTO>>));
            var contentResult = actionResult as OkNegotiatedContentResult<List<VetCardDTO>>;
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(4, contentResult.Content.Count); // Assuming you're returning top 4 vets
            // Add more assertions as needed to verify the content of the returned list
        }

        [TestMethod]
        public void GetTopRatedVets_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetAllVets()).Throws<Exception>();

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetTopRatedVets();

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }

        [TestMethod]
        public void GetUniqueSpecialties_Returns_NotFound_When_No_Specialties()
        {
            // Arrange
            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetUniqueSpecialties()).Returns(new List<string>());

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetUniqueSpecialties();

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetUniqueSpecialties_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetUniqueSpecialties()).Throws<Exception>();

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetUniqueSpecialties();

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ExceptionResult));
        }

        [TestMethod]
        public void GetUniqueSpecialties_Returns_Specialties_List()
        {
            // Arrange
            var specialties = new List<string> { "Cardiology", "Dentistry", "Orthopedics" };
            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetUniqueSpecialties()).Returns(specialties);

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetUniqueSpecialties();
            var contentResult = actionResult as OkNegotiatedContentResult<List<string>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(specialties, contentResult.Content);
        }



        [TestMethod]
        public void GetVetsBySpecialty_Returns_BadRequest_When_No_Specialties_Provided()
        {
            // Arrange
            List<string> specialties = null; // No specialties provided

            var controller = new VetsController();

            // Act
            IHttpActionResult actionResult = controller.GetVetsBySpecialty(specialties);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
            BadRequestErrorMessageResult badRequestResult = actionResult as BadRequestErrorMessageResult;
            Assert.AreEqual("No specialties provided.", badRequestResult.Message);
        }

        [TestMethod]
        public void GetVetsBySpecialty_Returns_NotFound_When_No_Vets_Found()
        {
            // Arrange
            List<string> specialties = new List<string> { "Dermatology", "Oncology" }; // Non-existing specialties

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetsBySpecialty(specialties)).Returns(new List<Vet>());

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetVetsBySpecialty(specialties);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetVetsBySpecialty_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            List<string> specialties = new List<string> { "Cardiology", "Dentistry" }; // Existing specialties

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetsBySpecialty(specialties)).Throws<Exception>();

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetVetsBySpecialty(specialties);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }

        [TestMethod]
        public void GetVetsBySpecialty_Returns_VetCards_When_Vets_Found()
        {
            // Arrange
            List<string> specialties = new List<string> { "Cardiology", "Dentistry" }; // Existing specialties
            List<Vet> vets = new List<Vet>
    {
        new Vet { VetId = 1, FName = "John", LName = "Doe", NPINumber = "12345", Phone = "1234567890", Speciality = "Cardiology", Photo = "photo1.jpg", Rating = 4.5 },
        new Vet { VetId = 2, FName = "Jane", LName = "Smith", NPINumber = "54321", Phone = "0987654321", Speciality = "Dentistry", Photo = "photo2.jpg", Rating = 4.8 }
    };

            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetsBySpecialty(specialties)).Returns(vets);

            var controller = new VetsController
            {
                vetRepository = mockRepository.Object
            };

            // Act
            IHttpActionResult actionResult = controller.GetVetsBySpecialty(specialties);
            var contentResult = actionResult as OkNegotiatedContentResult<List<VetCardDTO>>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(vets.Count, contentResult.Content.Count);
            // Assert individual VetCardDTO properties if needed
        }


        //Added New 02/05/2024
        [TestMethod]
        public void GetVetByNpiNumber_Returns_Ok_When_Vet_Found()
        {
            // Arrange
            string npiNumber = "1234567890";
            var mockRepository = new Mock<IVetRepository>();
            var vet = new Vet { NPINumber = npiNumber, /* Other properties */ };
            mockRepository.Setup(x => x.GetVetByNpiNumber(npiNumber)).Returns(vet);
            var controller = new VetsController();
            controller.vetRepository = mockRepository.Object; // Inject the mock repository

            // Act
            IHttpActionResult actionResult = controller.GetVetByNpiNumber(npiNumber);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<Vet>));
        }

        [TestMethod]
        public void GetVetByNpiNumber_Returns_NotFound_When_Vet_Not_Found()
        {
            // Arrange
            string npiNumber = "1234567890";
            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetByNpiNumber(npiNumber)).Returns((Vet)null);
            var controller = new VetsController();
            controller.vetRepository = mockRepository.Object; // Inject the mock repository

            // Act
            IHttpActionResult actionResult = controller.GetVetByNpiNumber(npiNumber);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void GetVetByNpiNumber_Returns_InternalServerError_On_Exception()
        {
            // Arrange
            string npiNumber = "1234567890";
            var mockRepository = new Mock<IVetRepository>();
            mockRepository.Setup(x => x.GetVetByNpiNumber(npiNumber)).Throws<Exception>();
            var controller = new VetsController();
            controller.vetRepository = mockRepository.Object; // Inject the mock repository

            // Act
            IHttpActionResult actionResult = controller.GetVetByNpiNumber(npiNumber);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }

        [TestMethod]
        public void CheckNpiNumber_ValidNpi_ReturnsOk()
        {
            // Arrange
            string validNpi = "9287402030";
            var mockVetRepository = new Mock<IVetRepository>();
            mockVetRepository.Setup(repo => repo.CheckNpiNumber(validNpi)).Returns(true);
            var controller = new VetsController();
            controller.vetRepository = mockVetRepository.Object;

            // Act
            IHttpActionResult actionResult = controller.CheckNpiNumber(validNpi);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkNegotiatedContentResult<bool>));
            var contentResult = actionResult as OkNegotiatedContentResult<bool>;
            Assert.IsTrue(contentResult.Content);
        }

        [TestMethod]
        public void CheckNpiNumber_InvalidNpi_ReturnsBadRequest()
        {
            // Arrange
            string invalidNpi = "as382389r";
            var mockVetRepository = new Mock<IVetRepository>();
            mockVetRepository.Setup(repo => repo.CheckNpiNumber(invalidNpi)).Returns(false);
            var controller = new VetsController();
            controller.vetRepository = mockVetRepository.Object;

            // Act
            IHttpActionResult actionResult = controller.CheckNpiNumber(invalidNpi);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestErrorMessageResult));
            var contentResult = actionResult as BadRequestErrorMessageResult;
            Assert.AreEqual("Receptionist hasn't added you yet", contentResult.Message);
        }

        [TestMethod]
        public void CheckNpiNumber_ExceptionThrown_ReturnsInternalServerError()
        {
            // Arrange
            string npi = "123425";
            var mockVetRepository = new Mock<IVetRepository>();
            mockVetRepository.Setup(repo => repo.CheckNpiNumber(npi)).Throws(new Exception("Simulated exception"));
            var controller = new VetsController();
            controller.vetRepository = mockVetRepository.Object;

            // Act
            IHttpActionResult actionResult = controller.CheckNpiNumber(npi);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(InternalServerErrorResult));
        }


    }
}
