

// 08/30/2021 03:14 pm - SSN - [20210830-1352] - [002] - M05-06 - Demo: Unit testing with a container


using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PeopleViewer.SharedObjects;
using PersonRepository.Service.MyService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PersonRepository.Service.Tests
{

    [TestClass]
    public class UnityServiceRepositoryTest
    {

        // IPersonService _service;
        IUnityContainer Container;

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
            svcMock.Setup(r => r.GetPerson(It.IsAny<string>())).Returns((string n) => people.FirstOrDefault(p => p.LastName == n));

            // _service = svcMock.Object;
            Container = new UnityContainer();
            Container.RegisterInstance<IPersonService>(svcMock.Object);
            Container.RegisterType<ServiceRepository>(new InjectionProperty("ServiceProxy"));
        }


        [TestMethod]
        [TestCategory("Unity")]
        public void UnityGetPeople_OnExecute_ReturnsPeople()
        {

            // Arrange

            // var repo = new ServiceRepository();
            var repo = Container.Resolve<ServiceRepository>();

            // repo.ServiceProxy = _service;
            // No longer needed.


            // Act

            var output = repo.GetPeople();


            // Assert

            Assert.IsNotNull(output);
            Assert.AreEqual(numberOfTestRecords, output.Count());
        }


        [TestMethod]
        [TestCategory("Unity")]
        public void UnityGetPerson_OnExecuteWithValidValue_ReturnsPerson()
        {


            // Arrange

            //var repo = new ServiceRepository();
            var repo = Container.Resolve<ServiceRepository>();

            // repo.ServiceProxy = _service;
            // No longer needed.


            string expectedLastName = "Smith";

            // Act

            var output = repo.GetPerson(expectedLastName);


            // Assert

            Assert.IsNotNull(output);
            Assert.AreEqual(expectedLastName, output.LastName);


        }



        [TestMethod]
        [TestCategory("Unity")]
        public void UnityGetPerson_OnExecuteWithInvalidValue_ReturnsNull()
        {


            // Arrange

            // var repo = new ServiceRepository();
            var repo = Container.Resolve<ServiceRepository>();

            // repo.ServiceProxy = _service;
            // No longer needed.


            // Act

            var output = repo.GetPerson("InvalidLastName");


            // Assert

            Assert.IsNull(output);


        }



    }
}
