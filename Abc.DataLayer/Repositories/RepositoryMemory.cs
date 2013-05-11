using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;

using Abc.Entity;

namespace Abc.DataLayer
{
   public class RepositoryMemory : IRepository
   {
      private IList<Person> _people;
      private IList<InfoInquiry> _infoInquiries;

      public RepositoryMemory()
      {
          Initialize();
      }

      public void Initialize()
      {
          var random = new RandomData();

          _people = new List<Person>();
          for (int iLoop = 0; iLoop < random.Int(3, 10); iLoop++)
          {
              _people.Add(new Person
              {
                  Id = iLoop + 1,
                  FirstName = random.Word(),
                  LastName = random.Word()
              });
          }

          _infoInquiries = new List<InfoInquiry>();
          for (int iLoop = 0; iLoop < random.Int(3, 10); iLoop++)
          {
              _infoInquiries.Add(new InfoInquiry
              {
                  Id = iLoop + 1,
                  Email = random.Word() + "@" + random.Word() + ".com"
              });
          }
      }

      public IQueryable<T> List<T>() where T : EntityBase
      {
         if (typeof(T).Name == "Person")
         {
            return ((IEnumerable<T>)_people).Select(x => x).AsQueryable();
         }
         if (typeof(T).Name == "InfoInquiry")
         {
            return ((IEnumerable<T>)_infoInquiries).Select(x => x).AsQueryable();
         }
         throw new NotImplementedException(typeof(T).Name + " for List method not implemented in RepositoryMemory");
      }

      public T Add<T>(T t) where T : EntityBase
      {
         T retVal = t;

         if (typeof(T).Name == "Person")
         {
            t.Id = _people.Count + 1;
            Person p = t as Person;
            _people.Add(p);
            retVal = t;
         }
         else if (typeof(T).Name == "InfoInquiry")
         {
            t.Id = _infoInquiries.Count + 1;
            InfoInquiry p = t as InfoInquiry;
            _infoInquiries.Add(p);
            retVal = t;
         }
         else
         {
            throw new NotImplementedException(typeof(T).Name + " for Add method not implemented in RepositoryMemory");
         }
         return retVal;
      }
   
   }
}
