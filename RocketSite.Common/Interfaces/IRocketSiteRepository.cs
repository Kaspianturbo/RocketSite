using RocketSite.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RocketSite.Common.Options;

namespace RocketSite.Common.Interfaces
{
    public interface IRocketSiteRepository
    {
        List<SpaceMission> Get1(string param1, string param2, string param3, string param4);
        List<Rocket> Get2(int param1, string param2, string param3);
        List<Cosmodrome> Get3(string param1, string param2);
        List<Employee> Get4(string param1, string param2, string param3);
        List<TrainingProgram> Get5(string param1, string param2);
        List<Cargo> Get6(string param1, string param2);
        List<Equipment> Get7(string param1, string param2);
        List<Resources> Get8(string param1, string param2, string param3);
        List<Customer> Get9(string param1, string param2);
        List<Purchase> Get10(string param1);
    }
}
