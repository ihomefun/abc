using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Abc.Entity;

namespace Abc.DataLayer.Repositories
{
    public interface IRepository
    {
        IList<InfoInquiry> ListInfoInquiry(string sortExpression, int maxRows, int startRow, out int totalRowCount);
        InfoInquiry Add(InfoInquiry infoInquiry);
    }
}
