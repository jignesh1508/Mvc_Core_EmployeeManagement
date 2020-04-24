using MVC_EmployeeManagement.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_EmployeeManagement.Interface
{
    public interface IRepository<T> where T:BaseEntity
    {

        T Add(T entity);
        void Update(T entity);
        void Delete(T entitiy);

        T GetById(Guid id);
        IEnumerable<T> ListAll();
    }
}
