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
        public Response1 Get1(string param1, string param2, string param3, string param4)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Response1>(
                    "SELECT s.name as missionName, s.cost as missionCost, s.altitude, s.startDate, s.endDate, s.rocketName, c.name as customerName, r.name as rocketName FROM SpaceMission as s " +
                    "JOIN Customer as c ON s.name = c.spaceMissionName " +
                    "JOIN Rocket as r ON s.rocketName = r.name AND s.rocketVersion = r.version " +
                    "WHERE s.startDate = @StartDate " +
                    "AND s.endDate =  @EndDate " +
                    "AND c.name = @CustomerName AND c.country = @CustomerCountry", 
                    new { CustomerName = param1, CustomerCountry = param2, StartDate = param3 , EndDate = param4 }).FirstOrDefault();
            }
        }

        public Response2 Get2(string param1, string param2)
        {
            throw new NotImplementedException();
        }

        public Response3 Get3(string param1, string param2)
        {
            throw new NotImplementedException();
        }

        public Response4 Get4(string param1, string param2)
        {
            throw new NotImplementedException();
        }
    }
}
