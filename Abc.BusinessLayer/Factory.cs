using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

using Abc.DataLayer;

namespace Abc.BusinessLayer
{
   internal class Factory
   {
      private static volatile Factory _factory;
      private static readonly object _syncRoot = new Object();

      public static Factory Current
      {
         get
         {
            if (_factory == null)
            {
               lock (_syncRoot)
               {
                  if (_factory == null)
                     _factory = new Factory();
               }
            }
            return _factory;
         }
      }

      private readonly IRepository _repository;

      //public Factory()
      //{
      //   _repository = new AppRepositoryEf();
      //   // _repository = new AppRepositoryMemory();
      //}

      private Factory()
      {
         bool useRamDb = false;
         bool.TryParse( ConfigurationManager.AppSettings["UseRamRepository"], out useRamDb);

         if (useRamDb)
         {
            _repository = new RepositoryMemory();
         }
         else
         {
            _repository = new RepositoryEntityFramework();
         }
      }

      public IRepository Repository
      {
         get { return _repository; }
      }
   }
}
