using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using RocketSite.Common.Interfaces;
using RocketSite.Common.Models;
using RocketSite.Common.Options;
using RocketSite.Common.Responses;

namespace RocketSite.Common.Repositories
{
    public class RocketSiteRepository : IRocketSiteRepository
    {
        private readonly string _connectionString;
        public RocketSiteRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }
        public List<SpaceMission> Get1(string name, string type, string startDate, string endDate)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query(
                    "SELECT cr.name as crname, cr.type as crtype, cr.weight as crweight, cr.weight as crweight, " +
                    "cr.emaunt as cremaunt, cr.spaceMissionName, cr.customerName, cr.customerCountry, " +
                    "sm.*, ro.name as roname, ro.version as roversion, ro.weight as roweight, ro.height as roheight, ro.diameter as rodiameter, " +
                    "ro.cost as rocost, ro.stages as rostages, ro.massToLEO as romassToLEO, ro.massToGTO as romassToGTO, ro.engineType as roengineType, " +
                    "co.name as coname, co.timezone as cotimezone, lo.latitude as lolatitude, lo.longitude as lolongitude, " +
                    "lo.country as locountry, lo.city as locity " +
                    "FROM Cargo as cr " +
                    "INNER JOIN SpaceMission as sm ON cr.spaceMissionName = sm.name " +
                    "INNER JOIN Rocket as ro ON sm.rocketName = ro.name " +
                    "AND sm.rocketVersion = ro.version " +
                    "INNER JOIN Cosmodrome as co ON sm.cosmodromeName = co.name " +
                    "INNER JOIN Location as lo ON co.latitude = lo.latitude " +
                    "AND co.longitude = lo.longitude " +
                    "WHERE cr.name = @Name AND cr.type = @Type",
                    param: new { Name = name, Type = Enum.Parse<CargoOption>(type), StartDate = startDate, EndDate = endDate });

                return (from item in itemList
                        let rocket = new Rocket 
                        { 
                            Name = item.roname, Version = item.roversion, Weight = item.roweight, Height = item.roheight, 
                            Diameter = item.rodiameter, Cost = item.rocost, Stages = item.rostages, MassToLEO = item.romassToLEO, 
                            MassToGTO = item.romassToGTO, EngineType = item.roengineType 
                        }
                        let location = new Location { City = item.locity, Country = item.locountry, Latitude = item.lolatitude, Longitude = item.lolongitude }
                        let cosmodrome = new Cosmodrome { Name = item.coname, Timezone = item.cotimezone, Location = location }
                        let cargo = new Cargo { Name = item.crname, Type = Enum.Parse<CargoOption>(item.crtype), Weight = item.crweight, Emaunt = item.cremaunt }
                        select new SpaceMission
                        {
                            Name = item.name,
                            Status = Enum.Parse<StatusOption>(item.status),
                            Cost = item.cost,
                            Altitude = item.altitude,
                            StartDate = item.startDate,
                            EndDate = item.endDate,
                            Rocket = rocket,
                            Cosmodrome = cosmodrome,
                            Cargoes = new List<Cargo> { cargo }
                        }).ToList();
            }
        }

        public List<Response2> Get2(string param1, string param2)
        {
            throw new NotImplementedException();
        }

        public List<Response3> Get3(string param1, string param2)
        {
            throw new NotImplementedException();
        }

        public List<Response4> Get4(string param1, string param2)
        {
            throw new NotImplementedException();
        }
    }
}
