using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PeopleViewer.SharedObjects;
using PersonRepository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

// 08/30/2021 03:04 pm - SSN - [20210830-1352] - [001] - M05-06 - Demo: Unit testing with a container
// Copied from PeopleViewViewModelTest.cs

namespace PeopleViewer.Presentation.Tests
{

    [TestClass]
    public class UnityPeopleViewerViewModelTest
    {

        // IPersonRepository _repository;
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

            var repoMock = new Mock<IPersonRepository>();
            repoMock.Setup(r => r.GetPeople()).Returns(people);

            // _repository = repoMock.Object;
            Container = new UnityContainer();
            Container.RegisterInstance<IPersonRepository>(repoMock.Object);

        }

        [TestMethod]
        [TestCategory("Unity")]
        public void UnityPeople_OnRefreshCommand_IsPopulated()
        {

            // Arrange

            // var vm = new PeopleViewerViewModel(_repository);
            var vm = Container.Resolve<PeopleViewerViewModel>();

            // Act

            vm.RefreshPeopleCommand.Execute(null);


            // Assert

            Assert.IsNotNull(vm.People);
            Assert.AreEqual(numberOfTestRecords, vm.People.Count());

        }


        [TestMethod]
        [TestCategory("Unity")]
        public void UnityPeople_OnClearCommand_IsEmpty()
        {


            // Arrange

            // var vm = new PeopleViewerViewModel(_repository);
            var vm = Container.Resolve<PeopleViewerViewModel>();


            vm.RefreshPeopleCommand.Execute(null);
            Assert.AreEqual(numberOfTestRecords, vm.People.Count(), "Invalid test arrangement setup (20210830-1002)");

            // Act

            vm.ClearPeopleCommand.Execute(null);

            // Assert

            Assert.AreEqual(0, vm.People.Count());

        }


    }
}
