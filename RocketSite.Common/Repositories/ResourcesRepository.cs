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
                return db.Query<Resources>("SELECT * FROM Resources WHERE name = @Name AND type = @Type", @object).FirstOrDefault();
            }
        }

        public List<Resources> GetObjects()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Resources>("SELECT * FROM Resources").ToList();
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
                    $"WHERE name = \'{key.First}\' AND type = {(int)Enum.Parse(typeof(ResourceOption), key.Second)}";
                db.Execute(sqlQuery, @object);
            }
        }
    }
}
