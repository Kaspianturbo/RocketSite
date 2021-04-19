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
    public class RocketRepository : ICRUDRepository<Rocket>
    {
        private readonly string _connectionString;
        public RocketRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Create(Rocket user)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO Rocket (name, version, weight, height, diameter, cost, stages, massToLEO, massToGTO, engineType) " +
                    "VALUES(@Name, @Version, @Weight, @Height, @Diameter, @Cost, @Stages, @MassToLEO, @MassToGTO, @EngineType)";
                db.Execute(sqlQuery, user);
            }
        }

        public void Delete(Rocket @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM Rocket WHERE name = @Name AND version = @Version";
                db.Execute(sqlQuery, @object);
            }
        }

        public Rocket Get(Rocket @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query("SELECT * FROM Rocket WHERE name = @Name AND version = @Version", @object);

                return (from item in itemList
                        select new Rocket
                        {
                            Name = item.name,
                            Version = item.version,
                            Weight = item.weight,
                            Height = item.height,
                            Diameter = item.diameter,
                            Cost = item.cost,
                            Stages = item.stages,
                            MassToLEO = item.massToLEO,
                            MassToGTO = item.massToGTO,
                            EngineType = item.engineType
                        }).FirstOrDefault();
            }
        }

        public List<Rocket> GetObjects()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query("SELECT * FROM Rocket");

                return (from item in itemList
                        select new Rocket
                        {
                            Name = item.name,
                            Version = item.version,
                            Weight = item.weight,
                            Height = item.height,
                            Diameter = item.diameter,
                            Cost = item.cost,
                            Stages = item.stages,
                            MassToLEO = item.massToLEO,
                            MassToGTO = item.massToGTO,
                            EngineType = item.engineType
                        }).ToList();
            }
        }

        public void Update(Rocket rocket, Key key)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"UPDATE Rocket SET " +
                    $"name = @Name, " +
                    $"version = @Version, " +
                    $"weight = @Weight, " +
                    $"height = @Height, " +
                    $"diameter = @Diameter, " +
                    $"cost = @Cost, " +
                    $"stages = @Stages, " +
                    $"massToLEO = @MassToLEO, " +
                    $"massToGTO = @MassToGTO, " +
                    $"engineType = @EngineType " +
                    $"WHERE name = \'{key.First}\' AND version = \'{key.Second}\'";
                db.Execute(sqlQuery, rocket);
            }
        }
    }
}
