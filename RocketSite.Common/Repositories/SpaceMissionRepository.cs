using Dapper;
using Microsoft.Data.SqlClient;
using RocketSite.Common.Interfaces;
using RocketSite.Common.Models;
using RocketSite.Common.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketSite.Common.Repositories
{
    public class SpaceMissionRepository : ICRUDRepository<SpaceMission>
    {
        private readonly string _connectionString;
        public SpaceMissionRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Create(SpaceMission @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO SpaceMission (name, status, cost, altitude, startDate, endDate, rocketName, rocketVersion, cosmodromeName) " +
                    "VALUES(@Name, @Status, @Cost, @Altitude, @StartDate, @EndDate, @RocketName, @RocketVersion, @CosmodromeName)";
                db.Execute(sqlQuery,
                    new
                    {
                        @object.Name,
                        @object.Status,
                        @object.Cost,
                        @object.Altitude,
                        @object.StartDate,
                        @object.EndDate,
                        RocketName = @object.Rocket.Name,
                        RocketVersion = @object.Rocket.Version,
                        CosmodromeName = @object.Cosmodrome.Name
                    });
            }
        }

        public void Delete(SpaceMission @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM SpaceMission WHERE name = @Name";
                db.Execute(sqlQuery, @object);
            }
        }

        public SpaceMission Get(SpaceMission @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query("SELECT * FROM SpaceMission WHERE name = @Name", @object);

                return (from item in itemList
                        let rocket = new Rocket { Name = item.rocketName, Version = item.rocketVersion }
                        let cosmodrome = new Cosmodrome { Name = item.cosmodromeName }
                        select new SpaceMission
                        {
                            Name = item.name,
                            Status = Enum.Parse<StatusOption>(item.status),
                            Cost = item.cost,
                            Altitude = item.altitude,
                            StartDate = item.startDate,
                            EndDate = item.endDate,
                            Rocket = rocket,
                            Cosmodrome = cosmodrome
                        }).FirstOrDefault();
            }
        }

        public List<SpaceMission> GetObjects()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query("SELECT * FROM SpaceMission");

                return (from item in itemList
                        let rocket = new Rocket { Name = item.rocketName, Version = item.rocketVersion }
                        let cosmodrome = new Cosmodrome { Name = item.cosmodromeName }
                        select new SpaceMission
                        {
                            Name = item.name,
                            Status = Enum.Parse<StatusOption>(item.status),
                            Cost = item.cost,
                            Altitude = item.altitude,
                            StartDate = item.startDate,
                            EndDate = item.endDate,
                            Rocket = rocket,
                            Cosmodrome = cosmodrome
                        }).ToList();
            }
        }

        public void Update(SpaceMission @object, Key key)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var builder = new StringBuilder();
                builder.Append($"UPDATE SpaceMission SET " +
                               $"name = @Name, " +
                               $"status = @Status, " +
                               $"cost = @Cost, " +
                               $"altitude = @Altitude, " +
                               $"startDate = @StartDate, " +
                               $"endDate = @EndDate, " +
                               $"rocketName = @RocketName, " +
                               $"rocketVersion = @RocketVersion, " +
                               $"cosmodromeName = @CosmodromeName ");
                builder.Append($"WHERE name = \'{key.First}\'");
                db.Execute(builder.ToString(), new
                {
                    @object.Name,
                    @object.Status,
                    @object.Cost,
                    @object.Altitude,
                    @object.StartDate,
                    @object.EndDate,
                    RocketName = @object.Rocket.Name,
                    RocketVersion = @object.Rocket.Version,
                    CosmodromeName = @object.Cosmodrome.Name
                });
            }
        }
    }
}
