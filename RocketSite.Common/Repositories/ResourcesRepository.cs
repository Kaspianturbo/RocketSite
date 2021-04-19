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
using RocketSite.Common.Options;

namespace RocketSite.Common.Repositories
{
    public class ResourcesRepository : ICRUDRepository<Resources>
    {
        private readonly string _connectionString;
        public ResourcesRepository(string connectionString)
        {
            this._connectionString = connectionString;
        }
        public void Create(Resources @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO Resources (name, type, emaunt, cost) " +
                    "VALUES(@Name, @Type, @Emaunt, @Cost)";
                db.Execute(sqlQuery, @object);
            }
        }

        public void Delete(Resources @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM Resources WHERE name = @Name AND type = @Type";
                db.Execute(sqlQuery, @object);
            }
        }

        public Resources Get(Resources @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query("SELECT * FROM Resources WHERE name = @Name AND type = @Type", @object);

                return (from item in itemList
                    select new Resources
                    {
                        Name = item.name,
                        Type = Enum.Parse<ResourceOption>(item.type),
                        Emaunt = item.emaunt,
                        Cost = item.cost
                    }).FirstOrDefault();
            }
        }

        public List<Resources> GetObjects()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var itemList = db.Query("SELECT * FROM Resources");

                return (from item in itemList
                    select new Resources
                    {
                        Name = item.name,
                        Type = Enum.Parse<ResourceOption>(item.type),
                        Emaunt = item.emaunt,
                        Cost = item.cost
                    }).ToList();
            }
        }

        public void Update(Resources @object, Key key)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"UPDATE Resources SET " +
                    $"name = @Name, " +
                    $"type = @Type, " +
                    $"emaunt = @Emaunt, " +
                    $"cost = @Cost " +
                    $"WHERE name = @Key1 AND type = @Key2";
                db.Execute(sqlQuery, new
                {
                    @object.Name, @object.Type, @object.Cost, 
                    @object.Emaunt, Key1 = key.First, Key2 = Enum.Parse<ResourceOption>(key.Second)
                });
            }
        }
    }
}
