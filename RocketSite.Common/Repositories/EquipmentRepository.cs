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
    public class EquipmentRepository : ICRUDRepository<Equipment>
    {
        private readonly string _connectionString;
        public EquipmentRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }
        public void Create(Equipment @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO Equipment (name, producer, cost, compatibleRocketName, compatibleRocketVersion) " +
                    "VALUES(@Name, @Producer, @Cost, @CompatibleRocketName, @CompatibleRocketVersion)";
                db.Execute(sqlQuery,
                    new
                    {
                        Name = @object.Name, Producer = @object.Producer, Cost = @object.Cost,
                        CompatibleRocketName = @object.CompatibleRocket.Name,
                        CompatibleRocketVersion = @object.CompatibleRocket.Version
                    });
            }
        }

        public void Delete(Equipment @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM Equipment WHERE name = @Name AND producer = @Producer";
                db.Execute(sqlQuery, @object);
            }
        }

        public Equipment Get(Equipment @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Equipment>("SELECT * FROM Equipment WHERE name = @Name AND producer = @Producer", @object).FirstOrDefault();
            }
        }

        public List<Equipment> GetObjects()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Equipment>("SELECT * FROM Equipment").ToList();
            }
        }

        public void Update(Equipment @object, Key key)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"UPDATE Equipment SET " +
                    $"name = @Name, " +
                    $"producer = @Producer, " +
                    $"cost = @Cost, " +
                    $"compatibleRocketName = @CompatibleRocketName, " +
                    $"compatibleRocketVersion = @CompatibleRocketVersion " +
                    $"WHERE name = \'{key.First}\' AND producer = \'{key.Second}\'";
                db.Execute(sqlQuery, new
                {
                    Name = @object.Name,
                    Producer = @object.Producer,
                    Cost = @object.Cost,
                    CompatibleRocketName = @object.CompatibleRocket.Name,
                    CompatibleRocketVersion = @object.CompatibleRocket.Version
                });
            }
        }
    }
}
