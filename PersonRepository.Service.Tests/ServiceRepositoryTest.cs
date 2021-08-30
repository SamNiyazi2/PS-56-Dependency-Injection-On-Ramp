using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PeopleViewer.SharedObjects;
using PersonRepository.Service.MyService;

// 08/30/2021 10:16 am - SSN - [20210830-1006] - [002] - M04-04 - Demo: Property injection and unit testing

namespace PersonRepository.Service.Tests
{
    [TestClass]
    public class ServiceRepositoryTest
    {

        IPersonService _service;
         
        int numberOfTestRecords;


        [TestInitialize]
        public void Setup()
        {
            var people = new List<Person>()
            {

                new Person()
                {
                    FirstName = "John",
                    LastName = "Smith",
                    StartDate = new DateTime(2000, 10, 1),
                    Rating = 7
                },
                new Person()
                {
                    FirstName = "Mary",
                    LastName = "Thomas",
                    StartDate = new DateTime(1971,7,23),
                    Rating = 9
                }
            };

            numberOfTestRecords = people.Count();

            var svcMock = new Mock<IPersonService>();
            svcMock.Setup(r => r.GetPeople()).Returns(people);
            svcMock.Setup(r => r.GetPerson(It.IsAny<string>())).Returns((string n)=>people.FirstOrDefault(p=>p.LastName==n));

            _service= svcMock.Object;

        }


        [TestMethod]
        public void GetPeople_OnExecute_ReturnsPeople()
        {

            // Arrange

            var repo = new ServiceRepository();
            repo.ServiceProxy = _service;


            // Act

            var output = repo.GetPeople();


            // Assert

            Assert.IsNotNull(output);
            Assert.AreEqual(numberOfTestRecords, output.Count());
        }

        [TestMethod]
        public void GetPerson_OnExecuteWithValidValue_ReturnsPerson()
        {


            // Arrange

            var repo = new ServiceRepository();
            repo.ServiceProxy = _service;
            string expectedLastName = "Smith";

            // Act

            var output = repo.GetPerson(expectedLastName);


            // Assert

            Assert.IsNotNull(output);
            Assert.AreEqual(expectedLastName, output.LastName);


        }



        [TestMethod]
        public void GetPerson_OnExecuteWithInvalidValue_ReturnsNull()
        {


            // Arrange

            var repo = new ServiceRepository();
            repo.ServiceProxy = _service;

            // Act

            var output = repo.GetPerson("InvalidLastName");


            // Assert

            Assert.IsNull(output);


        }



    }
}
