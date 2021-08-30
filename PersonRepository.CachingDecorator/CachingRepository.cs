using PeopleViewer.SharedObjects;
using PersonRepository.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace PersonRepository.CachingDecorator
{
    public class CachingRepository : IPersonRepository
    {
        private TimeSpan _cacheDuration = TimeSpan.FromSeconds(30);
        private DateTime _dataDateTime;
        private IPersonRepository _personRepository;
        private IEnumerable<Person> _cachedItems;


        // 08/30/2021 07:55 am - SSN - [20210827-1701] - [010] - M03-03 - Demo: Additional repositories
        public CachingRepository()
        {
            string _defaultCacheDuration_sec = ConfigurationManager.AppSettings["defaultCacheDuration"];

            if (int.TryParse(_defaultCacheDuration_sec, out int _cacheDuration_sec))
            {
                _cacheDuration = TimeSpan.FromSeconds(_cacheDuration_sec);
            }
        }


        private bool IsCacheValid
        {
            get
            {
                var _cacheAge = DateTimeOffset.Now - _dataDateTime;
                return _cacheAge < _cacheDuration;
            }
        }

        private void ValidateCache()
        {
            if (_cachedItems == null || !IsCacheValid)
            {
                try
                {
                    _cachedItems = _personRepository.GetPeople();
                    _dataDateTime = DateTime.Now;
                }
                catch
                {
                    _cachedItems = new List<Person>()
                    {
                        new Person(){ FirstName="No Data Available", LastName = string.Empty, Rating = 0, StartDate = DateTime.Today},
                    };
                }
            }
        }

        private void InvalidateCache()
        {
            _cachedItems = null;
        }

        // 08/30/2021 09:07 am - SSN - [20210830-0800] - [002] - M03-05 - Demo: Using the caching repository
        // public CachingRepository(IPersonRepository personRepository) 
        public CachingRepository(IPersonRepository personRepository) : this()
        {
            _personRepository = personRepository;
        }

        public IEnumerable<Person> GetPeople()
        {
            ValidateCache();
            return _cachedItems;
        }

        public Person GetPerson(string lastName)
        {
            ValidateCache();
            return _cachedItems.FirstOrDefault(p => p.LastName == lastName);
        }

        public void AddPerson(Person newPerson)
        {
            _personRepository.AddPerson(newPerson);
            InvalidateCache();
        }

        public void UpdatePerson(string lastName, Person updatedPerson)
        {
            _personRepository.UpdatePerson(lastName, updatedPerson);
            InvalidateCache();
        }

        public void DeletePerson(string lastName)
        {
            _personRepository.DeletePerson(lastName);
            InvalidateCache();
        }

        public void UpdatePeople(IEnumerable<Person> updatedPeople)
        {
            _personRepository.UpdatePeople(updatedPeople);
            InvalidateCache();
        }
    }
}
