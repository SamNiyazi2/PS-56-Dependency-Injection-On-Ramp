using PeopleViewer.SharedObjects;
using PersonRepository.Interface;
using PersonRepository.Service.MyService;
using System.Collections.Generic;
using System.Linq;

namespace PersonRepository.Service
{

    // 08/27/2021 03:14 pm - SSN - [20210827-1500] - [002] - M02-03 - Demo: Adding the repository interface
    // public class ServiceRepository
    public class ServiceRepository : IPersonRepository
    {

        // 08/30/2021 10:09 am - SSN - [20210830-1006] - [001] - M04-04 - Demo: Property injection and unit testing
        // PersonServiceClient ServiceProxy = new PersonServiceClient();

        private IPersonService _serviceProxy;

        public IPersonService ServiceProxy
        {
            get {

                if (_serviceProxy == null) _serviceProxy = new PersonServiceClient();
                return _serviceProxy;

            }

            set { _serviceProxy = value; }

        }


        public IEnumerable<Person> GetPeople()
        {
            return ServiceProxy.GetPeople();
        }

        public Person GetPerson(string lastName)
        {
            return ServiceProxy.GetPerson(lastName);
        }

        public void AddPerson(Person newPerson)
        {
            ServiceProxy.AddPerson(newPerson);
        }

        public void UpdatePerson(string lastName, Person updatedPerson)
        {
            ServiceProxy.UpdatePerson(lastName, updatedPerson);
        }

        public void DeletePerson(string lastName)
        {
            ServiceProxy.DeletePerson(lastName);
        }

        public void UpdatePeople(IEnumerable<Person> updatedPeople)
        {
            ServiceProxy.UpdatePeople(updatedPeople.ToList());
        }
    }
}
