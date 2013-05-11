using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abc.Entity
{
    public class InfoInquiry : EntityBase
    {
        public virtual string Email { get; set; }
        public virtual DateTime DateAddedUtc { get; set; }

        public InfoInquiry()
        {
            DateAddedUtc = DateTime.UtcNow;
        }
    }
}
