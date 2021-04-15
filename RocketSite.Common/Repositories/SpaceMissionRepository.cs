using Dapper;
using Microsoft.Data.SqlClient;
using RocketSite.Common.Interfaces;
using RocketSite.Common.Models;
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
                var sqlQuery = $"INSERT INTO SpaceMission (name, statys, cost, aititude, startDate, endDate, rocketName, rocketVersion) " +
                    "VALUES(@Name, @Statys, @Cost, @Aititude, @StartDate, @EndDate, @RocketName, @RocketVersion)";
                db.Execute(sqlQuery,
                    new
                    {
                        Name = @object.Name,
                        Statys = @object.Status,
                        Cost = @object.Cost,
                        Aititude = @object.Altitude,
                        StartDate = @object.StartDate,
                        EndDate = @object.EndDate,
                        RocketName = @object.Rocket.Name,
                        RocketVersion = @object.Rocket.Version
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
                return db.Query<SpaceMission>("SELECT * FROM SpaceMission WHERE name = @Name", @object).FirstOrDefault();
            }
        }

        public List<SpaceMission> GetObjects()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<SpaceMission, Rocket, SpaceMission>(
                    "SELECT * FROM SpaceMission as s LEFT JOIN Rocket as r ON s.rocketName = r.name AND s.rocketVersion = r.version",
                    (s, r) =>
                    {
                        SpaceMission mission = s;
                        s.Rocket = r;
                        s.Resources = new List<Resources>();
                        return mission;
                    }, splitOn: "rocketName").ToList(); ;
            }
        }

        public void Update(SpaceMission @object, Key key)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                StringBuilder builder = new StringBuilder();
                builder.Append($"UPDATE SpaceMission SET " +
                               $"name = @Name, " +
                               $"country = @Country, " +
                               $"education = @Education, " +
                               $"sex = @Sex, " +
                               $"profession = @Profession ");
                builder.Append($"WHERE name = \'{key.First}\' AND country = \'{key.Second}\'");
                db.Execute(builder.ToString(), new
                {
                    Name = @object.Name,
                    Statys = @object.Status,
                    Cost = @object.Cost,
                    Aititude = @object.Altitude,
                    StartDate = @object.StartDate,
                    EndDate = @object.EndDate,
                    RocketName = @object.Rocket.Name,
                    RocketVersion = @object.Rocket.Version
                });
            }
        }
    }
}
