using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PeopleViewer.SharedObjects;
using PersonRepository.Interface;

// 08/30/2021 09:44 am - SSN - [20210830-0944] - [001] -  M04-03 - Demo: Unit testing 

namespace PeopleViewer.Presentation.Tests
{
    [TestClass]
    public class PeopleViewerViewModelTest
    {
        IPersonRepository _repository;
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

            _repository = repoMock.Object;

        }

        [TestMethod]
        public void People_OnRefreshCommand_IsPopulated()
        {

            // Arrange

            var vm = new PeopleViewerViewModel(_repository);


            // Act

            vm.RefreshPeopleCommand.Execute(null);


            // Assert

            Assert.IsNotNull(vm.People);
            Assert.AreEqual(numberOfTestRecords, vm.People.Count());

        }


        [TestMethod]
        public void People_OnClearCommand_IsEmpty()
        {


            // Arrange

            var vm = new PeopleViewerViewModel(_repository);
            vm.RefreshPeopleCommand.Execute(null);
            Assert.AreEqual(numberOfTestRecords, vm.People.Count(), "Invalid test arrangement setup (20210830-1002)");

            // Act

            vm.ClearPeopleCommand.Execute(null);

            // Assert

            Assert.AreEqual(0, vm.People.Count());

        }


    }
}
