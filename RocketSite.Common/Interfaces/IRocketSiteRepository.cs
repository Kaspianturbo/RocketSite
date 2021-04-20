using RocketSite.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RocketSite.Common.Responses;

namespace RocketSite.Common.Interfaces
{
    public interface IRocketSiteRepository
    {
        List<SpaceMission> Get1(string param1, string param2, string param3, string param4);
        List<Response2> Get2(string param1, string param2);
        List<Response3> Get3(string param1, string param2);
        List<Response4> Get4(string param1, string param2);
    }
}
