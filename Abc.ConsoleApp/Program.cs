using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Abc.Entity;
using Abc.BusinessLayer;

namespace Abc.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started");

            CodeTest();

            Console.WriteLine("Press Enter");
            Console.ReadLine();
        }

        private static void CodeTest()
        {
            PersonManager mgr = new PersonManager();
            // Add a new person.
            //var person = new Person { FirstName = "Billy", LastName = "Tuttle" };
            //Person newPerson = mgr.Add(person);
            //long id = newPerson.Id;

            List<Person> list1 = mgr.List();

            Console.WriteLine("List One");
            foreach (var item in list1)
            {
                Console.WriteLine("Person: " + item.Id + " " + item.FirstName + " " + item.LastName);
            }

            InfoInquiryManager infoMgr = new InfoInquiryManager();

            // Add a new InfoInquiry.
            var info = new InfoInquiry { Email = "suzy@tuttle.com" };
            InfoInquiry newInfo = infoMgr.Add(info);
            long id = newInfo.Id;

            List<InfoInquiry> infoList = infoMgr.List();

            foreach (var item in infoList)
            {
                Console.WriteLine("Email: " + item.Id + " " + item.Email);
            }

        }
    }
}
