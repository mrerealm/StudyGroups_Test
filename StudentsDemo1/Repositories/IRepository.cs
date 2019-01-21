using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsDemo1.Repositories
{
    public interface IRepository<T> where T: class
    {
        IEnumerable<T> Get();
        T Get(int id);
        T Add(T instance);
        void Add(IEnumerable<T> instance);
    }
}
