using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Abc.WebRole.Models
{
    public class InfoRequest
    {
        public string Email { get; set; }
        public DateTime DateAddedUtc { get; set; }
    }
}