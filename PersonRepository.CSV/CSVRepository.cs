using PeopleViewer.SharedObjects;
using PersonRepository.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows;

namespace PersonRepository.CSV
{
    public class CSVRepository : IPersonRepository
    {
        string path;

        // 08/30/2021 06:50 am - SSN - [20210827-1701] - [008] - M03-03 - Demo: Additional repositories
        public CSVRepository(string fullCSVFileName)
        {
            // var filename = ConfigurationManager.AppSettings["CSVFileName"];
            // path = AppDomain.CurrentDomain.BaseDirectory + filename;
            path = ConfigurationManager.AppSettings[fullCSVFileName];
        }

        public IEnumerable<Person> GetPeople()
        {
            var people = new List<Person>();

            // 08/30/2021 06:51 am - SSN - [20210827-1701] - [009] - M03-03 - Demo: Additional repositories
            if (!File.Exists(path))
            {
                MessageBox.Show($"Requested file: [{path}], does not exist.", "Missing file", MessageBoxButton.OK);
            }
            else
            {
                using (var sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var elems = line.Split(',');
                        var per = new Person()
                        {
                            FirstName = elems[0],
                            LastName = elems[1],
                            StartDate = DateTime.Parse(elems[2]),
                            Rating = Int32.Parse(elems[3])
                        };
                        people.Add(per);
                    }
                }
            }
            return people;
        }

        public Person GetPerson(string lastName)
        {
            Person selPerson = new Person();
            if (File.Exists(path))
            {
                var sr = new StreamReader(path);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var elems = line.Split(',');
                    if (elems[1].ToLower() == lastName.ToLower())
                    {
                        selPerson.FirstName = elems[0];
                        selPerson.LastName = elems[1];
                        selPerson.StartDate = DateTime.Parse(elems[2]);
                        selPerson.Rating = Int32.Parse(elems[3]);
                    }
                }
            }

            return selPerson;
        }

        public void AddPerson(Person newPerson)
        {
            throw new NotImplementedException();
        }

        public void UpdatePerson(string lastName, Person updatedPerson)
        {
            throw new NotImplementedException();
        }

        public void DeletePerson(string lastName)
        {
            throw new NotImplementedException();
        }

        public void UpdatePeople(IEnumerable<Person> updatedPeople)
        {
            throw new NotImplementedException();
        }
    }
}
