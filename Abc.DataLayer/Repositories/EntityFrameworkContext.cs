using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

using Abc.Entity;

namespace Abc.DataLayer
{
    public class EntityFrameworkContext : DbContext
    {
        public EntityFrameworkContext()
        {
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<InfoInquiry> InfoInquiries { get; set; }
    }
}
