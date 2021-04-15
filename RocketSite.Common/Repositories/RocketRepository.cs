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
        private readonly string connectionString;
        public RocketRepository(string _connectionString)
        {
            connectionString = _connectionString;
        }
        public void Create(Rocket user)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = $"INSERT INTO Rocket (name, version, weight, height, diameter, cost, stages, massToLEO, massToGTO, engineType) " +
                    "VALUES(@Name, @Version, @Weight, @Height, @Diameter, @Cost, @Stages, @MassToLEO, @MassToGTO, @EngineType)";
                db.Execute(sqlQuery, user);
            }
        }

        public void Delete(Rocket @object)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Rocket WHERE name = @Name AND version = @Version";
                db.Execute(sqlQuery, @object);
            }
        }

        public Rocket Get(Rocket @object)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Rocket>("SELECT * FROM Rocket WHERE name = @Name AND version = @Version", @object).FirstOrDefault();
            }
        }

        public List<Rocket> GetObjects()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Rocket>("SELECT * FROM Rocket").ToList();
            }
        }

        public void Update(Rocket rocket, Key key)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
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
