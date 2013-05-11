using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Abc.DataLayer;
using Abc.Entity;

namespace Abc.BusinessLayer
{
    public class InfoInquiryManager
    {
        private IRepository _db;

        public InfoInquiryManager()
            : this(false)
        {
        }

        public InfoInquiryManager(bool initializeData)
        {
            _db = Factory.Current.Repository;
            if (initializeData)
            { _db.Initialize(); }
        }

        public InfoInquiry Add(InfoInquiry infoInquiry)
        {
            return _db.Add(infoInquiry);
        }

        public List<InfoInquiry> List()
        {
            return _db.List<InfoInquiry>().ToList();
        }
    }

}
