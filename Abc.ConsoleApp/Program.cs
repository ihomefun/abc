using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Abc.Entity;

namespace Abc.ConsoleApp
{

    public class EfDatabaseContext : DbContext
    {
        public DbSet<InfoInquiry> InfoInquiries { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Started");

            using (var db = new EfDatabaseContext())
            {
                db.InfoInquiries.Add(new InfoInquiry { Email="Dude@wow.com"});
                db.SaveChanges();
            }

            Console.ReadLine();
        }
    }
}
