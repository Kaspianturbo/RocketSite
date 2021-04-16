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
    public class CustomerRepository : ICRUDRepository<Customer>
    {
        private readonly string _connectionString;
        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void Create(Customer @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"INSERT INTO Customer (name, country, totalWortht, spaceMissionName) " +
                    "VALUES(@Name, @Country, @TotalWortht, @SpaceMissionName)";
                db.Execute(sqlQuery, @object);
            }
        }

        public void Delete(Customer @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = "DELETE FROM Customer WHERE name = @Name AND country = @Country";
                db.Execute(sqlQuery, @object);
            }
        }

        public Customer Get(Customer @object)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Customer>("SELECT * FROM Customer WHERE name = @Name AND country = @Country", @object).FirstOrDefault();
            }
        }

        public List<Customer> GetObjects()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return db.Query<Customer>("SELECT * FROM Customer").ToList();
            }
        }

        public void Update(Customer @object, Key key)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"UPDATE Customer SET " +
                    $"name = @Name, " +
                    $"country = @Country, " +
                    $"totalWorth = @TotalWorth, " +
                    $"spaceMissionName = @SpaceMissionName " +
                    $"WHERE name = \'{key.First}\' AND country = \'{key.Second}\'";
                db.Execute(sqlQuery, @object);
            }
        }
    }
}
