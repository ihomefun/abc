﻿using System;
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

        private Factory()
        {
            string repositoryType = ConfigurationManager.AppSettings["RepositoryType"];

            if (string.IsNullOrEmpty(repositoryType))
            {
                repositoryType = "EntityFramework";
            }

            switch (repositoryType.ToLower())
            {
                case "ram":
                case "memory":
                    _repository = new RepositoryMemory();
                    break;
                case "dynamodb":
                    _repository = new RepositoryDynamoDb();
                    break;
                default:
                    _repository = new RepositoryEntityFramework();
                    break;
            }
        }

        public IRepository Repository
        {
            get { return _repository; }
        }
    }
}
