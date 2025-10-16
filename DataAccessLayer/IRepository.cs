using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DataAccessLayer
{
    public interface IRepository<T> where T : IDomainObject
    {
        void Add(T entity);
        void Delete(string id);
        List<T> ReadAll();
        T ReadById(string id);
        void Update(T entity);
    }
}
