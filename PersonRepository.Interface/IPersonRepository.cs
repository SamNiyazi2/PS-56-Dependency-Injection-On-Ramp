using PeopleViewer.SharedObjects;
using System.Collections.Generic;

// 08/27/2021 03:09 pm - SSN - [20210827-1500] - [001] - M02-03 - Demo: Adding the repository interface

namespace PersonRepository.Interface
{
    public interface IPersonRepository
    {

        IEnumerable<Person> GetPeople();

        Person GetPerson(string lastName);

        void AddPerson(Person newPerson);

        void UpdatePerson(string lastName, Person updatedPerson);

        void DeletePerson(string lastName);

        void UpdatePeople(IEnumerable<Person> updatedPeople);

    }
}
