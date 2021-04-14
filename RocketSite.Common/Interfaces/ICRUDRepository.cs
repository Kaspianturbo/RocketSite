using RocketSite.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketSite.Common.Interfaces
{
    public interface ICRUDRepository<T>
    {
        void Create(T @object);
        void Delete(int id);
        T Get(string name);
        List<T> GetObjects();
        void Update(T @object);
    }
}
