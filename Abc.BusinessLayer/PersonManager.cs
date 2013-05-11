using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Abc.Entity;
using Abc.DataLayer;

namespace Abc.BusinessLayer
{
    public class PersonManager
    {
        private IRepository _db;

        public PersonManager()
        {
            _db = Factory.Current.Repository;
        }

        public Person Add(Person person)
        {
            return _db.Add(person);
        }

        public List<Person> List()
        {
            return _db.List<Person>().ToList();
        }
    }
}
