using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Abc.Entity;

namespace Abc.DataLayer
{
    public interface IRepository
    {
        T Add<T>(T t) where T : EntityBase;
        IQueryable<T> List<T>() where T : EntityBase;
        void Initialize();
    }
}
