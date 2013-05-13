﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using Amazon.SecurityToken;

using AppCommon;

namespace Abc.DataLayer
{
    public class RepositoryDynamoDb : IRepository
    {
        private static AmazonDynamoDBClient client;

        public RepositoryDynamoDb()
        {
            try
            {
                var config = new AmazonDynamoDBConfig();
                config.ServiceURL = ConfigurationManager.AppSettings["ServiceURL"];

                client = new AmazonDynamoDBClient(config);

            }
            catch (AmazonDynamoDBException e) { Console.WriteLine(e.Message); }
            catch (AmazonServiceException e) { Console.WriteLine(e.Message); }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        public T Add<T>(T t) where T : Entity.EntityBase
        {
            T retVal = t;

            try
            {
                Table table = Table.LoadTable(client, "Play");
                // ********** Add Books *********************
                var item = new Document();
                item["Id"] = "101";
                item["DateAdded"] = Conversion.DateTimeIso8601FormatToString(DateTime.Now);
                item["SomeSetting"] = "SomeValue";
                table.PutItem(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }


            return retVal;
        }

        public IQueryable<T> List<T>() where T : Entity.EntityBase
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            // Nothing to do here.  This method only works on memory databases.
        }
    }
}
