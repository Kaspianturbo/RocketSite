using RocketSite.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketSite.Common.Interfaces
{
    public interface IRocketSiteRepository
    {
        void Create(T @object);
        void Delete(T @object);
        T Get(T @object);
        List<T> GetObjects();
        void Update(T @object, Key key);
    }
}
