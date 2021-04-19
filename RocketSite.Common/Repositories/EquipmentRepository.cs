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
                var sqlQuery = $"INSERT INTO Equipment (name, producer, cost, rocketName, rocketVersion) " +
                    "VALUES(@Name, @Producer, @Cost, @RocketName, @RocketVersion)";
                db.Execute(sqlQuery,
                    new
                    {
                        @object.Name, @object.Producer, @object.Cost,
                        RocketName = @object.Rocket.Name,
                        RocketVersion = @object.Rocket.Version
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
                var itemList = db.Query("SELECT * FROM Equipment WHERE name = @Name AND producer = @Producer", @object);

                return (from item in itemList
                        let rocket = new Rocket { Name = item.rocketName, Version = item.rocketVersion }
                        select new Equipment
                        {
                            Name = item.name,
                            Producer = item.producer,
                            Cost = item.cost,
                            Rocket = rocket
                        }).FirstOrDefault();
            }
        }

        public List<Equipment> GetObjects()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query("SELECT * FROM Equipment");

                return (from item in itemList
                        let rocket = new Rocket { Name = item.rocketName, Version = item.rocketVersion }
                        select new Equipment
                        {
                            Name = item.name,
                            Producer = item.producer,
                            Cost = item.cost,
                            Rocket = rocket
                        }).ToList();
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
                    $"rocketName = @RocketName, " +
                    $"rocketVersion = @RocketVersion " +
                    $"WHERE name = @Key1 AND producer = @Key2";
                db.Execute(sqlQuery, new
                {
                    @object.Name, @object.Producer, @object.Cost,
                    RocketName = @object.Rocket.Name,
                    RocketVersion = @object.Rocket.Version,
                    Key1 = key.First, Key2 = key.Second
                });
            }
        }
    }
}
