using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Abc.Entity;
using Abc.BusinessLayer;

namespace Abc.BusinessLayer.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void ListPersonCountTest()
        {
            PersonManager mgr = new PersonManager(true);
            List<Person> list = mgr.List();

            System.Diagnostics.Debug.Print("---------");
            foreach (Person person in list)
            {
                System.Diagnostics.Debug.Print("One: " + person.Id + " " + person.FirstName + " " + person.LastName);
            }
            Assert.IsTrue(list.Count > 0);

        }
        [TestMethod]
        public void AddPersonTest()
        {
            Person one = new Person { FirstName = "Tom", LastName = "Tuttle" };
            PersonManager mgr = new PersonManager(true);

            List<Person> list = mgr.List();
            int priorCount = list.Count;

            foreach (Person person in list)
            {
                System.Diagnostics.Debug.Print("Two: " + person.Id + " " + person.FirstName + " " + person.LastName);
            }

            Person newOne = mgr.Add(one);

            Assert.IsTrue(newOne.Id > 0, "Id on new object was not set");
            Assert.IsTrue(mgr.List().Count == priorCount + 1, "Failed to add new object to collection");
        }

    }
}
